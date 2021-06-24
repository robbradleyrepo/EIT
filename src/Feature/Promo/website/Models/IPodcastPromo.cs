namespace LionTrust.Feature.Promo.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.PodcastPromo.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface IPodcastPromo : IPromoGlassBase
    {
        [SitecoreField(Constants.PodcastPromo.Heading_FieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.PodcastPromo.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.PodcastPromo.Body_FieldId)]
        string Body { get; set; }

        [SitecoreField(Constants.PodcastPromo.BackgroundImage_FieldId)]
        Image BackgroundImage { get; set; }

        [SitecoreField(Constants.PodcastPromo.BackgroundImageOpacity_FieldId)]
        string BackgroundImageOpacity { get; set; }

        [SitecoreField(Constants.PodcastPromo.MobileBackgroundImageFieldId)]
        Image MobileBackgroundImage { get; set; }

        [SitecoreField(Constants.PodcastPromo.PodcastsLabel_FieldId)]
        string PodcastsLabel { get; set; }
        
        [SitecoreChildren]
        IEnumerable<IPodcastLink> PodcastLinks { get; set; }
    }
}