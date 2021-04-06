namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface IHeaderDropdown
    {
        [SitecoreField(Constants.HeaderDropDown.NavigationColumns_FieldName)]
        IEnumerable<IHeaderNavigationColumn> NavigationColumns { get; set; }

        [SitecoreField(Constants.HeaderDropDown.Images_FieldName)]
        IEnumerable<IHeaderImage> Images { get; set; }

        [SitecoreField(Constants.HeaderDropDown.ShowCTA_FieldName)]
        bool ShowCTA { get; set; }

        [SitecoreField(Constants.HeaderDropDown.CTALabel_FieldName)]
        string CTALabel { get; set; }

        [SitecoreField(Constants.HeaderDropDown.CTALink_FieldName)]
        Link CTALink { get; set; }
    }
}