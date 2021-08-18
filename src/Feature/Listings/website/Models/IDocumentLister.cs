namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;

    public interface IDocumentLister : IDocumentFolder, IListingsGlassBase
    {
        [SitecoreField(Constants.DocumentLister.TitleColor_FieldID)]
        Foundation.Design.ILookupValue TitleColor { get; set; }

        [SitecoreField(Constants.DocumentLister.SortText_FieldID)]
        string SortText { get; set; }

        [SitecoreField(Constants.DocumentLister.AZText_FieldID)]
        string AZText { get; set; }

        [SitecoreField(Constants.DocumentLister.ZAText_FieldID)]
        string ZAText { get; set; }

        [SitecoreField(Constants.DocumentLister.NextText_FieldID)]
        string NextText { get; set; }

        [SitecoreField(Constants.DocumentLister.ViewPageText_FieldID)]
        string ViewPageText { get; set; }

        [SitecoreField(Constants.DocumentLister.WatchVideoText_FieldID)]
        string WatchVideoText { get; set; }

        [SitecoreField(Constants.DocumentLister.PreviousText_FieldID)]
        string PreviousText { get; set; }

        [SitecoreField(Constants.DocumentLister.DownloadFileLabel_FieldID)]
        string DownloadFileLabel { get; set; }
    }
}