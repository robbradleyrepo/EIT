namespace LionTrust.Feature.Article.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Legacy.Models;

    public interface IArticleScroller : IArticleGlassBase, IArticleFilter
    {
        [SitecoreField(Constants.ArticleScroller.Heading_FieldId, SitecoreFieldType.SingleLineText, "Article Scroller")]
        string Heading { get; set; }

        [SitecoreField(Constants.ArticleScroller.CTA_FieldId, SitecoreFieldType.GeneralLink, "Article Scroller")]
        Link CTA { get; set; }

        [SitecoreField(Constants.ArticleScroller.SelectedArticles_FieldId, SitecoreFieldType.Treelist, "Article Scroller")]
        IEnumerable<IArticlePromo> SelectedArticles { get; set; }
    }
}