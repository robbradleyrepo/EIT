namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IFooterConfiguration : INavigationGlassBase
    {
        [SitecoreField(Constants.FooterConfiguration.FindUsLabel_FieldName)]
        string FindUsLabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Address_FieldName)]
        string Address { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Location_FieldName)]
        string Location { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PostalCode_FieldName)]
        string PostalCode { get; set; }

        [SitecoreField(Constants.FooterConfiguration.GoogleMapsLink_FieldName)]
        Link GoogleMapsLink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.GoogleMapsCTALabel_FieldName)]
        string GoogleMapsCTALabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.GetInTouchLabel_FieldName)]
        string GetInTouchLabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PhoneNumber_FieldName)]
        string PhoneNumber { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Email_FieldName)]
        string Email { get; set; }

        [SitecoreField(Constants.FooterConfiguration.EmailCTALabel_FieldName)]
        string EmailCTALabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SubscribeNewsletterLabel_FieldName)]
        string SubscribeNewsletterLabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SubscribeCTALabel_FieldName)]
        string SubscribeCTALabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SubscribeCTALink_FieldName)]
        Link SubscribeCTALink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Copyright_FieldName)]
        string Copyright { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PageLinks_FieldName)]
        IEnumerable<IPageLink> PageLink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SocialLinks_FieldName)]
        IEnumerable<ISocialIcon> SocialIcons { get; set; }
    }
}