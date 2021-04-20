namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeaderDropdown : INavigationGlassBase
    {
        [SitecoreField(Constants.HeaderDropDown.PageItem_FieldName, SitecoreFieldType.DropTree, "Header Configuration")])]
        INavigablePage PageItem { get; set; } 

        [SitecoreField(Constants.HeaderDropDown.NavigationColumns_FieldName, SitecoreFieldType.Treelist, "Header Configuration")]
        IEnumerable<IHeaderNavigationColumn> NavigationColumns { get; set; }

        [SitecoreField(Constants.HeaderDropDown.Images_FieldName, SitecoreFieldType.Treelist, "Header Configuration")]
        IEnumerable<IHeaderImage> Images { get; set; }

        [SitecoreField(Constants.HeaderDropDown.ShowCTA_FieldName, SitecoreFieldType.Checkbox, "Header Configuration")]
        bool ShowCTA { get; set; }

        [SitecoreField(Constants.HeaderDropDown.CTALabel_FieldName, SitecoreFieldType.SingleLineText, "Header Configuration")]
        string CTALabel { get; set; }

        [SitecoreField(Constants.HeaderDropDown.CTALink_FieldName, SitecoreFieldType.GeneralLink, "Header Configuration")]
        Link CTALink { get; set; }
    }
}