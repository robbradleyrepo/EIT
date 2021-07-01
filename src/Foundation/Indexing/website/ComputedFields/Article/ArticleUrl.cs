namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using LionTrust.Foundation.Indexing.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.DependencyInjection;

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
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            return _linkGenerator.GenerateLink(item);
        }
    }
}