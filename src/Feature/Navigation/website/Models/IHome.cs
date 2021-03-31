namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    public interface IHome : INavigationGlassBase
    {
        [SitecoreField(Constants.Header.HeaderMenuItems_FieldName)]
        IEnumerable<INavigationGlassBase> HeaderMenuItems { get; set; }

        [SitecoreField(Constants.Header.ContactUsPage_FieldName)]
        INavigationGlassBase ContactUsPage { get; set; }

        [SitecoreField(Constants.Menu.MyLionTrust_FieldName)]
        INavigationGlassBase MyLionTrust { get; set; }

        [SitecoreField(Constants.Menu.MyPreferences_FieldName)]
        INavigationGlassBase MyPreferences { get; set; }

        [SitecoreField(Constants.Menu.SignUpNewsletter_FieldName)]
        INavigationGlassBase SignupNewsLetter { get; set; }

        [SitecoreField(Constants.Menu.YouAreViewingLabel_FieldName)]
        string YouAreViewingLabel { get; set; }

        [SitecoreField(Constants.Menu.ChangeLabel_FieldName)]
        string ChangeLabel { get; set; }
        
        [SitecoreField(Constants.Menu.MenuItems_FieldName)]
        IEnumerable<INavigationGlassBase> MenuItems { get; set; }

        [SitecoreField(Constants.Footer.FindUsLabel_FieldName)]
        string FindUsLabel { get; set; }

        [SitecoreField(Constants.Footer.Address_FieldName)]
        string Address { get; set; }

        [SitecoreField(Constants.Footer.Location_FieldName)]
        string Location { get; set; }

        [SitecoreField(Constants.Footer.PostalCode_FieldName)]
        string PostalCode { get; set; }

        [SitecoreField(Constants.Footer.GoogleMapsLink_FieldName)]
        Link GoogleMapsLink { get; set; }

        [SitecoreField(Constants.Footer.GoogleMapsCTALabel_FieldName)]
        string GoogleMapsCTALabel { get; set; }

        [SitecoreField(Constants.Footer.GetInTouchLabel_FieldName)]
        string GetInTouchLabel { get; set; }

        [SitecoreField(Constants.Footer.PhoneNumber_FieldName)]
        string PhoneNumber { get; set; }        

        [SitecoreField(Constants.Footer.Email_FieldName)]
        string Email { get; set; }

        [SitecoreField(Constants.Footer.EmailCTALabel_FieldName)]
        string EmailCTALabel { get; set; }

        [SitecoreField(Constants.Footer.TwitterAccounts_FieldName)]
        string TwitterAccounts { get; set; }

        [SitecoreField(Constants.Footer.TwitterIcon_FieldName)]
        Image TwitterIcon { get; set; }

        [SitecoreField(Constants.Footer.LinkedinLink_FieldName)]
        Link LinkedinLink { get; set; }

        [SitecoreField(Constants.Footer.LinkedinIcon_FieldName)]
        Image LinkedinIcon { get; set; }

        [SitecoreField(Constants.Footer.FacebookLink_FieldName)]
        Link FacebookLink { get; set; }

        [SitecoreField(Constants.Footer.FacebookIcon_FieldName)]
        Image FacebookIcon { get; set; }

        [SitecoreField(Constants.Footer.SubscribeNewsletterLabel_FieldName)]
        string SubscribeNewsletterLabel { get; set; }

        [SitecoreField(Constants.Footer.SubscribeCTALabel_FieldName)]
        string SubscribeCTALabel { get; set; }

        [SitecoreField(Constants.Footer.SubscribeCTALink_FieldName)]
        Link SubscribeCTALink_FieldName { get; set; }

        [SitecoreField(Constants.Footer.FooterLinks_FieldName)]
        IEnumerable<INavigationGlassBase> FooterLinks { get; set; }

        [SitecoreField(Constants.Footer.Copyright_FieldName)]
        string Copyright { get; set; }
    }
}