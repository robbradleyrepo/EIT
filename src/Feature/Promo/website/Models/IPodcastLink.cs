namespace LionTrust.Feature.Promo.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IPodcastLink : IPromoGlassBase
    {
        [SitecoreField(Constants.PodcastLink.Link_FieldId, SitecoreFieldType.GeneralLink, "Podcast Link")]
        Link Link { get; set; }

        [SitecoreField(Constants.PodcastLink.Icon_FieldId, SitecoreFieldType.Image, "Podcast Link")]
        Image Icon { get; set; }
    }
}