namespace LionTrust.Feature.Listings.Models.MediaGallery
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IMediaGallery : IListingsGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IMediaItem> MediaItems { get; set; }

        [SitecoreField(Constants.MediaLister.SearchByKeywordLabel_FieldId)]
        string SearchByKeywordLabel { get; set; }

        [SitecoreField(Constants.MediaLister.FilterByCategoryLabel_FieldId)]
        string FilterByCategoryLabel { get; set; }

        [SitecoreField(Constants.MediaLister.DownloadSelectedLabel_FieldId)]
        string DownloadSelectedLabel { get; set; }

        [SitecoreField(Constants.MediaLister.SelectAllLabel_FieldId)]
        string SelectAllLabel { get; set; }

        [SitecoreField(Constants.MediaLister.SelectLabel_FieldId)]
        string SelectLabel { get; set; }

        [SitecoreField(Constants.MediaLister.HeadshotLabel_FieldId)]
        string HeadshotLabel { get; set; }

        [SitecoreField(Constants.MediaLister.FullBodyLabel_FieldId)]
        string FullBodyLabel { get; set; }

        [SitecoreField(Constants.MediaLister.DownloadLabel_FieldId)]
        string DownloadLabel { get; set; }

        [SitecoreField(Constants.MediaLister.FilterSource_FieldId)]
        IFilterCategory FilterCategoryFolder { get; set; }
    }
}