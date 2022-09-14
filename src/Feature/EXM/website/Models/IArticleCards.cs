namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System.Collections.Generic;
    using LionTrust.Foundation.Article.Models;

    public interface IArticleCards : IExmGlassBase
    {
        [SitecoreField(Constants.ArticleCards.Title_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.ArticleCards.Cards_FieldID)]
        IList<IArticle> Articles { get; set; }

        [SitecoreField(Constants.ArticleCards.ArticleCtaText_FieldID)]
        string CtaText { get; set; }
    }
}