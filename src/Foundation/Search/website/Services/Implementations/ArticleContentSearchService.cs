namespace LionTrust.Foundation.Search.Services.Implementations
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Repositories.Interfaces;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.ContentSearch.Linq.Utilities;

    public class ArticleContentSearchService : IArticleContentSearchService
    {
        private readonly IArticleContentSearchRepository _articleContentSearchRepository;

        public ArticleContentSearchService(IArticleContentSearchRepository articleContentSearchRepository)
        {
            _articleContentSearchRepository = articleContentSearchRepository;
        }

        private Expression<Func<ArticleSearchResultItem, bool>> PopoulateDatedTaxonomyPredicate(Expression<Func<ArticleSearchResultItem, bool>> predicate, ITaxonomySearchRequest articleSearchRequest)
        {
            predicate = predicate.And(x => x.ArticleDate > articleSearchRequest.FromDate);
            predicate = predicate.And(x => x.ArticleDate < articleSearchRequest.ToDate);

            var taxonomyFilter = PredicateBuilder.True<ArticleSearchResultItem>();

            if (articleSearchRequest.FundCategories != null && articleSearchRequest.FundCategories.Any())
            {
                var fundCategoryPredicate = PredicateBuilder.False<ArticleSearchResultItem>();
                fundCategoryPredicate = articleSearchRequest.FundCategories.Aggregate(fundCategoryPredicate,
                                                                                            (current, category) => current
                                                                                                                    .Or(item => item.ArticleCategory == category));

                taxonomyFilter = taxonomyFilter.And(fundCategoryPredicate);
            }

            if (articleSearchRequest.Funds != null && articleSearchRequest.Funds.Any())
            {
                var fundPredicate = PredicateBuilder.False<ArticleSearchResultItem>();
                fundPredicate = articleSearchRequest.Funds.Aggregate(fundPredicate,
                                                                          (current, category) => current
                                                                                                  .Or(item => item.ArticleFund == category));

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


        public ContentSearchResults GetDatedTaxonomyRelatedArticles(ITaxonomySearchRequest articleSearchRequest)
        {
            var predicate = PredicateBuilder.True<ArticleSearchResultItem>();
            var language = Sitecore.Context.Language?.Name ?? "en";
            predicate = predicate.And(x => x.Language == language);

            predicate = this.PopoulateDatedTaxonomyPredicate(predicate, articleSearchRequest);

            return _articleContentSearchRepository.GetArticleSearchResultItems(predicate, articleSearchRequest.Skip, articleSearchRequest.Take);
        }
    }
}
