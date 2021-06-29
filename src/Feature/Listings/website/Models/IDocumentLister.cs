namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;

    public interface IDocumentLister : IDocumentFolder, IListingsGlassBase
    {
        [SitecoreField(Constants.DocumentLister.SortText_FieldID)]
        string SortText { get; set; }

        [SitecoreField(Constants.DocumentLister.DateNewestText_FieldID)]
        string DateNewestText { get; set; }

        [SitecoreField(Constants.DocumentLister.DateOldestText_FieldID)]
        string DateOldestText { get; set; }

        [SitecoreField(Constants.DocumentLister.NextText_FieldID)]
        string NextText { get; set; }

        [SitecoreField(Constants.DocumentLister.ViewPageText_FieldID)]
        string ViewPageText { get; set; }

        [SitecoreField(Constants.DocumentLister.WatchVideoText_FieldID]
        string WatchVideoText { get; set; }

        [SitecoreField(Constants.DocumentLister.PreviousText_FieldID)]
        string PreviousText { get; set; }
    }
}