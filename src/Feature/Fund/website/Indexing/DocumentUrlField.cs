namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using Sitecore.Abstractions;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;
    using Sitecore.Sites;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(ISearchUrlField), Lifetime = Lifetime.Singleton)]
    public class DocumentUrlField : ISearchUrlField
    {
        private readonly BaseFactory _factory;

        public DocumentUrlField(BaseFactory factory)
        {
            _factory = factory;
        }

        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(new Guid(Foundation.Legacy.Constants.Document.TemplateId));
        }

        public string GetUrl(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var hashedUrl = string.Empty;

            var document = (LinkField)item.Fields[Foundation.Legacy.Constants.Document.DownloadLink_FieldId];

            if (!string.IsNullOrWhiteSpace(document.Value))
            {
                return HashingUtils.ProtectAssetUrl(document.GetFriendlyUrl());
            }

            return hashedUrl;
        }
    }
}