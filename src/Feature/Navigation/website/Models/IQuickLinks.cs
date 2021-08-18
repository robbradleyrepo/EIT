namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Navigation.Models;

    public interface IQuickLinks : INavigationGlassBase
    {
        [SitecoreField(Constants.QuickLinks.QuickLinksList_FieldId, SitecoreFieldType.Treelist, "Content")]
        IEnumerable<IQuickLinkCTA> QuickLinksList { get; set; }
    }
}