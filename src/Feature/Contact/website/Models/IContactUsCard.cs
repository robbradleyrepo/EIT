namespace LionTrust.Feature.Contact.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System.Collections.Generic;

    public interface IContactUsCard : IContactGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IContactCard> ContactCards { get; set; }

        [SitecoreField(Constants.ContactUsCard.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.ContactUsCard.TitleColor_FieldId)]
        Foundation.Design.ILookupValue TitleColor { get; set; }
    }
}