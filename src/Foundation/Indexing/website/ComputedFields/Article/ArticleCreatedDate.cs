namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using System;

    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Fields;

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

            DateField dateTimeField = item.Fields[Constants.ArticleDate_FieldId];
            if (dateTimeField != null && dateTimeField.DateTime != null && dateTimeField.DateTime != DateTime.MinValue)
            {
                return dateTimeField.DateTime;
            }

            return item.Created;
        }
    }
}