namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using System;

    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;

    public class ArticleCreatedDate : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }
        
        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            if (item == null)
            {
                return null;
            }

            var dateValue = item.Fields[Constants.ArticleDate_FieldId]?.Value;
            if (!string.IsNullOrEmpty(dateValue) && DateTime.TryParse(dateValue, out DateTime date))
            {
                return date;
            }

            return item.Created;
        }
    }
}