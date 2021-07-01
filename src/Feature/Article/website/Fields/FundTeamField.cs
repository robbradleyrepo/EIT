namespace LionTrust.Feature.Article.Fields
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using Sitecore.Data.Items;

    [Service(ServiceType = typeof(IFundTeamField), Lifetime = Lifetime.Singleton)]
    public class FundTeamField : ArticleBaseField, IFundTeamField
    {
        public string GetFundTeam(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var fundField = (Sitecore.Data.Fields.LookupField) item.Fields[Foundation.Legacy.Constants.Article.Fund_FieldId];
            if (fundField == null || fundField.TargetItem == null)
            {
                return null;
            }

            var fundTeamField = (Sitecore.Data.Fields.LookupField) fundField.TargetItem.Fields[Foundation.Legacy.Constants.Fund.FundTeamFieldId];
            if (fundTeamField == null)
            {
                return null;
            }

            return fundTeamField.TargetItem[Foundation.Legacy.Constants.FundTeam.NameFieldId];
        }
    }
}