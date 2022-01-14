using System;
using System.Linq;
using FuseIT.Sitecore.Personalization.Facets;
using Sitecore;
using Sitecore.Data;
using Sitecore.Framework.Conditions;
using Sitecore.Modules.EmailCampaign.Core.Contacts;
using Sitecore.Modules.EmailCampaign.Core.Pipelines.GetContact;

namespace LionTrust.Feature.EXM
{
    public class GetContact
    {
        private readonly IContactService _contactService;

        public GetContact([NotNull] IContactService contactService)
        {
            Condition.Requires(contactService, nameof(contactService)).IsNotNull();

            _contactService = contactService;
        }

        public void Process([NotNull] GetContactPipelineArgs args)
        {
            Condition.Requires(args, nameof(args)).IsNotNull();

            if (args.ContactIdentifier == null && ID.IsNullOrEmpty(args.ContactId))
            {
                throw new ArgumentException("Either the contact identifier or the contact id must be set");
            }

            string[] facetKeys = args.FacetKeys.Concat(new[]
            {
                S4SInfo.DefaultFacetKey
            }).ToArray();

            args.Contact =
                args.ContactIdentifier != null ?
                    _contactService.GetContact(args.ContactIdentifier, facetKeys)
                    :
                    _contactService.GetContact(args.ContactId, facetKeys);
        }
    }
}
