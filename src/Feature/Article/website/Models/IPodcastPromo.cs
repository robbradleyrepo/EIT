namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.PodcastPromo.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface IPodcastPromo : IArticleGlassBase
    {
        [SitecoreField(Constants.PodcastPromo.Heading_FieldId, SitecoreFieldType.SingleLineText, "Podcast Promo")]
        string Heading { get; set; }

        [SitecoreField(Constants.PodcastPromo.Title_FieldId, SitecoreFieldType.SingleLineText, "Podcast Promo")]
        string Title { get; set; }

        [SitecoreField(Constants.PodcastPromo.Body_FieldId, SitecoreFieldType.MultiLineText, "Podcast Promo")]
        string Body { get; set; }

        [SitecoreField(Constants.PodcastPromo.BackgroundImage_FieldId, SitecoreFieldType.Image, "Podcast Promo")]
        Image BackgroundImage { get; set; }

        [SitecoreField(Constants.PodcastPromo.MobileBackgroundImageFieldId)]
        Image MobileBackgroundImage { get; set; }


        [SitecoreField(Constants.PodcastPromo.PodcastsLabel_FieldId, SitecoreFieldType.SingleLineText, "Podcast Promo")]
        string PodcastsLabel { get; set; }
        
        [SitecoreChildren]
        IEnumerable<IPodcastLink> PodcastLinks { get; set; }
    }
}