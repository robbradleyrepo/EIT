﻿namespace LionTrust.Feature.EXM.Dispatch
{
    using FuseIT.Sitecore.Personalization.Facets;
    using Sitecore.Annotations;
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
        public CustomDispatchTask([NotNull] ShortRunningTaskPool taskPool, [NotNull] IRecipientValidator recipientValidator, [NotNull] IContactService contactService, [NotNull] EcmDataProvider dataProvider, [NotNull] ItemUtilExt itemUtil, [NotNull] IEventDataService eventDataService, [NotNull] IDispatchManager dispatchManager, [NotNull] EmailAddressHistoryManager emailAddressHistoryManager, [NotNull] IRecipientManagerFactory recipientManagerFactory, [NotNull] SentMessageManager sentMessageManager)
        : base(taskPool, recipientValidator, contactService, dataProvider, itemUtil, eventDataService, dispatchManager, emailAddressHistoryManager, recipientManagerFactory, sentMessageManager)
        {
            _contactService = contactService;
        }

        protected override IReadOnlyCollection<IEntityLookupResult<Contact>> GetContacts(List<DispatchQueueItem> dispatchQueueItems)
        {
            return _contactService.GetContacts(dispatchQueueItems.Select(x => x.ContactIdentifier), PersonalInformation.DefaultFacetKey, EmailAddressList.DefaultFacetKey, ConsentInformation.DefaultFacetKey, PhoneNumberList.DefaultFacetKey, ListSubscriptions.DefaultFacetKey, EmailAddressHistory.DefaultFacetKey, ExmKeyBehaviorCache.DefaultFacetKey, S4SInfo.DefaultFacetKey);
        }
    }
}
