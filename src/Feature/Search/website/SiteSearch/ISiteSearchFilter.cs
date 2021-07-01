namespace LionTrust.Feature.Search.SiteSearch
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Search.Models;
    using System;


    public interface ISiteSearchFilter: ISearchGlassBase
    {
        [SitecoreField(Constants.SiteSearchFilter.TemplateFieldId)]
        Guid PageTemplateId { get; set; }

        [SitecoreField(Constants.SiteSearchFilter.PageNameFieldId)]
        string FilterName { get; set; }
    }
}
