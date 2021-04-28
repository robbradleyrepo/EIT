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

        [SitecoreField(Constants.FooterConfiguration.Address_FieldID, SitecoreFieldType.SingleLineText, "Footer")]
        string Address { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Location_FieldID, SitecoreFieldType.SingleLineText, "Footer")]
        string Location { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PostalCode_FieldID, SitecoreFieldType.SingleLineText, "Footer")]
        string PostalCode { get; set; }

        [SitecoreField(Constants.FooterConfiguration.GoogleMapsLink_FieldID, SitecoreFieldType.GeneralLink, "Footer")]
        Link GoogleMapsLink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.GetInTouchLabel_FieldID, SitecoreFieldType.SingleLineText, "Footer")]
        string GetInTouchLabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PhoneNumber_FieldID, SitecoreFieldType.SingleLineText, "Footer")]
        string PhoneNumber { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Email_FieldID, SitecoreFieldType.SingleLineText, "Footer")]
        string Email { get; set; }

        [SitecoreField(Constants.FooterConfiguration.EmailCTALabel_FieldID, SitecoreFieldType.SingleLineText, "Footer")]
        string EmailCTALabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SubscribeNewsletterLabel_FieldID, SitecoreFieldType.SingleLineText, "Footer")]
        string SubscribeNewsletterLabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SubscribeCTALink_FieldID, SitecoreFieldType.GeneralLink, "Footer")]
        Link SubscribeCTALink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Copyright_FieldID, SitecoreFieldType.SingleLineText, "Footer")]
        string Copyright { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PageLinks_FieldId, SitecoreFieldType.Treelist, "Footer")]
        IEnumerable<IPageLink> PageLink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SocialLinks_FieldId, SitecoreFieldType.Treelist, "Footer")]
        IEnumerable<ISocialIcon> SocialIcons { get; set; }
    }
}