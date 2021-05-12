namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System;

    public interface IPodcastLink : IArticleGlassBase
    {
        [SitecoreField(Constants.PodcastLink.Link_FieldId, SitecoreFieldType.GeneralLink, "Podcast Link")]
        Link Link { get; set; }

        [SitecoreField(Constants.PodcastLink.LinkGoal_FieldId, SitecoreFieldType.GeneralLink, "Podcast Link")]
        Guid LinkGoal { get; set; }

        [SitecoreField(Constants.PodcastLink.Icon_FieldId, SitecoreFieldType.Image, "Podcast Link")]
        Image Icon { get; set; }
    }
}