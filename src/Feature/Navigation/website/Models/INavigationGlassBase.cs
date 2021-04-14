namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;    

    public interface INavigationGlassBase : IGlassBase
    {
        [SitecoreField(Constants.Navigation.MenuTitle_FieldName)]
        string MenuTitle { get; set; }

        [SitecoreChildren]
        IEnumerable<INavigationGlassBase> Children { get; set; }
    }
}