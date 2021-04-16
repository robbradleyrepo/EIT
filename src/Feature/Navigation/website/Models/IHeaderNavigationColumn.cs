﻿namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeaderNavigationColumn : INavigationGlassBase
    {
        [SitecoreField(Constants.HeaderNavigationColumn.ColumnTitle_FieldName)]
        string ColumnTitle { get; set; }

        [SitecoreField(Constants.HeaderNavigationColumn.ShowColumnLink_FieldName)]
        bool ShowColumnLink { get; set; }

        [SitecoreField(Constants.HeaderNavigationColumn.ColumnLink_FieldName)]
        Link ColumnLink { get; set; }

        [SitecoreField(Constants.HeaderNavigationColumn.ColumnLinkLabel_FieldName)]
        string ColumnLinkLabel { get; set; }

        [SitecoreField(Constants.HeaderNavigationColumn.PageLinks_FieldName)]
        IEnumerable<INavigablePage> PageLinks { get; set; }
    }
}