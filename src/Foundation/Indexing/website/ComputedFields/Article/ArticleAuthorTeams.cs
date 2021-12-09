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

    public class ArticleAuthorTeams : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }
        
        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);

            var multiValueField = item?.Fields[Legacy.Constants.Article.Authors_FieldId];
            var ids = multiValueField != null ? ComputedValueHelper.ExtractIds(multiValueField) : null;            
            if (ids == null)               
            {
                return null;                
            }

            var results = new List<string>();
            foreach (var authorId in ids)
            {
                var authorItem = item.Database.GetItem(new ID(authorId));
                if (authorItem != null)
                {
                    results.AddRange(GetTeamIds(authorItem));
                }
            }
            
            return results.Distinct();
        }

        private IEnumerable<string> GetTeamIds(Item authorItem)
        {
            return (from link in Globals.LinkDatabase.GetItemReferrers(authorItem, false)
                    let sourceItem = link.GetSourceItem()
                    where sourceItem != null
                    where sourceItem.TemplateID.ToString() == Legacy.Constants.FundTeam.FundTeamTemplateId
                    select sourceItem.ID.Guid.ToString("N")).ToArray();
        }
    }
}