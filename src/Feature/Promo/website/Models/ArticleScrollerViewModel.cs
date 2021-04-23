namespace LionTrust.Feature.Promo.Models
{
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    public class ArticleScrollerViewModel
    {
        public IArticleScroller ArticleScroller { get; set; }
        public IEnumerable<IArticle> ArticleList { get; set; }
    }
}