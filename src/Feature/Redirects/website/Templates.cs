using System;

namespace LionTrust.Feature.Redirects
{
    /// <summary>
    /// https://staticreadonly.com/2019/02/06/structures-vs-static-classes-for-sitecore-template-references/
    /// </summary>
    public static class Templates
    {
        public static class GlobalFolder
        {
            public static readonly Guid TemplateId = new Guid("{B2076F9F-E735-4F31-91CF-597524B76A1E}");
        }
        public static class RedirectFolder
        {
            public static readonly Guid TemplateId = new Guid("{06C2205B-CECB-4622-9680-E61E5DF1CB05}");
        }
        public static class RedirectContentItem
        {
            public const string RedirectContentItemId = "{E85BB7D7-85D1-403E-A178-D0988966583F}";
            public static readonly Guid TemplateId = new Guid(RedirectContentItemId);
            public const string RequestedUrlFieldId = "{CFAF69F2-4A0E-42E8-A90E-2A8D3EE59A2D}";
            public const string RedirectItemFieldId = "{8F536075-8222-4A90-BB51-A36797B24129}";
        }
        public static class ErrorMessages
        {
            public const string NoUrlOnItem = "Could not find a URL value on the redirect item";
            public const string NoRedirectFolder = "Redirect folder not found";
        }
    }
}
