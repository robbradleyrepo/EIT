namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface IFundManagers : IGlassBase
    {
        [SitecoreField(Constants.FundManagers.FundManagersFieldId)]
        IEnumerable<IAuthor> Managers { get; set; }
    }
}