namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Fields;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ISocialIcon : INavigationGlassBase
    {
        [SitecoreField(Constants.SocialIcon.SocialIcon_FieldName)]
        Image SocialIcon { get; set; }

        [SitecoreField(Constants.SocialIcon.SocialLink_FieldName)]
        Link SocialLink { get; set; }

        [SitecoreField(Constants.SocialIcon.TwitterAccounts_FieldName)]
        string TwitterAccounts { get; set; }
    }
}