namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Feature.Fund.Repository;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.Services;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using Sitecore.Abstractions;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Sites;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(IFundTeamUrlField), Lifetime = Lifetime.Singleton)]
    public class FundManagerFundTeamUrlField : IFundTeamUrlField
    {
        private readonly IndexableLinkGenerator _indexableLinkGenerator;
        private readonly BaseFactory _factory;
        private readonly IFundRepository _fundRepository;

        public FundManagerFundTeamUrlField(IndexableLinkGenerator indexableLinkGenerator, BaseFactory factory, IFundRepository fundRepository)
        {
            _indexableLinkGenerator = indexableLinkGenerator;
            _factory = factory;
            _fundRepository = fundRepository;
        }

        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Constants.FundManager.TemplateId);
        }

        public string GetFundTeamUrl(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var fundManager = (Sitecore.Data.Fields.LookupField)item.Fields[Constants.FundManager.ManagerFieldId];
            if (fundManager == null || fundManager.TargetItem == null)
            {
                return null;
            }

            var fundTeam = _fundRepository.GetFundTeamByManagerId(fundManager.TargetID.Guid, item.Database.Name);

            if (fundTeam == null || fundTeam.Page == null || !fundTeam.Page.Any())
            {
                return null;
            }

           var fundTeamPage = item.Database.GetItem(new ID(fundTeam.Page.First()));

            if(fundTeamPage == null)
            {
                return null;
            }

            using (new SiteContextSwitcher(_factory.GetSite(Foundation.Indexing.Constants.SiteName)))
            {
                return _indexableLinkGenerator.GenerateLink(fundTeamPage);
            }
        }
    }
}