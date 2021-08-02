namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    public interface IMediaItem : IListingsGlassBase
    {
        [SitecoreField(Constants.MediaItem.HeadshotImage_FieldId)]
        Image HeadshotImage { get; set; }
        
        [SitecoreField(Constants.MediaItem.FullBodyImage_FieldId)]
        Image FullBodyImage { get; set; }

        [SitecoreField(Constants.MediaItem.Description_FieldId)]
        string Description { get; set; }

        [SitecoreField(Constants.MediaItem.Categories_FieldId)]
        IEnumerable<IListingsGlassBase> Categories { get; set; }
    }
}