﻿namespace LionTrust.Foundation.Search.Repositories.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using LionTrust.Foundation.Search.Models.ContentSearch;

    public interface IArticleContentSearchRepository
    {
        ContentSearchResults<ArticleSearchResultItem> GetArticleSearchResultItems(Expression<Func<ArticleSearchResultItem, bool>> predicate, int skip, int take, string database = "web", Func<IQueryable<ArticleSearchResultItem>, IQueryable<ArticleSearchResultItem>> sort = null);
    }
}