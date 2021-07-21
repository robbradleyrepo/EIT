namespace LionTrust.Feature.Search.SiteSearch
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Search.Models;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.SiteSearchFilter.TemplateId)]
    public interface ISiteSearchFilter: ISearchGlassBase
    {
        [SitecoreField(Constants.SiteSearchFilter.TemplateFieldId)]
        IEnumerable<IGlassBase> PageTemplates { get; set; }

        [SitecoreField(Constants.SiteSearchFilter.PageNameFieldId)]
        string FilterName { get; set; }
    }
}
