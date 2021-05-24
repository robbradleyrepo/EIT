namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeaderDropdown : INavigationGlassBase
    {
        [SitecoreField(Constants.HeaderDropDown.PageItem_FieldID, SitecoreFieldType.DropTree, "Header Configuration")]
        INavigablePage PageItem { get; set; } 

        [SitecoreField(Constants.HeaderDropDown.NavigationColumns_FieldID, SitecoreFieldType.Treelist, "Header Configuration")]
        IEnumerable<IHeaderNavigationColumn> NavigationColumns { get; set; }

        [SitecoreField(Constants.HeaderDropDown.Images_FieldID, SitecoreFieldType.Treelist, "Header Configuration")]
        IEnumerable<IQuickLinkCTA> Images { get; set; }

        [SitecoreField(Constants.HeaderDropDown.ShowCTA_FieldID, SitecoreFieldType.Checkbox, "Header Configuration")]
        bool ShowCTA { get; set; }

        [SitecoreField(Constants.HeaderDropDown.CTALink_FieldID, SitecoreFieldType.GeneralLink, "Header Configuration")]
        Link CTALink { get; set; }
    }
}