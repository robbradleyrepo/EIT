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
    using Sitecore.Data;

    public class GenericContentSearchService : IGenericContentSearchService
    {
        private readonly IGenericContentSearchRepository _genericContentSearchRepository;

        public GenericContentSearchService(IGenericContentSearchRepository genericContentSearchRepository)
        {
            _genericContentSearchRepository = genericContentSearchRepository;
        }

        private Expression<Func<GenericSearchResultItem, bool>> PopoulateDatedTaxonomyPredicate(Expression<Func<GenericSearchResultItem, bool>> predicate, GenericSearchRequest genericSearchRequest)
        {
            var taxonomyFilter = PredicateBuilder.True<GenericSearchResultItem>();
            if (genericSearchRequest.Months != null && genericSearchRequest.Months.Any())
            {
                var monthPredicate = PredicateBuilder.False<GenericSearchResultItem>();
                monthPredicate = genericSearchRequest.Months.Aggregate(monthPredicate,
                                                                          (current, month) => current
                                                                                                  .Or(item => item.Month == month));
                taxonomyFilter = taxonomyFilter.And(monthPredicate);
            }

            if (genericSearchRequest.Years != null && genericSearchRequest.Years.Any())
            {
                var yearPredicate = PredicateBuilder.False<GenericSearchResultItem>();
                yearPredicate = genericSearchRequest.Years.Aggregate(yearPredicate,
                                                                          (current, year) => current
                                                                                                  .Or(item => item.Year == year));
                taxonomyFilter = taxonomyFilter.And(yearPredicate);
            }
                        
            if (genericSearchRequest.ListingType != null && genericSearchRequest.ListingType.Any())
            {
                var listingTypePredicate = PredicateBuilder.False<GenericSearchResultItem>();
                listingTypePredicate = genericSearchRequest.ListingType.Aggregate(listingTypePredicate,
                                                                          (current, listingType) => current
                                                                                                  .Or(item => item.GenericListingType == IdHelper.NormalizeGuid(listingType, true)));

                taxonomyFilter = taxonomyFilter.And(listingTypePredicate);                
            }

            predicate = predicate.And(taxonomyFilter);

            if (!string.IsNullOrEmpty(genericSearchRequest.SearchTerm))
            {
                var searchTermPredicate = PredicateBuilder.False<GenericSearchResultItem>();
                searchTermPredicate = searchTermPredicate.Or(item => item.GenericListingTitle.Contains(genericSearchRequest.SearchTerm));
                searchTermPredicate = searchTermPredicate.Or(item => item.GenericListingSubtitle.Contains(genericSearchRequest.SearchTerm));
                searchTermPredicate = searchTermPredicate.Or(item => item.GenericListingText.Contains(genericSearchRequest.SearchTerm));

                predicate = predicate.Or(searchTermPredicate);
            }

            return predicate;
        }

        public ContentSearchResults<GenericSearchResultItem> GetTaxonomyRelatedGenericItems(GenericSearchRequest genericSearchRequest)
        {
            var predicate = PredicateBuilder.True<GenericSearchResultItem>();
            var language = Sitecore.Context.Language?.Name ?? "en";
            predicate = predicate.And(x => x.Language == language);
            if (!string.IsNullOrEmpty(genericSearchRequest.Parent)) 
            {
                var parent = new ID(genericSearchRequest.Parent);
                predicate = predicate.And(x => x.Parent == parent);
            }

            var country = OnboardingHelper.GetCurrentContactAddress()?.Country;
            predicate = predicate.And(x => !x.ExcludedCountries.Contains(country));

            predicate = this.PopoulateDatedTaxonomyPredicate(predicate, genericSearchRequest);

            return _genericContentSearchRepository.GetGenericSearchResultItems(predicate, genericSearchRequest.Skip, genericSearchRequest.Take, genericSearchRequest.DatabaseName);
        }
    }
}
