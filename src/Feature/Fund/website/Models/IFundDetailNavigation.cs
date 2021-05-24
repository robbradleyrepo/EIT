namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Navigation.Models;
    using System.Collections.Generic;

    public interface IFundDetailNavigation: IFundGlassBase
    {
        [SitecoreField(Constants.FundDetailNavigation.HeadingFieldId)]
        string Header { get; set; }

        IEnumerable<IAnchor> Children { get; set; }
    }
}
