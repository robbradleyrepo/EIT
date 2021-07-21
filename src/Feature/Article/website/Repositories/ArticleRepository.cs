namespace LionTrust.Feature.Article.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Services.Interfaces;

    public class ArticleRepository
    {
        private readonly IArticleContentSearchService _searchService;
        private readonly IMvcContext _mvcContext;

        public ArticleRepository(IArticleContentSearchService searchService, IMvcContext mvcContext)
        {
            this._searchService = searchService;
            this._mvcContext = mvcContext;
        }

        public IEnumerable<IArticlePromo> GetArticlePromosByTopics(IEnumerable<Guid> topics, IEnumerable<string> fundManagers = null)
        {
            var request = new ArticleSearchRequest
            {
                Topics = topics,
                FromDate = DateTime.MinValue,
                ToDate = DateTime.MaxValue,
                Take = int.MaxValue,
                DatabaseName = _mvcContext.SitecoreService.Database.Name,
                FundManagers = fundManagers
            };

            var results = _searchService.GetDatedTaxonomyRelatedArticles(request, result => result.OrderByDescending(hit => hit.Created));
            if (results == null || results.SearchResults == null)
            {
                return null;
            }

            return results.SearchResults
                .Where(sr => sr.Document != null)
                .Select(sr => BuildArticle(sr.Document));
        }

        public IEnumerable<IArticlePromo> Map(IEnumerable<Guid> funds, IEnumerable<Guid> categories, IEnumerable<Guid> fundTeams, IEnumerable<Guid> fundManagers, IEnumerable<Guid> topics, string databaseName)
        {
            var request = new ArticleSearchRequest
            {
                Topics = topics,
                Funds = funds?.Select(f => f.ToString().Replace("-", string.Empty)),
                Categories = categories?.Select(fc => fc.ToString().Replace("-", string.Empty)),
                FundTeams = fundTeams?.Select(ft => ft.ToString().Replace("-", string.Empty)),
                FundManagers = fundManagers?.Select(fm => fm.ToString().Replace("-", string.Empty)),
                Take = 6,
                DatabaseName = databaseName,
                FromDate = DateTime.MinValue,
                ToDate = DateTime.MaxValue
            };

            var results = _searchService.GetDatedTaxonomyRelatedArticles(request, result => result.OrderByDescending(hit => hit.Created));

            if (results == null || results.SearchResults == null)
            {
                return new IArticlePromo[0];
            }

            return results.SearchResults
                .Where(sr => sr.Document != null)
                .Select(sr => BuildArticle(sr.Document));
        }

        public IEnumerable<IArticlePromo> Map(IArticleFilter filter, string databaseName)
        {
            return Map(filter.Funds?.Select(f => f.Id), filter.ContentTypes?.Select(fc => fc.Id), filter.FundTeams?.Select(ft => ft.Id), filter.FundManagers?.Select(fm => fm.Id), filter.Topics?.Select(t => t.Id), databaseName);
        }

        private IArticlePromo BuildArticle(ArticleSearchResultItem hit)
        {
            if (hit == null)
            {
                return null;
            }

            var item = hit.GetItem();
            if (item == null)
            {
                return null;
            }

            return _mvcContext.SitecoreService.GetItem<IArticlePromo>(item.ID.Guid);
        }
    }
}