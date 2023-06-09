﻿namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using System;

    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Items;

    public class ArticleAuthorNames : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }
        
        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);

            var multiValueField = item?.Fields[Legacy.Constants.Article.Authors_FieldId];
            
            return multiValueField != null 
                                    ? ComputedValueHelper.GetMultiListValue(multiValueField, Legacy.Constants.Author.FullName_FieldId) 
                                    : string.Empty;            
        }
    }
}