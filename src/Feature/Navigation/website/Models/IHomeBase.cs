namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.Navigation.Models;

    public interface IHomeBase : IIdentity, INavigationGlassBase, IPresentationBase
    {
        [SitecoreField(Constants.NavigationRoot.HeaderConfiguration_FieldID, SitecoreFieldType.Droplink, "Header")]
        IHeaderConfiguration HeaderConfiguration { get; set; }

        [SitecoreField(Constants.NavigationRootBase.FooterConfiguration_FieldId, SitecoreFieldType.Droplink, "Footer")]
        IFooterConfiguration FooterConfiguration { get; set; }
    }
}