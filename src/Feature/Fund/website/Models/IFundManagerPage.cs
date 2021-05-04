namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.ORM.Models;

    public interface IFundManagerPage: IGlassBase
    {
        [SitecoreField(Constants.FundManager.ManagerFieldId)]
        IAuthor Manager { get; set; }
    }
}
