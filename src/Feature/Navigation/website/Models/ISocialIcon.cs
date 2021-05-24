namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Fields;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Configuration;

    public interface ISocialIcon : INavigationGlassBase
    {
        [SitecoreField(Constants.SocialIcon.SocialIcon_FieldID, SitecoreFieldType.Image, "Social Icon")]
        Image SocialIcon { get; set; }

        [SitecoreField(Constants.SocialIcon.SocialLink_FieldID, SitecoreFieldType.GeneralLink, "Social Icon")]
        Link SocialLink { get; set; }

        [SitecoreField(Constants.SocialIcon.TwitterAccounts_FieldID, SitecoreFieldType.SingleLineText, "Social Icon")]
        string TwitterAccounts { get; set; }
    }
}