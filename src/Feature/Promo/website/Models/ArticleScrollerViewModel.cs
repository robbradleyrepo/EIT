namespace LionTrust.Feature.Promo.Models
{
    using System.Collections.Generic;

    public class ArticleScrollerViewModel
    {
        public IArticleScroller ArticleScroller { get; set; }
        public IEnumerable<IArticlePromo> ArticleList { get; set; }
    }
}