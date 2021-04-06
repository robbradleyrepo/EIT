namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using Sitecore.Data.Items;
    
    public interface IHeaderConfiguration
    {
        [SitecoreChildren]
        IEnumerable<Item> HeaderDropDowns { get; set; }
    }
}