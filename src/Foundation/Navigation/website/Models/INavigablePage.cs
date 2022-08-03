namespace LionTrust.Foundation.Navigation.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.ORM.Models;

    public interface INavigablePage : IPresentationBase, IGlassBase
    {
        [SitecoreChildren]
        IEnumerable<INavigablePage> Children { get; set; }

        [SitecoreField(Constants.HeaderConfiguration.MenuItems_FieldID, SitecoreFieldType.Treelist, "Menu")]
        IEnumerable<INavigablePage> MenuItems { get; set; }
    }
}