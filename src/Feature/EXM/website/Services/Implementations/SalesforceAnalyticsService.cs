using FuseIT.Sitecore.Personalization.Facets;
using FuseIT.Sitecore.SalesforceConnector.Entities;
using Glass.Mapper.Sc;
using LionTrust.Feature.EXM.Extensions;
using LionTrust.Feature.EXM.Models;
using LionTrust.Feature.EXM.Services.Interfaces;
using LionTrust.Feature.EXM.ViewModels;
using LionTrust.Foundation.Contact.Enums;
using LionTrust.Foundation.Contact.Managers;
using LionTrust.Foundation.Contact.Services;
using LionTrust.Foundation.Logging.Repositories;
using LionTrust.Foundation.SitecoreExtensions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.SecurityModel;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactConstants = LionTrust.Foundation.Contact.Constants;

namespace LionTrust.Feature.EXM.Services.Implementations
{
    public class SalesforceAnalyticsService : ISalesforceAnalyticsService
    {
        private readonly ISitecoreService _sitecoreService;
        private readonly IMailManager _mailManager;
        private readonly ISFEntityUtility _sfEntityUtility;
        private readonly ISitecoreContactUtility _sitecoreContactUtility;
        private readonly ILogRepository _logRepository;

        public SalesforceAnalyticsService(ISitecoreService sitecoreService)
            : this(sitecoreService, ServiceLocator.ServiceProvider.GetService<IMailManager>(), ServiceLocator.ServiceProvider.GetService<ISFEntityUtility>(), ServiceLocator.ServiceProvider.GetService<ISitecoreContactUtility>(), ServiceLocator.ServiceProvider.GetService<ILogRepository>())
        {
        }

        public SalesforceAnalyticsService(ISitecoreService sitecoreService, IMailManager mailManager, ISFEntityUtility sfEntityUtility, ISitecoreContactUtility sitecoreContactUtility, ILogRepository logRepository)
        {
            _sitecoreService = sitecoreService;
            _mailManager = mailManager;
            _sfEntityUtility = sfEntityUtility;
            _sitecoreContactUtility = sitecoreContactUtility;
            _logRepository = logRepository;
        }

        public async Task<bool> RunSyncProcess()
        {
            var settings = _sitecoreService.GetItem<Item>(Constants.SalesforceSyncSettings.Id);

            try
            {
                if (settings.GetField(Constants.SalesforceSyncSettings.Running_FieldID, false))
                {
                    return false;
                }

                _logRepository.Info("[Salesforce Sync] Starting job...", this);

                SetRunning(settings, true);

                var lastSyncDate = settings.GetField(Constants.SalesforceSyncSettings.LastSyncDate_FieldID, DateTime.MinValue).ToUniversalTime();
                var entities = await GetEntityWithInteractions(lastSyncDate);
                SyncEngagementHistory(entities, settings);
                SyncScore(entities, settings);

                if (entities.SelectMany(x => x.Interactions).Any())
                {
                    var lastRun = entities.SelectMany(x => x.Interactions).Max(x => x.Date);
                    SetLastSyncDate(settings, lastRun);
                }
            }
            catch (Exception ex)
            {
                SetRunning(settings, false);

                var message = string.Format("Error updating EXM data to Salesforce: {0}", ex.Message);
                SendEmailOnError(message, ex, settings);

                return false;
            }

            SetRunning(settings, false);
            _logRepository.Info(string.Format("[Salesforce Sync] Job finished: LastRun {0}", DateTime.Now.ToString("f")), this);

            return true;
        }

        private async Task<List<EntityViewModel>> GetEntityWithInteractions(DateTime fromDate)
        {
            var entities = new List<EntityViewModel>();

            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
            {
                var asyncQueryable = client.Interactions
                        .Where(interaction => interaction.StartDateTime > fromDate && 
                            interaction.Events.Any(x => x.DefinitionId == Sitecore.EmailCampaign.Model.Constants.EmailOpenedPageEventId ||
                                x.DefinitionId == Sitecore.EmailCampaign.Model.Constants.EmailSentPageEventId ||
                                x.DefinitionId == Sitecore.EmailCampaign.Model.Constants.ClickPageEventId))
                        .WithExpandOptions(new InteractionExpandOptions
                        {
                            Contact = new RelatedContactExpandOptions(new string[2]
                            {
                                        EmailAddressList.DefaultFacetKey,
                                        S4SInfo.DefaultFacetKey
                            }),
                        });

                var interactionList = new List<Interaction>();
                var batchEnumeratorSync = asyncQueryable.GetBatchEnumeratorSync(1000);

                while (batchEnumeratorSync.MoveNext())
                {
                    var enumerator = batchEnumeratorSync.Current.GetEnumerator();
                    
                    while (enumerator.MoveNext())
                    {
                        Interaction current = enumerator.Current;

                        if (!current.CampaignId.HasValue)
                        {
                            continue;
                        }

                        var xConnectContact = current.Contact as Sitecore.XConnect.Contact;
                        var email = xConnectContact.GetFacet<EmailAddressList>(EmailAddressList.DefaultFacetKey)?.PreferredEmail?.SmtpAddress;
                        var s4sInfoFacet = xConnectContact.GetFacet<S4SInfo>(S4SInfo.DefaultFacetKey);

                        if (s4sInfoFacet == null)
                        {
                            continue;
                        }

                        s4sInfoFacet.Fields.TryGetValue(ContactConstants.SF_IdField, out var sfEntityId);
                        var entityType = _sitecoreContactUtility.GetEntityType(sfEntityId);

                        var entity = entities.FirstOrDefault(x => x.EntityId == current.Contact.Id.Value);
                        if(entity == null && entityType != Foundation.Contact.Enums.EntityType.None)
                        {
                            entity = new EntityViewModel()
                            {
                                EntityId = xConnectContact.Id.Value,
                                SalesforceEntityId = sfEntityId,
                                SalesforceEntityType = entityType == Foundation.Contact.Enums.EntityType.Contact
                                                        ? ContactConstants.SFContactEntityName
                                                        : ContactConstants.SfLeadEntityName,
                                Email = email
                            };
                            entities.Add(entity);
                        }

                        foreach (var ev in current.Events)
                        {
                            if (!(ev is EmailEvent emailEvent))
                            {
                                continue;
                            }

                            var messageItem = _sitecoreService.GetItem<IMailMessage>(emailEvent.MessageId);
                            if (string.IsNullOrWhiteSpace(messageItem?.SalesforceCampaignId))
                            {
                                continue;
                            }

                            var contactList = messageItem.IncludedRecipientLists.FirstOrDefault()?.Name;

                            var messageRoot = _sitecoreService.GetChildren(emailEvent.MessageId, Constants.MessageRoot.TemplateID).FirstOrDefault();
                            var messageUrl = messageRoot.GetAbsoluteMessageUrl();

                            if (ev is EmailSentEvent)
                            {
                                var interaction = GetInteractionViewModel(xConnectContact.Id.Value, sfEntityId, current.CampaignId.Value, entityType, messageItem, messageUrl, email, current.StartDateTime, InteractionType.EmailSent);
                                entity.Interactions.Add(interaction);
                            }
                            else if (ev is EmailOpenedEvent)
                            {
                                var firstTime = await IsFirstEmailOpen(current.CampaignId.Value, xConnectContact.Id.Value, current.StartDateTime);
                                var interaction = GetInteractionViewModel(xConnectContact.Id.Value, sfEntityId, current.CampaignId.Value, entityType, messageItem, messageUrl, email, current.StartDateTime, InteractionType.EmailOpen, firstTime);
                                entity.Interactions.Add(interaction);
                            }
                            else if (ev is UnsubscribedFromEmailEvent unsubscribedEvent)
                            {
                                var interaction = GetInteractionViewModel(xConnectContact.Id.Value, sfEntityId, current.CampaignId.Value, entityType, messageItem, messageUrl, email, current.StartDateTime, InteractionType.Unsubscribed);
                                entity.Interactions.Add(interaction);
                            }
                            else if (ev is EmailClickedEvent clickedEvent)
                            {
                                var interaction = GetInteractionViewModel(xConnectContact.Id.Value, sfEntityId, current.CampaignId.Value, entityType, messageItem, messageUrl, email, current.StartDateTime, InteractionType.LinkClicked, false, clickedEvent.Url);
                                entity.Interactions.Add(interaction);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }

            entities = SetInteractionPoints(entities);
            return entities;
        }        

        private bool SyncEngagementHistory(List<EntityViewModel> entities, Item settings)
        {
            try
            {
                var entitiesToSync = new List<GenericSalesforceEntity>();

                var interactions = entities.SelectMany(x => x.Interactions).Where(x => x.Type != InteractionType.Unsubscribed).OrderBy(x => x.Date);
                foreach (var interaction in interactions)
                {
                    var entity = _sfEntityUtility.GenerateEngagementHistory(
                        interaction.SalesforceEntityId,
                        interaction.SalesforceCampaignId,
                        interaction.EntityType,
                        interaction.Type,
                        interaction.ContactList,
                        interaction.Email,
                        interaction.MessageId,
                        interaction.MessageLink,
                        interaction.Link,
                        interaction.Date,
                        interaction.FirstTime);

                    entitiesToSync.Add(entity);
                }

                _sfEntityUtility.UpdateOrInsertEntities(entitiesToSync, ContactConstants.SfEngagementHistory, ContactConstants.EngagementHistory.SF_IdField);

                return true;
            }
            catch(Exception ex)
            {
                var message = string.Format("Exception occured when syncing engagement history objects to Salesforce: {0}", ex.Message);
                SendEmailOnError(message, ex, settings);

                return false;
            }
        }

        private bool SyncScore(List<EntityViewModel> entities, Item settings)
        {
            try
            {
                var contactsToSync = new List<GenericSalesforceEntity>();
                var leadsToSync = new List<GenericSalesforceEntity>();

                foreach (var entity in entities)
                {
                    var scorePoints = entity.Interactions.Sum(x => x.Score);

                    if (scorePoints <= 0)
                    {
                        continue;
                    }

                    var entityObj = _sfEntityUtility.GetEntityWithUpdatedScore(entity.SalesforceEntityId, entity.SalesforceEntityType, scorePoints);
                    if (entity.SalesforceEntityType == ContactConstants.SFContactEntityName)
                    {
                        contactsToSync.Add(entityObj);
                    }
                    else
                    {
                        leadsToSync.Add(entityObj);
                    }
                }

                _sfEntityUtility.UpdateOrInsertEntities(contactsToSync, ContactConstants.SFContactEntityName, ContactConstants.SF_IdField);
                _sfEntityUtility.UpdateOrInsertEntities(leadsToSync, ContactConstants.SfLeadEntityName, ContactConstants.SF_IdField);

                return true;
            }
            catch (Exception ex)
            {
                var message = string.Format("Exception occured when syncing contact/lead score to Salesforce: {0}", ex.Message);
                SendEmailOnError(message, ex, settings);

                return false;
            }
        }

        private List<EntityViewModel> SetInteractionPoints(List<EntityViewModel> entities)
        {
            var teams = _sitecoreService.GetChildren<ITeamScore>(Constants.TeamScore.TeamScoreFolderId, Constants.TeamScore.Id);
            entities = entities.Where(x => x.Interactions.Any()).ToList();

            foreach (var contact in entities)
            {
                var unsubscribed = false;
                contact.IsUnsubscribed = _sfEntityUtility.IsUnsubscribed(contact.SalesforceEntityId);

                if (!contact.IsUnsubscribed && !unsubscribed)
                {
                    foreach (var interaction in contact.Interactions)
                    {
                        var messageCampaign = _sitecoreService.GetItem<IMessageCampaign>(interaction.MessageId);
                        if (messageCampaign?.Team != null)
                        {
                            var team = teams.FirstOrDefault(x => x.Id == messageCampaign.Team.Value);

                            switch (interaction.Type)
                            {
                                case InteractionType.EmailOpen:
                                    interaction.Score = interaction.FirstTime ? team.EmailOpenedPoints : 0;
                                    break;
                                case InteractionType.LinkClicked:
                                    interaction.Score = team.ClickedThroughPoints;
                                    break;
                                case InteractionType.Unsubscribed:
                                    unsubscribed = true;
                                    break;
                            }

                            if (unsubscribed)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return entities;
        }

        private async Task<bool> IsFirstEmailOpen(Guid campaignId, Guid contactId, DateTime fromDate)
        {
            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
            {
                var hasPreviousEvents = await client.Interactions
                        .Where(interaction => interaction.CampaignId == campaignId && interaction.Contact.Id == contactId && interaction.StartDateTime < fromDate && interaction.Events.Any(x => x.DefinitionId == Sitecore.EmailCampaign.Model.Constants.EmailOpenedPageEventId))
                        .Any();

                return !hasPreviousEvents;
            }
        }

        private InteractionViewModel GetInteractionViewModel(
            Guid entityId, 
            string sfEntityId, 
            Guid campaignId,
            Foundation.Contact.Enums.EntityType entityType, 
            IMailMessage messageItem, 
            string messageUrl, 
            string email,
            DateTime date,
            InteractionType interactionType,
            bool firstTime = false,
            string link = null)
        {
            var contactList = messageItem.IncludedRecipientLists.FirstOrDefault()?.Name;

            var interaction = new InteractionViewModel
            {
                EntityId = entityId,
                EntityType = entityType,
                CampaignId = campaignId,
                Email = email,
                SalesforceEntityId = sfEntityId,
                SalesforceCampaignId = messageItem.SalesforceCampaignId,
                ContactList = contactList,
                MessageId = messageItem.Id,
                MessageName = messageItem.Name,
                MessageLink = messageUrl,
                Link = link,
                Date = date,
                FirstTime = firstTime,
                Type = interactionType
            };

            return interaction;
        }

        private void SetRunning(Item settings, bool value)
        {
            using (new SecurityDisabler())
            {
                settings.Editing.BeginEdit();
                settings.Fields[new ID(Constants.SalesforceSyncSettings.Running_FieldID)].Value = (value ? "1": "");
                settings.Editing.EndEdit();
            }
        }

        private void SetLastSyncDate(Item settings, DateTime dateTime)
        {
            using (new SecurityDisabler())
            {
                settings.Editing.BeginEdit();
                settings.Fields[new ID(Constants.SalesforceSyncSettings.LastSyncDate_FieldID)].Value = dateTime.ToDateTimeWithTicks();
                settings.Editing.EndEdit();
            }
        }

        private void SendEmailOnError(string message, Exception exception, Item settings)
        {
            _logRepository.Error(string.Format("[Salesforce Sync] {0}", message), exception);

            try
            {
                if (settings == null)
                {
                    return;
                }

                var fromAddress = settings.Fields[new ID(Constants.SalesforceSyncSettings.FromAddress_FieldID)].Value;
                var fromDisplayName = settings.Fields[new ID(Constants.SalesforceSyncSettings.FromDisplayName_FieldID)].Value;
                var toAddresses = settings.Fields[new ID(Constants.SalesforceSyncSettings.ToAddress_FieldID)].Value;
                var subject = settings.Fields[new ID(Constants.SalesforceSyncSettings.Subject_FieldID)].Value;
                _mailManager.SendEmail(fromAddress, fromDisplayName, toAddresses, subject, message, true);
            }
            catch (Exception ex)
            {
                _logRepository.Error("[Salesforce Sync]: Error sending email with salesforce sync information", ex);
            }
        }
    }
}