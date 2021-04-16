namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IHeaderConfiguration : INavigationGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IHeaderDropdown> HeaderDropDowns { get; set; }
    }
}