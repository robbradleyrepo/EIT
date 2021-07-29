namespace LionTrust.Feature.Listings.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IMediaGallery : IListingsGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IMediaItem> MediaItems { get; set; }

        [SitecoreField(Constants.MediaLister.SearchByKeywordLabel_FieldId)]
        public string SearchByKeywordLabel { get; set; }

        [SitecoreField(Constants.MediaLister.FilterByCategoryLabel_FieldId)]
        public string FilterByCategoryLabel { get; set; }

        [SitecoreField(Constants.MediaLister.DownloadSelectedLabel_FieldId)]
        public string DownloadSelectedLabel { get; set; }

        [SitecoreField(Constants.MediaLister.SelectAllLabel_FieldId)]
        public string SelectAllLabel { get; set; }

        [SitecoreField(Constants.MediaLister.SelectLabel_FieldId)]
        public string SelectLabel { get; set; }

        [SitecoreField(Constants.MediaLister.HeadshotLabel_FieldId)]
        public string HeadshotLabel { get; set; }

        [SitecoreField(Constants.MediaLister.FullBodyLabel_FieldId)]
        public string FullBodyLabel { get; set; }
    }
}