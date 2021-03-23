using Sitecore.Data.Items;

namespace LionTrust.Feature.Redirects.Models
{
    public interface I301Redirect : IRedirectGlassBase
    {
        string RequestedUrl { get; set; }
        Item RedirectItem { get; set; }
    }
}
