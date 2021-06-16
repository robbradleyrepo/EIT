namespace LionTrust.Feature.Contact.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IContactUsForm : IContactGlassBase
    {
        [SitecoreField(Constants.ContactUsForm.Heading_FieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.ContactUsForm.Description_FieldId)]
        string Description { get; set; }
    }
}