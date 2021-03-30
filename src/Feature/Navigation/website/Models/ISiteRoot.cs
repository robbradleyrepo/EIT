namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface ISiteRoot : INavigationGlassBase
    {
        [SitecoreField(Constants.Identity.Logo_FieldName)]
        Image Logo { get; set; }
    }
}