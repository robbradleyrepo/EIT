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
    using Sitecore.ContentSearch.Utilities;
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
        private readonly IPersonalizedContentService _personalizedContentService;
        private readonly IFundContentSearchService _fundContentSearchService;


        public MyFundsManagerInsightsController(IMvcContext context, IArticleContentSearchService contentSearchService, IRequestContext requestContext, IPersonalizedContentService personalizedContentService, IFundContentSearchService fundContentSearchService)
        {
            _context = context;
            _contentSearchService = contentSearchService;
            _databaseName = requestContext.SitecoreService.Database.Name;
            _personalizedContentService = personalizedContentService;
            _fundContentSearchService = fundContentSearchService;
        }

        public ActionResult Render(string @ref)
        {
            var data = _context.GetDataSourceItem<IFundManagerInsightsBase>();
            IEnumerable<IArticlePromo> articles = new List<IArticlePromo>();

            if (!Sitecore.Context.PageMode.IsExperienceEditor && (data == null || Tracker.Current == null || !Tracker.IsActive || Tracker.Current.Contact == null))
            {
                return null;
            }

            var contactData = _personalizedContentService.GetContactFacetData(@ref);

            if (!Sitecore.Context.PageMode.IsExperienceEditor && (contactData == null || contactData.SalesforceFundIds == null || !contactData.SalesforceFundIds.Any()))
            {
                return null;
            }
            //Get the funds that are followed.
            var fundSearchRequest = new FundSearchRequest
            {
                DatabaseName = _databaseName,
                SalesforceFundIds = contactData.SalesforceFundIds,
                Take = int.MaxValue
            };

            var fundSearchResults = _fundContentSearchService.GetFunds(fundSearchRequest);

            if (fundSearchResults == null || fundSearchResults.TotalResults < 1)
            {
                return null;
            }

            var followedFunds = MapFundResultHits(fundSearchResults.SearchResults.ToList());

            var fundTeams = followedFunds.Select(t => new Guid(t.FundTeam))?.Distinct();

            if(fundTeams == null || !fundTeams.Any())
            {
                return null;
            }

            articles = new ArticleRepository(_contentSearchService, _context).Map(null, null, fundTeams, null, null, _databaseName);

            if (articles == null || !articles.Any())
            {
                return null;
            }

            var fundManagerInsightsViewModel = new FundManagerInsightsViewModel(data, articles);

            if (fundManagerInsightsViewModel.Data.CTA != null)
            {
                fundManagerInsightsViewModel.Data.CTA.Query = $"fundTeams={string.Join("|", fundTeams.Select(t => IdHelper.NormalizeGuid(t, true)))}";
            }

            return View("/views/article/fundmanagerinsights.cshtml", fundManagerInsightsViewModel);
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
                FundTeam = x.Document.FundTeam,
                FundTeamName = x.Document.FundTeamName,
                SalesforceFundId = x.Document.SalesforceFundId
            });
        }
    }
}