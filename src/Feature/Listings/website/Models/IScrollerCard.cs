namespace LionTrust.Feature.Listings.Models
{
    using System;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;    

    public interface IScrollerCard : IListingsGlassBase
    {
        [SitecoreField(Constants.ScrollerCard.Title_FieldID, SitecoreFieldType.SingleLineText, "Content")]
        string Title { get; set; }

        [SitecoreField(Constants.ScrollerCard.Description_FieldID, SitecoreFieldType.RichText, "Content")]
        string Description { get; set; }

        [SitecoreField(Constants.ScrollerCard.Link_FieldID, SitecoreFieldType.GeneralLink, "Content")]
        Link Link { get; set; }

        [SitecoreField(Constants.ScrollerCard.LinkGoal_FieldID, SitecoreFieldType.Droplink, "Content")]
        Guid LinkGoal { get; set; }
    }
}