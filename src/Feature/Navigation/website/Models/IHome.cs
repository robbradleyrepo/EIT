namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    public interface IHome : INavigationGlassBase
    {
        [SitecoreField(Constants.Menu.HeaderMenuItems_FieldName)]
        IEnumerable<INavigationGlassBase> HeaderMenuItems { get; set; }

        [SitecoreField(Constants.Menu.ContactUsPage_FieldName)]
        INavigationGlassBase ContactUsPage { get; set; }

        [SitecoreField(Constants.Menu.MyLionTrust_FieldName)]
        INavigationGlassBase MyLionTrust { get; set; }

        [SitecoreField(Constants.Menu.MyPreferences_FieldName)]
        INavigationGlassBase MyPreferences { get; set; }

        [SitecoreField(Constants.Menu.SignUpNewsletter_FieldName)]
        INavigationGlassBase SignupNewsLetter { get; set; }

        [SitecoreField(Constants.Menu.YouAreViewingLabel_FieldName)]
        string YouAreViewingLabel { get; set; }

        [SitecoreField(Constants.Menu.ChangeLabel_FieldName)]
        string ChangeLabel { get; set; }

        [SitecoreField(Constants.Menu.CloseLabel_FieldName)]
        string CloseLabel { get; set; }

        [SitecoreField(Constants.Menu.MenuItems_FieldName)]
        IEnumerable<INavigationGlassBase> MenuItems { get; set; }
    }
}