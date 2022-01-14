namespace LionTrust.Foundation.Legacy.Models
{
    using System;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IArticle : IHeroHomePageData
    {
        [SitecoreField(Constants.Article.Authors_FieldId, SitecoreFieldType.Treelist, "Article page data")]
        IEnumerable<IAuthor> Authors { get; set; }

        [SitecoreField(Constants.Article.Title_FieldId, SitecoreFieldType.SingleLineText, "Article page data")]
        string Title { get; set; }

        [SitecoreField(Constants.Article.Subtitle_FieldId, SitecoreFieldType.SingleLineText, "Article page data")]
        string Subtitle { get; set; }

        [SitecoreField(Constants.Article.ShortDescription_FieldId, SitecoreFieldType.SingleLineText, "Article page data")]
        string ShortDescription { get; set; }

        [SitecoreField(Constants.Article.Body_FieldId, SitecoreFieldType.RichText, "Article page data")]
        string Body { get; set; }

        [SitecoreField(Constants.Article.PromoType_FieldId, SitecoreFieldType.Droplink, "Article page data")]
        IContentType PromoType { get; set; }

        [SitecoreField(Constants.Article.PublishDate_FieldId, SitecoreFieldType.DateTime, "Article page data")]
        DateTime PublishedDate { get; set; }

        [SitecoreField(Constants.Article.CreatedDate_FieldId)]
        DateTime CreatedDate { get; set; }
    }
}