namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IFooterConfiguration : INavigationGlassBase
    {
        [SitecoreField(Constants.FooterConfiguration.FindUsLabel_FieldID, SitecoreFieldType.SingleLineText, "Footer")]
        string FindUsLabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Address_FieldName, SitecoreFieldType.SingleLineText, "Footer")]
        string Address { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Location_FieldName, SitecoreFieldType.SingleLineText, "Footer")]
        string Location { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PostalCode_FieldName, SitecoreFieldType.SingleLineText, "Footer")]
        string PostalCode { get; set; }

        [SitecoreField(Constants.FooterConfiguration.GoogleMapsLink_FieldName, SitecoreFieldType.GeneralLink, "Footer")]
        Link GoogleMapsLink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.GoogleMapsCTALabel_FieldName, SitecoreFieldType.SingleLineText, "Footer")]
        string GoogleMapsCTALabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.GetInTouchLabel_FieldName, SitecoreFieldType.SingleLineText, "Footer")]
        string GetInTouchLabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PhoneNumber_FieldName, SitecoreFieldType.SingleLineText, "Footer")]
        string PhoneNumber { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Email_FieldName, SitecoreFieldType.SingleLineText, "Footer")]
        string Email { get; set; }

        [SitecoreField(Constants.FooterConfiguration.EmailCTALabel_FieldName, SitecoreFieldType.SingleLineText, "Footer")]
        string EmailCTALabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SubscribeNewsletterLabel_FieldName, SitecoreFieldType.SingleLineText, "Footer")]
        string SubscribeNewsletterLabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SubscribeCTALabel_FieldName, SitecoreFieldType.SingleLineText, "Footer")]
        string SubscribeCTALabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SubscribeCTALink_FieldName, SitecoreFieldType.GeneralLink, "Footer")]
        Link SubscribeCTALink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Copyright_FieldName, SitecoreFieldType.SingleLineText, "Footer")]
        string Copyright { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PageLinks_FieldName, SitecoreFieldType.Treelist, "Footer")]
        IEnumerable<IPageLink> PageLink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SocialLinks_FieldName, SitecoreFieldType.Treelist, "Footer")]
        IEnumerable<ISocialIcon> SocialIcons { get; set; }
    }
}