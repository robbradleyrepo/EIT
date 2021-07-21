namespace LionTrust.Feature.Search.SiteSearch
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Search.Models;
    using System.Collections.Generic;

    public interface ISiteSearch: IPagination
    {
        [SitecoreField(Constants.SiteSearch.SearchResultsFoundLabelFieldId)]
        string SearchResultsFoundCopy { get; set; }

        [SitecoreField(Constants.SiteSearch.ResultsPerPageFieldId)]
        string ResultsPerPage { get; set; }
        
        [SitecoreField(Constants.SiteSearch.SearchLabelFieldId)]
        string SearchLabel { get; set; }
        
        [SitecoreField(Constants.SiteSearch.HeadingFieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.SiteSearch.ViewAllFilterTextFieldId)]
        string ViewAllFilterText { get; set; }

        [SitecoreField(Constants.SiteSearch.FactsheetLinkTextFieldId)]
        string FactsheetLinkText { get; set; }

        [SitecoreChildren(TemplateId = Constants.SiteSearchFilter.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<ISiteSearchFilter> Filters { get; set; }
    }
}
