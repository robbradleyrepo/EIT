namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IAuthor : ILegacyGlassBase
    {
        [SitecoreField(Constants.IAuthor.FullName_FieldId, SitecoreFieldType.SingleLineText, "Personal details")]
        string FullName { get; set; }

        [SitecoreField(Constants.IAuthor.Image_FieldId, SitecoreFieldType.Image, "Personal details")]
        Image Image { get; set; }

        [SitecoreField(Constants.IAuthor.Telephone_FieldId, SitecoreFieldType.SingleLineText, "Personal details")]
        string Telephone { get; set; }

        [SitecoreField(Constants.IAuthor.Mobile_FieldId, SitecoreFieldType.SingleLineText, "Personal details")]
        string Mobile { get; set; }

        [SitecoreField(Constants.IAuthor.Email_FieldId, SitecoreFieldType.SingleLineText, "Personal details")]
        string Email { get; set; }

        [SitecoreField(Constants.IAuthor.JobTitle_FieldId, SitecoreFieldType.SingleLineText, "Additional details")]
        Image JobTitle { get; set; }

        [SitecoreField(Constants.IAuthor.ShortBio_FieldId, SitecoreFieldType.RichText, "Additional details")]
        string ShortBio { get; set; }
    }
}