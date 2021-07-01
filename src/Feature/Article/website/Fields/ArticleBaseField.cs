namespace LionTrust.Feature.Article.Fields
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ArticleBaseField
    {
        public bool CanHandle(IEnumerable<Guid> baseTemplateIds)
        {
            return baseTemplateIds.Contains(Constants.ArticlePage.ArticlePageTemplateId);
        }
    }
}