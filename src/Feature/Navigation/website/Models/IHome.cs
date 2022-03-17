namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.Navigation.Models;
    using LionTrust.Foundation.Onboarding.Models;

    public interface IHome : IIdentity, INavigationGlassBase, IPresentationBase
    {
        [SitecoreField(Constants.NavigationRoot.ContactUsPage_FieldID, SitecoreFieldType.DropTree, "Header")]
        INavigablePage ContactUsPage { get; set; }

        [SitecoreField(Constants.NavigationRoot.FooterConfiguration_FieldId, SitecoreFieldType.DropTree, "Footer")]
        IFooterConfiguration FooterConfiguration { get; set; }

        [SitecoreField(Constants.NavigationRoot.MyLionTrust_FieldID, SitecoreFieldType.GeneralLink, "Menu")]
        Link MyLionTrust { get; set; }

        [SitecoreField(Constants.NavigationRoot.MyPreferences_FieldID, SitecoreFieldType.DropTree, "Menu")]
        INavigablePage MyPreferences { get; set; }

        [SitecoreField(Constants.NavigationRoot.SignUpNewsletter_FieldID, SitecoreFieldType.DropTree, "Menu")]
        INavigablePage SignupNewsLetter { get; set; }

        [SitecoreField(Constants.NavigationRoot.YouAreViewingLabel_FieldID, SitecoreFieldType.SingleLineText, "Menu")]
        string YouAreViewingLabel { get; set; }

        [SitecoreField(Constants.NavigationRoot.FromLabel_FieldID, SitecoreFieldType.SingleLineText, "Menu")]
        string FromLabel { get; set; }

        [SitecoreField(Constants.NavigationRoot.ChangeLabel_FieldID, SitecoreFieldType.SingleLineText, "Menu")]
        string ChangeLabel { get; set; }              

        [SitecoreField(Constants.NavigationRoot.OnboardingConfiguation_FieldId)]
        IOnboardingConfiguration OnboardingConfiguration { get; set; }
         
        string OnboardingRoleName { get; set; }

        string YouAreViewingLabelWithArticle { get; set; }

        IHeaderConfiguration HeaderConfiguration { get; set; }

        string ChangeInvestorUrl { get; set; }

        string CurrentCountry { get; set; }
    }
}