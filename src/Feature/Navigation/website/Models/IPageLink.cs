namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Fields;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Configuration;

    public interface IPageLink : INavigationGlassBase
    {
        [SitecoreField(Constants.PageLink.Link_FieldID, SitecoreFieldType.GeneralLink, "Content")]
        Link PageLink { get; set; }
    }
}