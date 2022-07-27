namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using System.Collections.Generic;
    using System.Linq;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data;
    using Sitecore.Data.Items;

    public class RelatedFundTeam : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }
        
        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);

            var lookupField = item?.Fields[Legacy.Constants.Article.Fund_FieldId];
            var teamId = lookupField != null ? ComputedValueHelper.GetDropLinkFieldValue(lookupField, Legacy.Constants.Fund.FundTeamFieldId) : null;
            
            if (!string.IsNullOrEmpty(teamId) && ID.TryParse(teamId, out ID id))
            {
                return id.Guid.ToString("N");
            }

            return null;
        }     
    }
}