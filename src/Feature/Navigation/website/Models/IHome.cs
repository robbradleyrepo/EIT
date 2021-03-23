namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System.Collections.Generic;

    public interface IHome : INavigationGlassBase
    {
        [SitecoreField(Constants.Identity.Logo_FieldName)]
        public Image Logo { get; set; }

        [SitecoreField(Constants.Identity.HeaderMenuItems_FieldName)]
        public IEnumerable<INavigationGlassBase> HeaderMenuItems { get; set; }

        [SitecoreField(Constants.Identity.ContactUsPage_FieldName)]
        public INavigationGlassBase ContactUsPage { get; set; }

        [SitecoreField(Constants.Menu.MyLionTrust_FieldName)]
        public INavigationGlassBase MyLionTrust { get; set; }

        [SitecoreField(Constants.Menu.MyPreferences_FieldName)]
        public INavigationGlassBase MyPreferences { get; set; }

        [SitecoreField(Constants.Menu.SignUpNewsletter_FieldName)]
        public INavigationGlassBase SignupNewsLetter { get; set; }

        [SitecoreField(Constants.Menu.YouAreViewingLabel_FieldName)]
        public string YouAreViewingLabel { get; set; }

        [SitecoreField(Constants.Menu.ChangeLabel_FieldName)]
        public string ChangeLabel { get; set; }

        [SitecoreField(Constants.Menu.CloseLabel_FieldName)]
        public string CloseLabel { get; set; }

        [SitecoreField(Constants.Menu.MenuItems_FieldName)]
        public IEnumerable<INavigationGlassBase> MenuItems { get; set; }
    }
}