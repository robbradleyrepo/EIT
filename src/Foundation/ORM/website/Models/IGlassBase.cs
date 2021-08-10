namespace LionTrust.Foundation.ORM.Models
{
    using System;
    using System.Collections;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Sitecore.Globalization;

    public interface IGlassBase
    {
        [SitecoreId]
        Guid Id { get; set; }

        [SitecoreInfo(SitecoreInfoType.Language)]
        Language Language { get; set; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        int Version { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        string Url { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url, UrlOptions = SitecoreInfoUrlOptions.AlwaysIncludeServerUrl)]
        string AbsoluteUrl { get; set; }

        [SitecoreInfo(SitecoreInfoType.Name)]
        string Name { get; set; }

        [SitecoreInfo(SitecoreInfoType.BaseTemplateIds)]
        IEnumerable BaseTemplateIds { get; set; }

        [SitecoreInfo(SitecoreInfoType.TemplateName)]
        string TemplateName { get; set; }

        [SitecoreInfo(SitecoreInfoType.TemplateId)]
        Guid TemplateId { get; set; }
    }
}
