namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface INavigationGlassBase : IGlassBase
    {
        [SitecoreField(Constants.Navigation.MenuTitle_FieldName)]
        public string MenuTitle { get; set; }

        [SitecoreChildren]
        public IEnumerable<INavigationGlassBase> Childrens { get; set; }
    }
}