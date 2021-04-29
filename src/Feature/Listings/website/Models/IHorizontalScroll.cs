namespace LionTrust.Feature.Listings.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IHorizontalScroll : IListingsGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IScrollerCard> ScrollerCards { get; set; }

        [SitecoreField(Constants.HorizontalScroll.Title_FieldID, SitecoreFieldType.SingleLineText, "Content")]
        string Title { get; set; }

        [SitecoreField(Constants.HorizontalScroll.NumberOfItems_FieldID, SitecoreFieldType.Integer, "Content")]
        int NumberOfItems { get; set; }
    }
}