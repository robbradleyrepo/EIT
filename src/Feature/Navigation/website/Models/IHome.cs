namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IHome : INavigationGlassBase
    {
        [SitecoreField(Constants.NavigationRoot.HeaderConfiguration_FieldName)]
        IHeaderConfiguration HeaderConfiguration { get; set; }

        [SitecoreField(Constants.NavigationRoot.ContactUsPage_FieldName)]
        INavigablePage ContactUsPage { get; set; }

        [SitecoreField(Constants.NavigationRoot.FooterConfiguration_FieldName)]
        IFooterConfiguration FooterConfiguration { get; set; }

        [SitecoreField(Constants.Menu.MyLionTrust_FieldName)]
        INavigablePage MyLionTrust { get; set; }

        [SitecoreField(Constants.Menu.MyPreferences_FieldName)]
        INavigablePage MyPreferences { get; set; }

        [SitecoreField(Constants.Menu.SignUpNewsletter_FieldName)]
        INavigablePage SignupNewsLetter { get; set; }

        [SitecoreField(Constants.Menu.YouAreViewingLabel_FieldName)]
        string YouAreViewingLabel { get; set; }

        [SitecoreField(Constants.Menu.ChangeLabel_FieldName)]
        string ChangeLabel { get; set; }
        
        [SitecoreField(Constants.Menu.MenuItems_FieldName)]
        IEnumerable<INavigablePage> MenuItems { get; set; }
    }
}