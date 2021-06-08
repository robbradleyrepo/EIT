﻿namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using System;

    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Items;

    public class ArticleFundName : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }
        
        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);

            var multiValueField = item?.Fields[Legacy.Constants.Article.Fund_FieldId];

            return multiValueField != null
                                    ? ComputedValueHelper.GetMultiListValue(multiValueField, "LegacyFund_FundName")
                                    : null;
        }
    }
}