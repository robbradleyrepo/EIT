namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Fields;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Configuration;
    using System.Collections.Generic;
    using System;

    public interface ISocialIcon : INavigationGlassBase
    {
        [SitecoreField(Constants.SocialIcon.SocialIcon_FieldID)]
        Image SocialIcon { get; set; }

        [SitecoreField(Constants.SocialIcon.SocialLink_FieldID)]
        Link SocialLink { get; set; }

        [SitecoreField(Constants.SocialIcon.SocialLinkGoal_FieldID)]
        Guid SocialLinkGoal { get; set; }

        [SitecoreChildren]
        IEnumerable<ITwitterAccount> TwitterAccounts { get; set; }
    }
}