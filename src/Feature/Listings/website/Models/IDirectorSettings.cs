namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IDirectorSettings : IListingsGlassBase
    {
        [SitecoreField(Constants.DirectorSettings.Header_FieldId)]
        string Header { get; set; }

        [SitecoreField(Constants.DirectorSettings.LinkedInLabel_FieldId)]
        string LinkedInLabel { get; set; }

        [SitecoreField(Constants.DirectorSettings.LinkedInImage_FieldId)]
        Image LinkedInImage { get; set; }

        [SitecoreField(Constants.DirectorSettings.ViewMoreLabel_FieldId)]
        string ViewMoreLabel { get; set; }

        [SitecoreField(Constants.DirectorSettings.EmailLabel_FieldId)]
        string EmailLabel { get; set; }

        [SitecoreField(Constants.DirectorSettings.MobileLabel_FieldId)]
        string MobileLabel { get; set; }

        [SitecoreField(Constants.DirectorSettings.DirectLineLabel_FieldId)]
        string DirectLineLabel { get; set; }
    }
}