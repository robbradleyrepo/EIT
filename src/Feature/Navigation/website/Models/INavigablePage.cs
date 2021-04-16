namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    
    public interface INavigablePage : INavigationGlassBase
    {
        [SitecoreField(Constants.Navigation.MenuTitle_FieldName)]
        string MenuTitle { get; set; }

        [SitecoreChildren]
        IEnumerable<INavigablePage> Children { get; set; }
    }
}