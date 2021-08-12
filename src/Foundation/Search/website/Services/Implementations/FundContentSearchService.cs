namespace LionTrust.Foundation.Search.Services.Implementations
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Repositories.Interfaces;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.ContentSearch.Utilities;

    public class FundContentSearchService : IFundContentSearchService
    {
        private readonly IFundContentSearchRepository _fundContentSearchRepository;

        public FundContentSearchService(IFundContentSearchRepository fundContentSearchRepository)
        {
            _fundContentSearchRepository = fundContentSearchRepository;
        }

        private Expression<Func<FundSearchResultItem, bool>> PopoulateFundPredicate(Expression<Func<FundSearchResultItem, bool>> predicate, FundSearchRequest fundSearchRequest)
        {
            var fundFilter = PredicateBuilder.True<FundSearchResultItem>();

            if (fundSearchRequest.FundManagers != null && fundSearchRequest.FundManagers.Any())
            {
                var managerPredicate = PredicateBuilder.False<FundSearchResultItem>();
                managerPredicate = fundSearchRequest
                                            .FundManagers
                                                    .Aggregate(managerPredicate,
                                                                    (current, manager)
                                                                                => current
                                                                                     .Or(item => item.FundManagers.Contains(manager)));

                fundFilter = fundFilter.And(managerPredicate);
            }

            if(fundSearchRequest.FundTeams != null && fundSearchRequest.FundTeams.Any())
            {
                var teamPredicate = PredicateBuilder.False<FundSearchResultItem>();
                teamPredicate = fundSearchRequest
                                            .FundTeams
                                                    .Aggregate(teamPredicate,
                                                                    (current, team)
                                                                                => current
                                                                                     .Or(item => item.FundTeam.Contains(team)));

                fundFilter = fundFilter.And(teamPredicate);
            }

            if (fundSearchRequest.FundRanges != null && fundSearchRequest.FundRanges.Any())
            {
                var rangePredicate = PredicateBuilder.False<FundSearchResultItem>();
                rangePredicate = fundSearchRequest
                                            .FundRanges
                                                    .Aggregate(rangePredicate,
                                                                    (current, range)
                                                                                => current
                                                                                     .Or(item => item.FundRanges.Contains(range)));

                fundFilter = fundFilter.And(rangePredicate);
            }

            if (fundSearchRequest.FundRegions != null && fundSearchRequest.FundRegions.Any())
            {
                var regionPredicate = PredicateBuilder.False<FundSearchResultItem>();
                regionPredicate = fundSearchRequest
                                            .FundRegions
                                                    .Aggregate(regionPredicate,
                                                                    (current, range)
                                                                                => current
                                                                                     .Or(item => item.FundRegion.Contains(range)));

                fundFilter = fundFilter.And(regionPredicate);
            }

            if (fundSearchRequest.SalesforceFundIds != null && fundSearchRequest.SalesforceFundIds.Any())
            {
                var fundsPredicate = PredicateBuilder.False<FundSearchResultItem>();
                fundsPredicate = fundSearchRequest
                                            .SalesforceFundIds
                                                    .Aggregate(fundsPredicate,
                                                                    (current, salesforceFundId)
                                                                                => current
                                                                                     .Or(item => item.SalesforceFundId.Contains(salesforceFundId)));

                predicate = predicate.And(fundsPredicate);
            }

            if (fundSearchRequest.ExcludeSalesforceFundIds != null && fundSearchRequest.ExcludeSalesforceFundIds.Any())
            {
                var excludeFundsPredicate = PredicateBuilder.False<FundSearchResultItem>();
                excludeFundsPredicate = fundSearchRequest
                                            .ExcludeSalesforceFundIds
                                                    .Aggregate(excludeFundsPredicate,
                                                                    (current, salesforceFundId)
                                                                                => current
                                                                                     .And(item => !item.SalesforceFundId.Contains(salesforceFundId)));

                predicate = predicate.And(excludeFundsPredicate);
            }

            predicate = predicate.And(fundFilter);

            if (!string.IsNullOrEmpty(fundSearchRequest.SearchTerm))
            {
                var searchTermPredicate = PredicateBuilder.False<FundSearchResultItem>();
                searchTermPredicate = searchTermPredicate.Or(item => item.FundName.Contains(fundSearchRequest.SearchTerm));
                searchTermPredicate = searchTermPredicate.Or(item => item.FundDescription.Contains(fundSearchRequest.SearchTerm));
                searchTermPredicate = searchTermPredicate.Or(item => item.Content.Contains(fundSearchRequest.SearchTerm));
                searchTermPredicate = searchTermPredicate.Or(item => item.FundCardHeading.Contains(fundSearchRequest.SearchTerm));
                searchTermPredicate = searchTermPredicate.Or(item => item.FundCardDescription.Contains(fundSearchRequest.SearchTerm));

                predicate = predicate.And(searchTermPredicate);
            }

            var validationPredicate = PredicateBuilder.True<FundSearchResultItem>();
            validationPredicate = validationPredicate.And(item => item.FundPageUrl != null && item.FundPageUrl != string.Empty);
            validationPredicate = validationPredicate.And(item => item.FundCardImage != null && item.FundCardImage != string.Empty);
            validationPredicate = validationPredicate.And(item => item.FundName != null && item.FundName != string.Empty);
            validationPredicate = validationPredicate.And(item => item.FundCardHeading != null && item.FundCardHeading != string.Empty);
            validationPredicate = validationPredicate.And(item => item.FundCardDescription != null && item.FundCardDescription != string.Empty);
            validationPredicate = validationPredicate.And(item => item.FundPageLinkText != null && item.FundPageLinkText != string.Empty);
            validationPredicate = validationPredicate.And(item => item.FundTeamName != null && item.FundTeamName != string.Empty);
            validationPredicate = validationPredicate.And(item => item.FundFactSheet != null && item.FundFactSheet != string.Empty);

            predicate = predicate.And(validationPredicate);

            return predicate;
        }


        public ContentSearchResults<FundSearchResultItem> GetFunds(FundSearchRequest fundSearchRequest, Func<IQueryable<FundSearchResultItem>, IQueryable<FundSearchResultItem>> sort = null)
        {
            var predicate = PredicateBuilder.True<FundSearchResultItem>();
            var language = Sitecore.Context.Language?.Name ?? "en";
            predicate = predicate.And(x => x.Language == language);

            var country = OnboardingHelper.GetCurrentContactAddress()?.Country;
            predicate = predicate.And(x => !x.ExcludedCountries.Contains(country));

            predicate = this.PopoulateFundPredicate(predicate, fundSearchRequest);

            return _fundContentSearchRepository.GetFundSearchResultItems(predicate, fundSearchRequest.Skip, fundSearchRequest.Take, fundSearchRequest.DatabaseName, sort);
        }
    }
}
