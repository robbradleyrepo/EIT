namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.Navigation.Models;
    using LionTrust.Foundation.Onboarding.Models;
    using LionTrust.Foundation.ORM.Models;
    using System;

    public interface IHome : IIdentity, INavigationGlassBase, IPresentationBase
    {
        [SitecoreField(Constants.NavigationRoot.ContactUsPage_FieldID, SitecoreFieldType.DropTree, "Header")]
        INavigablePage ContactUsPage { get; set; }

        [SitecoreField(Constants.NavigationRoot.FooterConfiguration_FieldId, SitecoreFieldType.DropTree, "Footer")]
        IFooterConfiguration FooterConfiguration { get; set; }

        [SitecoreField(Constants.NavigationRoot.MyLionTrust_FieldID, SitecoreFieldType.GeneralLink, "My Liontrust")]
        Link MyLionTrust { get; set; }

        [SitecoreField(Constants.NavigationRoot.MyLiontrustAllowedInvestors_FieldID, SitecoreFieldType.Multilist, "My Liontrust")]
        IEnumerable<IInvestor> MyLiontrusAllowedInvestors { get; set; }

        [SitecoreField(Constants.NavigationRoot.MyLionTrustGoal_FieldID)]
        Guid MyLiontrustGoal { get; set; }

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

        [SitecoreField(Constants.NavigationRoot.LionHub_FieldID, SitecoreFieldType.GeneralLink, "LionHub")]
        Link LionHub { get; set; }

        [SitecoreField(Constants.NavigationRoot.LionHubAllowedInvestors_FieldID, SitecoreFieldType.Multilist, "LionHub")]
        IEnumerable<IInvestor> LionHubAllowedInvestors { get; set; }

        [SitecoreField(Constants.NavigationRoot.LionHubAllowedPages_FieldID, SitecoreFieldType.Treelist, "LionHub")]
        IEnumerable<INavigablePage> LionHubAllowedPages { get; set; }

        string OnboardingRoleName { get; set; }

        string YouAreViewingLabelWithArticle { get; set; }

        IHeaderConfiguration HeaderConfiguration { get; set; }

        string ChangeInvestorUrl { get; set; }

        string CurrentCountry { get; set; }
    }
}