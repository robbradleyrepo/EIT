namespace LionTrust.Feature.Article.Fields
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.Services;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using Sitecore.Abstractions;
    using Sitecore.Data.Items;
    using Sitecore.Sites;

    [Service(ServiceType = typeof(IFundTeamUrlField), Lifetime = Lifetime.Singleton)]
    public class FundTeamUrlField : ArticleBaseField, IFundTeamUrlField
    {
        private readonly IndexableLinkGenerator _indexableLinkGenerator;
        private readonly BaseFactory _factory;

        public FundTeamUrlField(IndexableLinkGenerator indexableLinkGenerator, BaseFactory factory)
        {
            this._indexableLinkGenerator = indexableLinkGenerator;
            this._factory = factory;
        }

        public string GetFundTeamUrl(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var fundField = (Sitecore.Data.Fields.LookupField)item.Fields[Foundation.Legacy.Constants.Article.Fund_FieldId];
            if (fundField == null || fundField.TargetItem == null)
            {
                return null;
            }

            var fundTeamField = (Sitecore.Data.Fields.LookupField)fundField.TargetItem.Fields[Foundation.Legacy.Constants.Fund.FundTeamFieldId];
            if (fundTeamField == null || fundTeamField.TargetItem == null)
            {
                return null;
            }

            var pageField = (Sitecore.Data.Fields.LookupField)fundTeamField.TargetItem.Fields[Foundation.Legacy.Constants.FundTeam.TeamPageFieldId];

            if (pageField == null || pageField.TargetItem == null)
            {
                return null;
            }

            using (new SiteContextSwitcher(_factory.GetSite(Foundation.Indexing.Constants.SiteName)))
            {
                return _indexableLinkGenerator.GenerateLink(pageField.TargetItem);
            }
        }
    }
}