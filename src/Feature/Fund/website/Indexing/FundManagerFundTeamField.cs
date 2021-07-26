namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Feature.Fund.Repository;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(IFundTeamField), Lifetime = Lifetime.Singleton)]
    public class FundManagerFundTeamField : IFundTeamField
    {
        private readonly IFundRepository _fundRepository;

        public FundManagerFundTeamField(IFundRepository fundRepository)
        {
            _fundRepository = fundRepository;
        }
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Constants.FundManager.TemplateId);
        }

        public string GetFundTeam(Item item)
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

            if (fundTeam == null)
            {
                return null;
            }

            return fundTeam.FundTeamName;
        }
    }
}