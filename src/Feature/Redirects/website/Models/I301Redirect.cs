namespace LionTrust.Feature.Redirects.Models
{
    using Glass.Mapper.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Sitecore.Data.Items;

    [SitecoreType(TemplateId = Templates.RedirectContentItem.RedirectContentItemId)]
    public interface I301Redirect : IRedirectGlassBase
    {
        string RequestedUrl { get; set; }
        Item RedirectItem { get; set; }
    }
}
