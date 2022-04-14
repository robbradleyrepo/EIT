namespace LionTrust.Foundation.Navigation.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IHeaderConfiguration : IGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IHeaderDropdown> HeaderDropDowns { get; set; }

        [SitecoreField(Constants.HeaderConfiguration.MenuItems_FieldID, SitecoreFieldType.Treelist, "Menu")]
        IEnumerable<INavigablePage> MenuItems { get; set; }
    }
}