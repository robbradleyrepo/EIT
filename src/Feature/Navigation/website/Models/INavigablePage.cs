namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;

    public interface INavigablePage : IPresentationBase, INavigationGlassBase
    {
        [SitecoreChildren]
        IEnumerable<INavigablePage> Children { get; set; }
    }
}