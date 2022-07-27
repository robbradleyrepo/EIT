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

    public class ArticleContentSearchService : IArticleContentSearchService
    {
        private readonly IArticleContentSearchRepository _articleContentSearchRepository;

        public ArticleContentSearchService(IArticleContentSearchRepository articleContentSearchRepository)
        {
            _articleContentSearchRepository = articleContentSearchRepository;
        }

        private Expression<Func<ArticleSearchResultItem, bool>> PopoulateDatedTaxonomyPredicate(Expression<Func<ArticleSearchResultItem, bool>> predicate, ITaxonomySearchRequest articleSearchRequest)
        {
            if (articleSearchRequest.Month != null)
            {                
                predicate = predicate.And(x => x.ArticleCreatedDateMonth == articleSearchRequest.Month);
            }
            
            if (articleSearchRequest.Year != null)
            {
                predicate = predicate.And(x => x.ArticleCreatedDateYear == articleSearchRequest.Year);
            }

            var taxonomyFilter = PredicateBuilder.True<ArticleSearchResultItem>();

            if (articleSearchRequest.ContentTypes != null && articleSearchRequest.ContentTypes.Any())
            {
                var contentTypePredicate = PredicateBuilder.False<ArticleSearchResultItem>();
                contentTypePredicate = articleSearchRequest.ContentTypes.Aggregate(contentTypePredicate,
                                                                                            (current, contentType) => current                                                                                                                    
                                                                                            .Or(item => item.ArticleContentType == IdHelper.NormalizeGuid(contentType, true)));

                taxonomyFilter = taxonomyFilter.And(contentTypePredicate);
            }

            if (articleSearchRequest.Funds != null && articleSearchRequest.Funds.Any())
            {
                var fundPredicate = PredicateBuilder.False<ArticleSearchResultItem>();
                fundPredicate = articleSearchRequest.Funds.Aggregate(fundPredicate,
                                                                          (current, fund) => current
                                                                                                  .Or(item => item.ArticleFund == IdHelper.NormalizeGuid(fund, true)));

                taxonomyFilter = taxonomyFilter.And(fundPredicate);
            }

            if (articleSearchRequest.FundManagers != null && articleSearchRequest.FundManagers.Any())
            {
                var managerPredicate = PredicateBuilder.False<ArticleSearchResultItem>();
                managerPredicate = articleSearchRequest
                                            .FundManagers
                                                    .Aggregate(managerPredicate,
                                                                    (current, manager)
                                                                                => current
                                                                                     .Or(item => item.ArticleAuthors.Contains(manager)));

                taxonomyFilter = taxonomyFilter.And(managerPredicate);
            }

            if (articleSearchRequest.Categories != null && articleSearchRequest.Categories.Any())
            {
                var topicPredicate = PredicateBuilder.False<ArticleSearchResultItem>();
                topicPredicate = articleSearchRequest
                                            .Categories
                                                    .Aggregate(topicPredicate,
                                                                    (current, category)
                                                                                => current
                                                                                     .Or(item => item.Topics.Contains(IdHelper.NormalizeGuid(category, true))));

                taxonomyFilter = taxonomyFilter.And(topicPredicate);
            }

            if (articleSearchRequest.FundTeams != null && articleSearchRequest.FundTeams.Any())
            {
                var teamPredicate = PredicateBuilder.False<ArticleSearchResultItem>();
                teamPredicate = articleSearchRequest
                                            .FundTeams
                                                    .Aggregate(teamPredicate,
                                                                    (current, team)
                                                                                => current
                                                                                     .Or(item => item.ArticleAuthorTeams.Contains(team)
                                                                                              || item.RelatedFundTeam == team));

                taxonomyFilter = taxonomyFilter.And(teamPredicate);
            }

            predicate = predicate.And(taxonomyFilter);

            if (!string.IsNullOrEmpty(articleSearchRequest.SearchTerm))
            {
                var searchTermPredicate = PredicateBuilder.False<ArticleSearchResultItem>();
                searchTermPredicate = searchTermPredicate.Or(item => item.ArticleContent.Contains(articleSearchRequest.SearchTerm));
                searchTermPredicate = searchTermPredicate.Or(item => item.Content.Contains(articleSearchRequest.SearchTerm));
                searchTermPredicate = searchTermPredicate.Or(item => item.ArticleTitle.Contains(articleSearchRequest.SearchTerm));
                searchTermPredicate = searchTermPredicate.Or(item => item.ArticleSubtitle.Contains(articleSearchRequest.SearchTerm));

                predicate = predicate.And(searchTermPredicate);
            }

            return predicate;
        }


        public ContentSearchResults<ArticleSearchResultItem> GetDatedTaxonomyRelatedArticles(ITaxonomySearchRequest articleSearchRequest, Func<IQueryable<ArticleSearchResultItem>, IQueryable<ArticleSearchResultItem>> sort = null)
        {
            var predicate = PredicateBuilder.True<ArticleSearchResultItem>();
            var language = Sitecore.Context.Language?.Name ?? "en";
            predicate = predicate.And(x => x.Language == language);
            predicate = predicate.And(x => x.IsLatestVersion);

            if (!Sitecore.Context.PageMode.IsExperienceEditorEditing)
            {
                var country = OnboardingHelper.GetCurrentContactCountryCode();
                predicate = predicate.And(x => !x.ExcludedCountries.Contains(country));
            }

            predicate = this.PopoulateDatedTaxonomyPredicate(predicate, articleSearchRequest);

            return _articleContentSearchRepository.GetArticleSearchResultItems(predicate, articleSearchRequest.Skip, articleSearchRequest.Take, articleSearchRequest.DatabaseName, sort);
        }

        public string GetArticleContent(Guid articleId)
        {
            var content = string.Empty;
            var articleItemId = new Sitecore.Data.ID(articleId);
            var predicate = PredicateBuilder.True<ArticleSearchResultItem>();
            var language = Sitecore.Context.Language?.Name ?? "en";
            predicate = predicate.And(x => x.Language == language);
            predicate = predicate.And(x => x.IsLatestVersion);
            predicate = predicate.And(x => x.ItemId == articleItemId);

            var results = _articleContentSearchRepository.GetArticleSearchResultItems(predicate, 0, 1);
            if (results != null && results.SearchResults != null)
            {
                var resultItem = results.SearchResults.FirstOrDefault();
                if (resultItem != null)
                {
                    content = resultItem.Document.ArticleContent;
                }
            }

            return content;
        }
    }
}
