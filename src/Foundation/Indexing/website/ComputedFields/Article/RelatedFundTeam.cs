namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using System;
    using System.Collections.Generic;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;

    public class RelatedFundTeam : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }
        
        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var multiValueField = item?.Fields[Legacy.Constants.Article.Fund_FieldId];
            var fundTeamIdsString = multiValueField != null
                ? ComputedValueHelper.GetMultiListValue(multiValueField, Legacy.Constants.Fund.FundTeamFieldId)
                : null;
            if (fundTeamIdsString == null)
            {
                return null;
            }

            var fundTeamIds = fundTeamIdsString.Split('|');
            var formattedFundTeamIds = new List<string>();
            if (fundTeamIds.Length <= 0)
            {
                return formattedFundTeamIds;
            }

            foreach (var fundTeamId in fundTeamIds)
            {
                Guid fundTeamGuid;
                if (Guid.TryParse(fundTeamId, out fundTeamGuid))
                {
                    formattedFundTeamIds.Add(fundTeamGuid.ToString("N"));
                }
            }


            return formattedFundTeamIds;
        }
    }
}