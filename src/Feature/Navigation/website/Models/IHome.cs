﻿namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Navigation.Models;
    using LionTrust.Foundation.Onboarding.Models;
    using System;

    public interface IHome : IHomeBase
    {
        [SitecoreField(Constants.NavigationRoot.ContactUsPage_FieldID, SitecoreFieldType.DropTree, "Header")]
        INavigablePage ContactUsPage { get; set; }

        [SitecoreField(Constants.NavigationRoot.MyLionTrust_FieldID, SitecoreFieldType.GeneralLink, "My Liontrust")]
        Link MyLionTrust { get; set; }

        [SitecoreField(Constants.NavigationRoot.MyLiontrustAllowedInvestors_FieldID, SitecoreFieldType.Multilist, "My Liontrust")]
        IEnumerable<IInvestor> MyLiontrusAllowedInvestors { get; set; }

        [SitecoreField(Constants.NavigationRoot.MyLionTrustGoal_FieldID)]
        Guid MyLiontrustGoal { get; set; }

        [SitecoreField(Constants.NavigationRoot.MyLiontrustName_FieldID)]
        string MyLiontrustName { get; set; }

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

        [SitecoreField(Constants.NavigationRoot.SearchLabel_FieldID, SitecoreFieldType.SingleLineText, "Menu")]
        string SearchLabel { get; set; }

        [SitecoreField(Constants.NavigationRoot.OnboardingConfiguation_FieldId)]
        IOnboardingConfiguration OnboardingConfiguration { get; set; }

        [SitecoreField(Constants.NavigationRoot.LionHub_FieldID, SitecoreFieldType.GeneralLink, "LionHub")]
        Link LionHub { get; set; }

        [SitecoreField(Constants.NavigationRoot.LionHubAllowedInvestors_FieldID, SitecoreFieldType.Multilist, "LionHub")]
        IEnumerable<IInvestor> LionHubAllowedInvestors { get; set; }

        [SitecoreField(Constants.NavigationRoot.LionHubAllowedPages_FieldID, SitecoreFieldType.Treelist, "LionHub")]
        IEnumerable<INavigablePage> LionHubAllowedPages { get; set; }

        [SitecoreField(Constants.NavigationRoot.LionHubName_FieldID, SitecoreFieldType.Treelist, "LionHub")]
        string LionHubName { get; set; }

        string OnboardingRoleName { get; set; }

        string YouAreViewingLabelWithArticle { get; set; }

        new IHeaderConfiguration HeaderConfiguration { get; set; }

        string ChangeInvestorUrl { get; set; }

        string CurrentCountry { get; set; }
    }
}