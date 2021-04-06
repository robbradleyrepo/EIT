namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Fields;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IPageLink
    {
        [SitecoreField(Constants.PageLink.Link_FieldName)]
        Link PageLink { get; set; }
    }
}