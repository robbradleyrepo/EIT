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

        [SitecoreField(Constants.Article.Body_FieldId, SitecoreFieldType.RichText, "Article page data")]
        string Body { get; set; }

        [SitecoreField(Constants.Article.PromoType_FieldId, SitecoreFieldType.Droplink, "Article page data")]
        IContentType PromoType { get; set; }
        
        [SitecoreField("__Created")]
        DateTime CreatedDate { get; set; }

        [SitecoreField("__Updated")]
        DateTime ModifiedDate { get; set; }
    }
}