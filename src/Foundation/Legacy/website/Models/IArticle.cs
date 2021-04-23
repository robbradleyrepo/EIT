namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System;

    public interface IArticle : ILegacyGlassBase
    {
        [SitecoreField(Constants.IArticle.Title_FieldId, SitecoreFieldType.SingleLineText, "Article page data")]
        string Title { get; set; }

        [SitecoreField(Constants.IArticle.Subtitle_FieldId, SitecoreFieldType.SingleLineText, "Article page data")]
        string Subitle { get; set; }

        [SitecoreField(Constants.IArticle.Body_FieldId, SitecoreFieldType.RichText, "Article page data")]
        string Body { get; set; }

        [SitecoreField(Constants.IArticle.Image_FieldId, SitecoreFieldType.Image, "Article page data")]
        Image Image { get; set; }

        [SitecoreField(Constants.IArticle.ArticleType_FieldId, SitecoreFieldType.Droplink, "Article page data")]
        IArticleType ArticleType { get; set; }

        [SitecoreField(Constants.IArticle.Date_FieldId, SitecoreFieldType.Date, "Article page data")]
        DateTime Date { get; set; }

        [SitecoreField(Constants.IArticle.PublishDate_FieldId, SitecoreFieldType.DateTime, "Article page data")]
        DateTime PublishedDate { get; set; }

        [SitecoreField(Constants.HeroImagePageData.HeaderImage_FieldId, SitecoreFieldType.Image, "Article page data")]
        Image HeaderImage { get; set; }
    }
}