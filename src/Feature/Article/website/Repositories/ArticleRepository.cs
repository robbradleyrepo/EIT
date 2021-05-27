﻿namespace LionTrust.Feature.Article.Repositories
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

        public IEnumerable<IArticlePromo> Map(IArticleFilter filter, string databaseName)
        {
            var request = new ArticleSearchRequest
            {
                Funds = filter.Funds?.Select(f => f.Id.ToString().Replace("-", string.Empty)),
                FundCategories = filter.FundCategories?.Select(fc => fc.Id.ToString().Replace("-", string.Empty)),
                FundTeams = filter.FundTeam?.Select(ft => ft.Id.ToString().Replace("-", string.Empty)),
                FundManagers = filter.FundManagers?.Select(fm => fm.Id.ToString().Replace("-", string.Empty)),
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