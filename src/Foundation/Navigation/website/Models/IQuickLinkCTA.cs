namespace LionTrust.Foundation.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;
    using System;

    public interface IQuickLinkCTA : IGlassBase
    {
        [SitecoreField(Constants.QuickLinkCTA.Image_FieldID, SitecoreFieldType.Image, "Content")]
        Image Image { get; set; }

        [SitecoreField(Constants.QuickLinkCTA.Title_FieldID, SitecoreFieldType.SingleLineText, "Content")]
        string Title { get; set; }

        [SitecoreField(Constants.QuickLinkCTA.Description_FieldID, SitecoreFieldType.MultiLineText, "Content")]
        string Description { get; set; }

        [SitecoreField(Constants.QuickLinkCTA.Link_FieldID, SitecoreFieldType.GeneralLink, "Content")]
        Link Link { get; set; }

        [SitecoreField(Constants.QuickLinkCTA.LinkGoal_FieldID, SitecoreFieldType.Droplink, "Content")]
        Guid LinkGoal { get; set; }

        [SitecoreField(Constants.QuickLinkCTA.SmallSize_FieldID, SitecoreFieldType.Checkbox, "Content")]
        bool SmallSize { get; set; }
    }
}