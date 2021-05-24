namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeaderNavigationColumn : INavigationGlassBase
    {
        [SitecoreField(Constants.HeaderNavigationColumn.ColumnTitle_FieldID, SitecoreFieldType.SingleLineText, "Content")]
        string ColumnTitle { get; set; }

        [SitecoreField(Constants.HeaderNavigationColumn.ShowColumnLink_FieldID, SitecoreFieldType.Checkbox, "Content")]
        bool ShowColumnLink { get; set; }

        [SitecoreField(Constants.HeaderNavigationColumn.ColumnLink_FieldID, SitecoreFieldType.GeneralLink, "Content")]
        Link ColumnLink { get; set; }

        [SitecoreField(Constants.HeaderNavigationColumn.PageLinks_FieldID, SitecoreFieldType.Treelist, "Content")]
        IEnumerable<INavigablePage> PageLinks { get; set; }
    }
}