namespace LionTrust.Feature.Navigation.Models
{
    using System;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Navigation.Models;

    public interface IFooterConfiguration : INavigationGlassBase
    {
        [SitecoreField(Constants.FooterConfiguration.FindUsLabel_FieldID)]
        string FindUsLabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Address_FieldID)]
        string Address { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Location_FieldID)]
        string Location { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PostalCode_FieldID)]
        string PostalCode { get; set; }

        [SitecoreField(Constants.FooterConfiguration.GoogleMapsLink_FieldID)]
        Link GoogleMapsLink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.GoogleMapsGoal_FieldID)]
        Guid GoogleMapsGoal { get; set; }

        [SitecoreField(Constants.FooterConfiguration.GetInTouchLabel_FieldID)]
        string GetInTouchLabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PhoneNumber_FieldID)]
        string PhoneNumber { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PhoneGoal_FieldID)]
        Guid PhoneGoal { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Email_FieldID)]
        string Email { get; set; }

        [SitecoreField(Constants.FooterConfiguration.EmailCTALabel_FieldID)]
        string EmailCTALabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.EmailCTAGoal_FieldID)]
        Guid EmailCTAGoal { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SubscribeNewsletterLabel_FieldID)]
        string SubscribeNewsletterLabel { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SubscribeCTALink_FieldID)]
        Link SubscribeCTALink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SubscribeCTAGoal_FieldID)]
        Guid SubscribeCTAGoal { get; set; }

        [SitecoreField(Constants.FooterConfiguration.Copyright_FieldID)]
        string Copyright { get; set; }

        [SitecoreField(Constants.FooterConfiguration.PageLinks_FieldId)]
        IEnumerable<IPageLink> PageLink { get; set; }

        [SitecoreField(Constants.FooterConfiguration.SocialLinks_FieldId)]
        IEnumerable<ISocialIcon> SocialIcons { get; set; }
    }
}