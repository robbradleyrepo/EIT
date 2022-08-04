namespace LionTrust.Feature.Video.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Design;
    using LionTrust.Foundation.Video;

    public interface IVideoWithCopy: IVideoModel
    {
        [SitecoreField(Video.Constants.VideoWithCopy.HeadingFieldId)]
        string Heading { get; set; }

        [SitecoreField(Video.Constants.VideoWithCopy.CopyFieldId)]
        string Copy { get; set; }

        [SitecoreField(Video.Constants.VideoWithCopy.SubheadingFieldId)]
        string Subheading { get; set; }

        [SitecoreField(Video.Constants.VideoWithCopy.TextAlignmentFieldId)]
        ILookupValue TextAlignment { get; set; }
    }
}
