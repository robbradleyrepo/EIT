namespace LionTrust.Feature.Navigation.Models
{
    using System.Collections.Generic;

    using Sitecore.Data.Items;

    public class NavigationViewModel
    {
        public ISiteRoot RootItem { get; set; }
        public IHome HomeItem { get; set; }
    }
}