namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeroHomePageData : IPresentationBase
    {
        [SitecoreField(Constants.HeroImagePageData.HeaderImage_FieldId, SitecoreFieldType.Image, "Page standard data")]
        Image HeaderImage { get; set; }
    }
}