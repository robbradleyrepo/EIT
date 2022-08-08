using FuseIT.Sitecore.Personalization.Facets;
using FuseIT.Sitecore.SalesforceConnector.Entities;
using Glass.Mapper.Sc;
using LionTrust.Feature.EXM.Extensions;
using LionTrust.Feature.EXM.Models;
using LionTrust.Feature.EXM.Services.Interfaces;
using LionTrust.Feature.EXM.ViewModels;
using LionTrust.Foundation.Contact.Services;
using LionTrust.Foundation.SitecoreExtensions.Extensions;
using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Synchronous;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LionTrust.Feature.EXM.Services.Implementations
{
    public class SalesforceAnalyticsService : ISalesforceAnalyticsService
    {
        private readonly ISitecoreService _sitecoreService;
        private readonly ISFEntityUtility _sfEntityUtility;

        private const char IdentifierSeparator = '_';

        public SalesforceAnalyticsService(ISitecoreService sitecoreService, ISFEntityUtility sfEntityUtility)
        {
            _sitecoreService = sitecoreService;
            _sfEntityUtility = sfEntityUtility;
        }

        public List<ContactViewModel> GetContactWithInteractions(DateTime fromDate)
        {
            var contacts = new List<ContactViewModel>();

            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
            {
                IAsyncQueryable<Interaction> asyncQueryable = client.Interactions
                        .Where(interaction => interaction.LastModified >= fromDate)
                        .WithExpandOptions(new InteractionExpandOptions
                        {
                            Contact = new RelatedContactExpandOptions(new string[2]
                            {
                                        EmailAddressList.DefaultFacetKey,
                                        S4SInfo.DefaultFacetKey
                            }),
                        });

                List<Interaction> interactionList = new List<Interaction>();
                IEntityBatchEnumerator<Interaction> batchEnumeratorSync = asyncQueryable.GetBatchEnumeratorSync<Interaction>(1000);

                while (batchEnumeratorSync.MoveNext())
                {
                    IEnumerator<Interaction> enumerator = batchEnumeratorSync.Current.GetEnumerator();
                    
                    while (enumerator.MoveNext())
                    {
                        Interaction current = enumerator.Current;

                        var xConnectContact = current.Contact as Sitecore.XConnect.Contact;
                        var email = xConnectContact.GetFacet<EmailAddressList>(EmailAddressList.DefaultFacetKey)?.PreferredEmail?.SmtpAddress;
                        var s4sInfoFacet = xConnectContact.GetFacet<S4SInfo>(S4SInfo.DefaultFacetKey);

                        if (s4sInfoFacet == null)
                        {
                            continue;
                        }

                        var sfContactId = GetSalesforceContactId(xConnectContact);

                        var contact = contacts.FirstOrDefault(x => x.ContactId == current.Contact.Id.Value);
                        if(contact == null)
                        {
                            contact = new ContactViewModel()
                            {
                                ContactId = xConnectContact.Id.Value,
                                SalesforceContactId = sfContactId,
                                Email = email
                            };
                            contacts.Add(contact);
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
                                var interaction = new InteractionViewModel
                                {
                                    ContactId = xConnectContact.Id.Value,
                                    SitecoreCampaignId = messageItem.Id,
                                    Email = email,
                                    SalesforceContactId = sfContactId,
                                    SalesforceCampaignId = messageItem.SalesforceCampaignId,
                                    ContactList = contactList,
                                    MessageName = messageItem.Name,
                                    MessageLink = messageUrl,
                                    Date = current.LastModified.Value,
                                    Type = Enums.InteractionType.EmailSent
                                };
                                contact.Interactions.Add(interaction);
                            }
                            else if (ev is EmailOpenedEvent)
                            {
                                var interaction = new InteractionViewModel
                                {
                                    ContactId = xConnectContact.Id.Value,
                                    SitecoreCampaignId = messageItem.Id,
                                    Email = email,
                                    SalesforceContactId = sfContactId,
                                    SalesforceCampaignId = messageItem.SalesforceCampaignId,
                                    ContactList = contactList,
                                    MessageName = messageItem.Name,
                                    MessageLink = messageUrl,
                                    Date = current.LastModified.Value,
                                    Type = Enums.InteractionType.EmailOpen
                                };
                                contact.Interactions.Add(interaction);
                            }
                            else if (ev is UnsubscribedFromEmailEvent unsubscribedEvent)
                            {
                                var interaction = new InteractionViewModel
                                {
                                    ContactId = xConnectContact.Id.Value,
                                    SitecoreCampaignId = messageItem.Id,
                                    Email = email,
                                    SalesforceContactId = sfContactId,
                                    SalesforceCampaignId = messageItem.SalesforceCampaignId,
                                    ContactList = contactList,
                                    MessageName = messageItem.Name,
                                    MessageLink = messageUrl,
                                    Date = current.LastModified.Value,
                                    Type = Enums.InteractionType.Unsubscribed
                                };
                                contact.Interactions.Add(interaction);
                            }
                            else if (ev is EmailClickedEvent clickedEvent)
                            {
                                var interaction = new InteractionViewModel
                                {
                                    ContactId = xConnectContact.Id.Value,
                                    SitecoreCampaignId = messageItem.Id,
                                    Email = email,
                                    SalesforceContactId = sfContactId,
                                    SalesforceCampaignId = messageItem.SalesforceCampaignId,
                                    ContactList = contactList,
                                    MessageName = messageItem.Name,
                                    MessageLink = messageUrl,
                                    Link = clickedEvent.Url,
                                    Date = current.LastModified.Value,
                                    Type = Enums.InteractionType.LinkClicked
                                };
                                contact.Interactions.Add(interaction);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }

            contacts = SetInteractionPoints(contacts);

            //var entity = _sfEntityUtility.GetEntityByEmail("harry.scargill@fairstone.co.uk");
            //var fields = entity.InternalFields.AvailableFields().OrderBy(x => x).ToList();
            //foreach(var e in entity.InternalFields.AvailableFields())
            //{
            //    var test = entity.InternalFields[e];
            //    System.Diagnostics.Debug.Print($"{e}: {entity.InternalFields[e]}");
            //}

            return contacts;
        }        

        public List<GenericSalesforceEntity> GetSalesforceEntities(List<ContactViewModel> contacts)
        {
            var result = new List<GenericSalesforceEntity>();

            var interactions = contacts.SelectMany(x => x.Interactions);
            foreach(var interaction in interactions)
            {
                var entity = new GenericSalesforceEntity("interactions");
                entity.InternalFields["sitecoreCampaignId"] = interaction.SitecoreCampaignId.ToString("D");

                result.Add(entity);
            }

            return result;
        }

        public void SyncData(List<GenericSalesforceEntity> entities)
        {
            var sfEntityUtility = new SFEntityUtility();
            sfEntityUtility.UpdateOrInsertEntities(entities, "history engagement");
        }

        private List<List<GenericSalesforceEntity>> GetEntitiesList<GenericSalesforceEntity>(List<GenericSalesforceEntity> entities, int maxItems)
        {
            List<List<GenericSalesforceEntity>> result = new List<List<GenericSalesforceEntity>>();

            for (int index = 0; index < entities.Count; index += maxItems)
            {
                result.Add(entities.GetRange(index, Math.Min(maxItems, entities.Count - index)));
            }

            return result;
        }

        private string GetSalesforceContactId(Sitecore.XConnect.Contact contact)
        {
            string contactId = null;

            var identifier = contact.Identifiers.FirstOrDefault(x => x.Source == Constants.Identifier.S4S)?.Identifier;
            
            if (identifier != null && identifier.Split(IdentifierSeparator).Length > 1)
            {
                contactId = identifier.Split(IdentifierSeparator)[1];
            }

            return contactId;
        }

        private List<ContactViewModel> SetInteractionPoints(List<ContactViewModel> contacts)
        {
            var teams = _sitecoreService.GetChildren<ITeamScore>(Constants.TeamScore.TeamScoreFolderId, Constants.TeamScore.Id);
            contacts = contacts.Where(x => x.Interactions.Any()).ToList();

            foreach (var contact in contacts)
            {
                var unsubscribed = false;
                contact.IsUnsubscribed = _sfEntityUtility.IsUnsubscribed(contact.Email);

                if (!contact.IsUnsubscribed || !unsubscribed)
                {
                    foreach (var interaction in contact.Interactions)
                    {
                        var campaign = _sitecoreService.GetItem<ICampaign>(interaction.SitecoreCampaignId);
                        if (campaign?.Team != null)
                        {
                            var team = teams.FirstOrDefault(x => x.Id == campaign.Team.Value);

                            switch (interaction.Type)
                            {
                                case Enums.InteractionType.EmailOpen:
                                    interaction.Score = team.EmailOpenedPoints;
                                    break;
                                case Enums.InteractionType.LinkClicked:
                                    interaction.Score = team.ClickedThroughPoints;
                                    break;
                                case Enums.InteractionType.Unsubscribed:
                                    unsubscribed = true;
                                    break;
                            }
                        }
                    }
                }
            }

            return contacts;
        }
    }
}