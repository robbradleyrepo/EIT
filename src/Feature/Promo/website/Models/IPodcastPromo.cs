namespace LionTrust.Feature.Promo.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IPodcastPromo : IPromoGlassBase
    {
        [SitecoreField(Constants.PodcastPromo.Heading_FieldId, SitecoreFieldType.SingleLineText, "Podcast Promo")]
        string Heading { get; set; }

        [SitecoreField(Constants.PodcastPromo.Title_FieldId, SitecoreFieldType.SingleLineText, "Podcast Promo")]
        string Title { get; set; }

        [SitecoreField(Constants.PodcastPromo.Body_FieldId, SitecoreFieldType.MultiLineText, "Podcast Promo")]
        string Body { get; set; }

        [SitecoreField(Constants.PodcastPromo.BackgroundImage_FieldId, SitecoreFieldType.Image, "Podcast Promo")]
        Image BackgroundImage { get; set; }

        [SitecoreField(Constants.PodcastPromo.PodcastsLabel_FieldId, SitecoreFieldType.SingleLineText, "Podcast Promo")]
        string PodcastsLabel { get; set; }

        [SitecoreField(Constants.PodcastPromo.ItunesLink_FieldId, SitecoreFieldType.GeneralLink, "Podcast Promo")]
        Link ItunesLink { get; set; }

        [SitecoreField(Constants.PodcastPromo.SpotifyLink_FieldId, SitecoreFieldType.GeneralLink, "Podcast Promo")]
        Link SpotifyLink { get; set; }

        [SitecoreField(Constants.PodcastPromo.PodbeanLink_FieldId, SitecoreFieldType.GeneralLink, "Podcast Promo")]
        Link PodbeanLink { get; set; }        
    }
}