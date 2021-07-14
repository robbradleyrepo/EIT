namespace LionTrust.Feature.Article.Controllers
{
    using Glass.Mapper.Sc.Web;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.Repositories;
    using LionTrust.Foundation.Contact.Services;
    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Analytics;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class MyFundsManagerInsightsController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly IArticleContentSearchService _contentSearchService;
        private readonly string _databaseName;
        private readonly IContactService _contactService;
        private readonly IFundContentSearchService _fundContentSearchService;


        public MyFundsManagerInsightsController(IMvcContext context, IArticleContentSearchService contentSearchService, IRequestContext requestContext, IContactService contactService, IFundContentSearchService fundContentSearchService)
        {
            _context = context;
            _contentSearchService = contentSearchService;
            _databaseName = requestContext.SitecoreService.Database.Name;
            _contactService = contactService;
            _fundContentSearchService = fundContentSearchService;
        }

        public ActionResult Render()
        {
            var data = _context.GetDataSourceItem<IFundManagerInsightsBase>();
            IEnumerable<IArticlePromo> articles = new List<IArticlePromo>();

            if (!Sitecore.Context.PageMode.IsExperienceEditor && (data == null || Tracker.Current == null || !Tracker.IsActive || Tracker.Current.Contact == null))
            {
                return null;
            }

            var contactData = _contactService.GetCurrentSitecoreContactFacetData(Tracker.Current.Contact.ContactId.ToString());

            if (!Sitecore.Context.PageMode.IsExperienceEditor && (contactData == null || contactData.SalesforceFundIds == null || !contactData.SalesforceFundIds.Any()))
            {
                return null;
            }
            else
            {
                //Get the funds that are followed.
                var fundSearchRequest = new FundSearchRequest
                {
                    Funds = contactData.SalesforceFundIds
                };

                fundSearchRequest.Funds = contactData.SalesforceFundIds;
                var fundSearchResults = _fundContentSearchService.GetFunds(fundSearchRequest);

                if (fundSearchResults == null || fundSearchResults.TotalResults < 1)
                {
                    return null;
                }
                var followedFunds = MapFundResultHits(_fundContentSearchService.GetFunds(fundSearchRequest)?.SearchResults);

                //search based on these funds.
                fundSearchRequest.FundManagers = followedFunds.SelectMany(f => f.FundManagers).Distinct();
                fundSearchRequest.FundTeams = followedFunds.Select(f => f.FundTeam);
                fundSearchRequest.FundRanges = followedFunds.SelectMany(f => f.FundRange).Distinct();
                fundSearchRequest.FundRegions = followedFunds.Select(f => f.FundRegion);
                fundSearchRequest.ExcludeFunds = followedFunds.Select(f => f.FundId.ToString());

                articles = new ArticleRepository(_contentSearchService, _context).Map(contactData.SalesforceFundIds.Select(f => new Guid(f)), null, followedFunds.Select(f => new Guid(f.FundTeam))?.Distinct(), followedFunds.SelectMany(f => f.FundManagers.Select(fm => new Guid(fm)))?.Distinct(), null, _databaseName);

                if (articles == null || !articles.Any())
                {
                    return null;
                }
                else
                {
                    var fundManagerInsightsViewModel = new FundManagerInsightsViewModel(data, articles);
                    return View("/views/article/fundmanagerinsights.cshtml", fundManagerInsightsViewModel);
                }
            }
        }

        private IEnumerable<IFundContentResult> MapFundResultHits(IEnumerable<SearchHit<FundSearchResultItem>> hits)
        {
            return hits.Select(x => new FundResult
            {
                FundId = x.Document.ItemId.Guid,
                FundName = x.Document.FundName,
                Url = x.Document.FundPageUrl,
                FundCardImageUrl = x.Document.FundCardImage,
                FundCardHeading = x.Document.FundCardHeading,
                FundCardDescription = x.Document.FundCardDescription,
                CTAText = x.Document.FundPageLinkText,
                FundFactSheet = x.Document.FundFactSheet,
                FundManagers = x.Document.FundManagers,
                FundRange = x.Document.FundRanges,
                FundRegion = x.Document.FundRegion,
                FundTeam = x.Document.FundTeam
            });
        }
    }
}