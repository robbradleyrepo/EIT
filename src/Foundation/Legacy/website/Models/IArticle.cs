﻿namespace LionTrust.Foundation.Legacy.Models
{
    using System;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
   
    public interface IArticle : IHeroHomePageData
    {
        [SitecoreField(Constants.Article.Authors_FieldId, SitecoreFieldType.Treelist, "Article page data")]
        IAuthor Author { get; set; }

        [SitecoreField(Constants.Article.Title_FieldId, SitecoreFieldType.SingleLineText, "Article page data")]
        string Title { get; set; }

        [SitecoreField(Constants.Article.Subtitle_FieldId, SitecoreFieldType.SingleLineText, "Article page data")]
        string Subitle { get; set; }

        [SitecoreField(Constants.Article.Body_FieldId, SitecoreFieldType.RichText, "Article page data")]
        string Body { get; set; }

        [SitecoreField(Constants.Article.Image_FieldId, SitecoreFieldType.Image, "Article page data")]
        Image Image { get; set; }

        [SitecoreField(Constants.Article.PromoType_FieldId, SitecoreFieldType.Droplink, "Article page data")]
        ILegacyGlassBase PromoType { get; set; }

        [SitecoreField(Constants.Article.Date_FieldId, SitecoreFieldType.Date, "Article page data")]
        DateTime Date { get; set; }

        [SitecoreField(Constants.Article.PublishDate_FieldId, SitecoreFieldType.DateTime, "Article page data")]
        DateTime PublishedDate { get; set; }

        [SitecoreField(Constants.Article.Fund_FieldId, SitecoreFieldType.Treelist, "Article page data")]
        IFundPage Fund { get; set; }
    }
}