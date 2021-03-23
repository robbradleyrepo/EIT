namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ISiteRoot : INavigationGlassBase
    {
        [SitecoreField(Constants.Identity.ContactUsPage_FieldName)]
        public INavigationGlassBase ContactUsPage { get; set; }
    }
}