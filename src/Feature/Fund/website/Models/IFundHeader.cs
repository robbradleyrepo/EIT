namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.ORM.Models;

    public interface IFundHeader : IGlassBase
    {
        [SitecoreField(Constants.FundHeader.FundFieldId)]
        IFund Fund { get; set; }

        [SitecoreField(Constants.FundHeader.TitleFieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.FundHeader.FundManagerFieldId)]
        IAuthor FundManager { get; set; }

        [SitecoreField(Constants.FundHeader.BackgroundImageFieldId)]
        Glass.Mapper.Sc.Fields.Image BackgroundImage { get;set; }

        [SitecoreField(Constants.FundHeader.FundSharingFieldId)]
        IFundShareLink FundSharing { get; set; }
    }
}
