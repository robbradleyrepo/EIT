namespace LionTrust.Feature.EXM.Dispatch
{
    using FuseIT.Sitecore.Personalization.Facets;
    using LionTrust.Foundation.Contact.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Annotations;
    using Sitecore.DependencyInjection;
    using Sitecore.EmailCampaign.Cm.Dispatch;
    using Sitecore.EmailCampaign.Cm.Managers;
    using Sitecore.EmailCampaign.Model.XConnect.Facets;
    using Sitecore.ExM.Framework.Distributed.Tasks.TaskPools.ShortRunning;
    using Sitecore.Modules.EmailCampaign.Core;
    using Sitecore.Modules.EmailCampaign.Core.Contacts;
    using Sitecore.Modules.EmailCampaign.Core.Data;
    using Sitecore.Modules.EmailCampaign.Core.Dispatch;
    using Sitecore.Modules.EmailCampaign.Core.Pipelines.HandleMessageEventBase;
    using Sitecore.Modules.EmailCampaign.Factories;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Collection.Model;
    using Sitecore.XConnect.Operations;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomDispatchTask : DispatchTask
    {
        private IContactService _contactService;
        private ISFEntityUtility _sfEntityUtility;

        public CustomDispatchTask([NotNull] ShortRunningTaskPool taskPool, [NotNull] IRecipientValidator recipientValidator, [NotNull] IContactService contactService, [NotNull] EcmDataProvider dataProvider, [NotNull] ItemUtilExt itemUtil, [NotNull] IEventDataService eventDataService, [NotNull] IDispatchManager dispatchManager, [NotNull] EmailAddressHistoryManager emailAddressHistoryManager, [NotNull] IRecipientManagerFactory recipientManagerFactory, [NotNull] SentMessageManager sentMessageManager)
        : this(taskPool, recipientValidator, contactService, dataProvider, itemUtil, eventDataService, dispatchManager, emailAddressHistoryManager, recipientManagerFactory, sentMessageManager, ServiceLocator.ServiceProvider.GetService<ISFEntityUtility>())
        {
        }

        public CustomDispatchTask([NotNull] ShortRunningTaskPool taskPool, [NotNull] IRecipientValidator recipientValidator, [NotNull] IContactService contactService, [NotNull] EcmDataProvider dataProvider, [NotNull] ItemUtilExt itemUtil, [NotNull] IEventDataService eventDataService, [NotNull] IDispatchManager dispatchManager, [NotNull] EmailAddressHistoryManager emailAddressHistoryManager, [NotNull] IRecipientManagerFactory recipientManagerFactory, [NotNull] SentMessageManager sentMessageManager, ISFEntityUtility sfEntityUtility)
        : base(taskPool, recipientValidator, contactService, dataProvider, itemUtil, eventDataService, dispatchManager, emailAddressHistoryManager, recipientManagerFactory, sentMessageManager)
        {
            _contactService = contactService;
            _sfEntityUtility = sfEntityUtility;
        }

        protected override IReadOnlyCollection<IEntityLookupResult<Contact>> GetContacts(List<DispatchQueueItem> dispatchQueueItems)
        {
            dispatchQueueItems = ExcludeUnsubscribedContacts(dispatchQueueItems);
            return _contactService.GetContacts(dispatchQueueItems.Select(x => x.ContactIdentifier), PersonalInformation.DefaultFacetKey, EmailAddressList.DefaultFacetKey, ConsentInformation.DefaultFacetKey, PhoneNumberList.DefaultFacetKey, ListSubscriptions.DefaultFacetKey, EmailAddressHistory.DefaultFacetKey, ExmKeyBehaviorCache.DefaultFacetKey, S4SInfo.DefaultFacetKey);
        }

        private List<DispatchQueueItem> ExcludeUnsubscribedContacts(List<DispatchQueueItem> dispatchQueueItems)
        {
            var result = new List<DispatchQueueItem>();

            // exclude unsubscribed contacts
            foreach (var item in dispatchQueueItems)
            {
                var contact = _contactService.GetContact(item.ContactIdentifier, S4SInfo.DefaultFacetKey, EmailAddressList.DefaultFacetKey);
                var s4sInfo = contact.GetFacet<S4SInfo>();
                var email = contact.Emails()?.PreferredEmail?.SmtpAddress;

                var isUnsubscribedOrHardBounced = _sfEntityUtility.IsUnsubscribedOrHardBounced(s4sInfo, email);
                if (!isUnsubscribedOrHardBounced)
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
