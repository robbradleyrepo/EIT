namespace LionTrust.Feature.Search.SearchOverlay
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Search.Models;
    using LionTrust.Foundation.ORM.Models;
    using System;

    public interface ISearchOverlay: ISearchGlassBase
    {
        [SitecoreField(Constants.SearchOverlay.PlaceholdertextFieldId)]
        string Placeholdertext { get; set; }

        [SitecoreField(Constants.SearchOverlay.SearchResultsPageFieldId)]
        IGlassBase SearchResultsPage { get; set; }

        [SitecoreField(Constants.SearchOverlay.SearchGoalFieldId)]
        Guid SearchGoal { get; set; }

        [SitecoreField(Constants.SearchOverlay.RecentSearchesLabelFieldId)]
        string RecentSearchesLabel { get; set; }
    }
}
