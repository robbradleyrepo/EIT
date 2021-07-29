namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    public class IMediaItem
    {
        [SitecoreField(Constants.MediaItem.HeadShotImage_FieldId)]
        public Image HeadShotImage { get; set; }
        
        [SitecoreField(Constants.MediaItem.FullBodyImage_FieldId)]
        public Image FullBodyImage { get; set; }

        [SitecoreField(Constants.MediaItem.Description_FieldId)]
        public string Description { get; set; }

        [SitecoreField(Constants.MediaItem.Categories_FieldId)]
        public IEnumerable<IListingsGlassBase> Categories { get; set; }
    }
}