namespace LionTrust.Feature.EXM.Repositories.Interfaces
{
    using System.Collections.Generic;
    using LionTrust.Foundation.Search.Models.ContentSearch;

    public interface IArticleRepository
    {
        IEnumerable<ArticleSearchResultItem> GetLatestArticles(int limit);
    }
}