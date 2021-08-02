namespace LionTrust.Feature.Fund.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using LionTrust.Foundation.Contact.Services;
    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Analytics;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.Mvc.Controllers;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Feature.Fund.Repository;
    using FundSearchResultItem = Foundation.Search.Models.ContentSearch.FundSearchResultItem;

    public class MyFundsScrollerController: SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly IPersonalizedContentService _personalizedContentService;
        private readonly IFundContentSearchService _fundContentSearchService;

        public MyFundsScrollerController(IMvcContext context, IPersonalizedContentService personalizedContentService, IFundContentSearchService fundContentSearchService)
        {
            _context = context;
            _personalizedContentService = personalizedContentService;
            _fundContentSearchService = fundContentSearchService;
        }

        public ActionResult Render(string @ref)
        {
            var datasource = _context.GetDataSourceItem<IMyFundsScroller>();
            if (datasource == null || Tracker.Current == null || !Tracker.IsActive || Tracker.Current.Contact == null)
            {
                return null;
            }

            var contactData = _personalizedContentService.GetContactFacetData(@ref);

            var fundSearchRequest = new FundSearchRequest 
            { 
                Take = datasource.MaxFunds, 
                DatabaseName = _context.SitecoreService.Database.Name 
            };

            ContentSearchResults<FundSearchResultItem> fundSearchResults;
            if (!Sitecore.Context.PageMode.IsExperienceEditor && contactData == null)
            {
                return null;
            }
            else if (contactData!= null && contactData.SalesforceFundIds != null && contactData.SalesforceFundIds.Any())
            {
                //Get the funds that are followed.
                fundSearchRequest.Funds = contactData.SalesforceFundIds;
                fundSearchResults = _fundContentSearchService.GetFunds(fundSearchRequest);

                if (fundSearchResults != null && fundSearchResults.TotalResults > 0)
                {
                    var followedFunds = MapFundResultHits(_fundContentSearchService.GetFunds(fundSearchRequest)?.SearchResults);

                    //search based on these funds.
                    fundSearchRequest.FundManagers = followedFunds.SelectMany(f => f.FundManagers).Distinct();
                    fundSearchRequest.FundTeams = followedFunds.Select(f => f.FundTeam);
                    fundSearchRequest.FundRanges = followedFunds.SelectMany(f => f.FundRange).Distinct();
                    fundSearchRequest.FundRegions = followedFunds.Select(f => f.FundRegion);
                    fundSearchRequest.ExcludeFunds = followedFunds.Select(f => f.FundId.ToString());
                }
                else
                {
                    //clear the followed funds as we don't want to search for these again if no results.
                    fundSearchRequest.Funds = null;
                }

            }

            fundSearchResults = _fundContentSearchService.GetFunds(fundSearchRequest);

            if (fundSearchResults != null && fundSearchResults.TotalResults > 0)
            {
                datasource.Funds = fundSearchResults.SearchResults.Select(f => _context.SitecoreService.GetItem<IFundCard>(f.Document.ItemId.Guid));
            }
            else if(datasource.Funds == null)
            {
                //No Funds found.
                return null;
            }

            return View("/views/fund/fundscroller.cshtml", datasource);
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
                FundTeamName = x.Document.FundTeamName
            });
        }
    }
}