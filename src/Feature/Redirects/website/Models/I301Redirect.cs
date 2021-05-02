namespace LionTrust.Feature.Redirects.Models
{
    using Glass.Mapper.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Sitecore.Data.Items;

    [SitecoreType(TemplateId = Templates.RedirectContentItem.RedirectContentItemId)]
    public interface I301Redirect : IRedirectGlassBase
    {
        [SitecoreField(Templates.RedirectContentItem.RequestedUrlFieldId)]
        string RequestedUrl { get; set; }

        [SitecoreField(Templates.RedirectContentItem.RedirectItemFieldId)]
        Item RedirectItem { get; set; }
    }
}
