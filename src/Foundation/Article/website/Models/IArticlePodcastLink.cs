namespace LionTrust.Foundation.Article.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System;

    public interface IArticlePodcastLink : IArticleGlassBase
    {
        [SitecoreField(Constants.ArticlePodcastLink.Link_FieldId, SitecoreFieldType.GeneralLink, "Podcast Link")]
        Link Link { get; set; }

        [SitecoreField(Constants.ArticlePodcastLink.LinkGoal_FieldId, SitecoreFieldType.GeneralLink, "Podcast Link")]
        Guid LinkGoal { get; set; }

        [SitecoreField(Constants.ArticlePodcastLink.Icon_FieldId, SitecoreFieldType.Image, "Podcast Link")]
        Image Icon { get; set; }
    }
}