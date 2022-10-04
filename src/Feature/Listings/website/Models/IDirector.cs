namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IDirector : IListingsGlassBase
    {
        [SitecoreField(Constants.Director.FirstName_FieldId)]
        string FirstName { get; set; }

        [SitecoreField(Constants.Director.FullName_FieldId)]
        string FullName { get; set; }

        [SitecoreField(Constants.Director.Role_FieldId)]
        string Role { get; set; }

        [SitecoreField(Constants.Director.ShortBio_FieldId)]
        string ShortBio { get; set; }

        [SitecoreField(Constants.Director.Image_FieldId)]
        Image Image { get; set; }

        [SitecoreField(Constants.Director.Email_FieldId)]
        string Email { get; set; }

        [SitecoreField(Constants.Director.DirectLine_FieldId)]
        string DirectLine { get; set; }

        [SitecoreField(Constants.Director.MobileNumber_FieldId)]
        string MobileNumber { get; set; }

        [SitecoreField(Constants.Director.EmailLabel_FieldId)]
        string EmailLabel { get; set; }

        [SitecoreField(Constants.Director.DirectLineLabel_FieldId)]
        string DirectLineLabel { get; set; }

        [SitecoreField(Constants.Director.MobileLabel_FieldId)]
        string MobileLabel { get; set; }

        [SitecoreField(Constants.Director.LinkedIn_FieldId)]
        string LinkedIn { get; set; }

        [SitecoreField(Constants.Director.SubHeader_FieldId)]
        string SubHeader { get; set; }
    }
}