namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using LionTrust.Foundation.Indexing.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.DependencyInjection;
    using Sitecore.Links;
    using Sitecore.Sites;

    public class ArticleUrl : IComputedIndexField
    {
        private readonly IndexableLinkGenerator _linkGenerator;

        public ArticleUrl()
        {
            this._linkGenerator = ServiceLocator.ServiceProvider.GetService<IndexableLinkGenerator>();
        }

        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var articlePageUrl = string.Empty;
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            if (item != null)
            {
                articlePageUrl = LinkManager.GetItemUrl(item, new UrlOptions { AlwaysIncludeServerUrl = true, LowercaseUrls = true, LanguageEmbedding = LanguageEmbedding.Never, SiteResolving = true });
            }

            return articlePageUrl;
        }
    }
}