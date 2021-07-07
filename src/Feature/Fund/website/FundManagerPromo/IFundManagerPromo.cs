namespace LionTrust.Feature.Fund.FundManagerPromo
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.ORM.Models;

    public interface IFundManagerPromo: IGlassBase
    {
        [SitecoreField(Constants.FundManagerPromo.MangerFieldId)]
        IAuthor FundManager { get; set; }
    }
}