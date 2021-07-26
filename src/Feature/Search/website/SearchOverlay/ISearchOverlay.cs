namespace LionTrust.Feature.Search.SearchOverlay
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Search.Models;
    using LionTrust.Foundation.ORM.Models;

    public interface ISearchOverlay: ISearchGlassBase
    {
        [SitecoreField(Constants.SearchOverlay.PlaceholdertextFieldId)]
        string Placeholdertext { get; set; }

        [SitecoreField(Constants.SearchOverlay.SearchResultsPageFieldId)]
        IGlassBase SearchResultsPage { get; set; }
    }
}
