namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Links;
    using Sitecore.Sites;

    public class ArticleUrl : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var articleUrl = string.Empty;
            using (new SiteContextSwitcher(Factory.GetSite(Constants.SiteName)))
            {
                articleUrl = LinkManager.GetItemUrl(item, new UrlOptions {AlwaysIncludeServerUrl = true, LowercaseUrls = true } );
            }

            return articleUrl;
        }
    }
}