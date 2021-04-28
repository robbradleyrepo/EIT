namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface ISiteRoot : INavigationGlassBase
    {
        [SitecoreField(Constants.Identity.Logo_FieldID, SitecoreFieldType.Image, "Identity")]
        Image Logo { get; set; }
    }
}