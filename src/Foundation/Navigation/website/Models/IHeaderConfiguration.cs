namespace LionTrust.Foundation.Navigation.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IHeaderConfiguration : IGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IHeaderDropdown> HeaderDropDowns { get; set; }
    }
}