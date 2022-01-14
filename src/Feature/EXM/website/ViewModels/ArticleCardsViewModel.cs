using Glass.Mapper.Sc.Fields;
using LionTrust.Feature.EXM.Models;
using LionTrust.Foundation.Search.Models.ContentSearch;
using System.Collections.Generic;

namespace LionTrust.Feature.EXM.ViewModels
{
    public class ArticleCardsViewModel
    {
        public IArticleCards ArticleCards { get; set; }

        public List<ArticleSearchResultItem> Articles { get; set; }
    }
}