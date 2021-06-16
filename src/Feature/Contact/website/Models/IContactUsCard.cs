namespace LionTrust.Feature.Contact.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IContactUsCard : IContactGlassBase
    {
        [SitecoreField(Constants.ContactUsCard.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.ContactUsCard.Heading_FieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.ContactUsCard.SubHeading_FieldId)]
        string SubHeading { get; set; }

        [SitecoreField(Constants.ContactUsCard.Image_FieldId)]
        Image Image { get; set; }

        [SitecoreField(Constants.ContactUsCard.Street_FieldId)]
        string Street { get; set; }

        [SitecoreField(Constants.ContactUsCard.City_FieldId)]
        string City { get; set; }

        [SitecoreField(Constants.ContactUsCard.PostalCode_FieldId)]
        string PostalCode { get; set; }

        [SitecoreField(Constants.ContactUsCard.TelLabel_FieldId)]
        string TelLabel { get; set; }

        [SitecoreField(Constants.ContactUsCard.TelNumber_FieldId)]
        string TelNumber { get; set; }

        [SitecoreField(Constants.ContactUsCard.OutsideTelNumber_FieldId)]
        string OutsideTelNumber { get; set; }

        [SitecoreField(Constants.ContactUsCard.OutsideLabel_FieldId)]
        string OutsideLabel { get; set; }

        [SitecoreField(Constants.ContactUsCard.FaxLabel_FieldId)]
        string FaxLabel { get; set; }

        [SitecoreField(Constants.ContactUsCard.FaxNumber_FieldId)]
        string FaxNumber { get; set; }

        [SitecoreField(Constants.ContactUsCard.OutsideFaxNumber_FieldId)]
        string OutsideFaxNumber { get; set; }

        [SitecoreField(Constants.ContactUsCard.CTA1Link_FieldId)]
        Link CTA1Link { get; set; }

        [SitecoreField(Constants.ContactUsCard.CTA2Link_FieldId)]
        Link CTA2Link { get; set; }
    }
}