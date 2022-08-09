namespace LionTrust.Foundation.Video
{
    using Foundation.Design;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;
    using System;

    public interface IVideoModel: IGlassBase
    {
        [SitecoreField(Constants.Video.ThumbnailFieldId)]
        Image Thumbnail { get; set; }

        [SitecoreField(Constants.Video.AuthorFieldId)]
        string Author { get; set; }

        [SitecoreField(Constants.Video.TitleFieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.Video.VideoIdFieldId)]
        string VideoId { get; set; }

        [SitecoreField(Constants.Video.MarginsContainerFieldId)]
        ILookupValue MarginsContainer { get; set; }

        [SitecoreField(Constants.Video.PlayGoalFieldId)]
        Guid PlayGoal { get; set; }
    }
}
