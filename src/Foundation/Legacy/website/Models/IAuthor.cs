namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IAuthor : ILegacyGlassBase
    {
        [SitecoreField(Constants.Author.FullName_FieldId, SitecoreFieldType.SingleLineText, "Personal details")]
        string FullName { get; set; }

        [SitecoreField(Constants.Author.Image_FieldId, SitecoreFieldType.Image, "Personal details")]
        Image Image { get; set; }

        [SitecoreField(Constants.Author.Telephone_FieldId, SitecoreFieldType.SingleLineText, "Personal details")]
        string Telephone { get; set; }

        [SitecoreField(Constants.Author.Mobile_FieldId, SitecoreFieldType.SingleLineText, "Personal details")]
        string Mobile { get; set; }

        [SitecoreField(Constants.Author.Email_FieldId, SitecoreFieldType.SingleLineText, "Personal details")]
        string Email { get; set; }

        [SitecoreField(Constants.Author.JobTitle_FieldId, SitecoreFieldType.SingleLineText, "Additional details")]
        Image JobTitle { get; set; }

        [SitecoreField(Constants.Author.ShortBio_FieldId, SitecoreFieldType.RichText, "Additional details")]
        string ShortBio { get; set; }

        [SitecoreField(Constants.Author.TitleFieldId, SitecoreFieldType.RichText, "Additional details")]
        string Title { get; set; }

        [SitecoreField(Constants.Author.PageFieldId, SitecoreFieldType.GeneralLink, "Additional details")]
        Link Page { get; set; }
    }
}