namespace LionTrust.Feature.Contact.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IContactCard : IContactGlassBase
    {       
        [SitecoreField(Constants.ContactCard.Heading_FieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.ContactCard.SubHeading_FieldId)]
        string SubHeading { get; set; }

        [SitecoreField(Constants.ContactCard.Image_FieldId)]
        Image Image { get; set; }

        [SitecoreField(Constants.ContactCard.Street_FieldId)]
        string Street { get; set; }

        [SitecoreField(Constants.ContactCard.City_FieldId)]
        string City { get; set; }

        [SitecoreField(Constants.ContactCard.PostalCode_FieldId)]
        string PostalCode { get; set; }

        [SitecoreField(Constants.ContactCard.TelLabel_FieldId)]
        string TelLabel { get; set; }

        [SitecoreField(Constants.ContactCard.TelNumber_FieldId)]
        string TelNumber { get; set; }

        [SitecoreField(Constants.ContactCard.OutsideTelNumber_FieldId)]
        string OutsideTelNumber { get; set; }

        [SitecoreField(Constants.ContactCard.OutsideLabel_FieldId)]
        string OutsideLabel { get; set; }

        [SitecoreField(Constants.ContactCard.FaxLabel_FieldId)]
        string FaxLabel { get; set; }

        [SitecoreField(Constants.ContactCard.FaxNumber_FieldId)]
        string FaxNumber { get; set; }

        [SitecoreField(Constants.ContactCard.OutsideFaxNumber_FieldId)]
        string OutsideFaxNumber { get; set; }

        [SitecoreField(Constants.ContactCard.CTA1Link_FieldId)]
        Link CTA1Link { get; set; }

        [SitecoreField(Constants.ContactCard.CTA2Link_FieldId)]
        Link CTA2Link { get; set; }
    }
}