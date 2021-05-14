namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.ArticlePodcastPromo.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface IArticlePodcastPromo : IArticleGlassBase
    {
        [SitecoreField(Constants.ArticlePodcastPromo.Heading_FieldId, SitecoreFieldType.SingleLineText, "Podcast Promo")]
        string Heading { get; set; }

        [SitecoreField(Constants.ArticlePodcastPromo.Title_FieldId, SitecoreFieldType.SingleLineText, "Podcast Promo")]
        string Title { get; set; }

        [SitecoreField(Constants.ArticlePodcastPromo.Body_FieldId, SitecoreFieldType.MultiLineText, "Podcast Promo")]
        string Body { get; set; }

        [SitecoreField(Constants.ArticlePodcastPromo.BackgroundImage_FieldId, SitecoreFieldType.Image, "Podcast Promo")]
        Image BackgroundImage { get; set; }

        [SitecoreField(Constants.ArticlePodcastPromo.MobileBackgroundImageFieldId)]
        Image MobileBackgroundImage { get; set; }


        [SitecoreField(Constants.ArticlePodcastPromo.PodcastsLabel_FieldId, SitecoreFieldType.SingleLineText, "Podcast Promo")]
        string PodcastsLabel { get; set; }
        
        [SitecoreChildren]
        IEnumerable<IArticlePodcastLink> PodcastLinks { get; set; }
    }
}