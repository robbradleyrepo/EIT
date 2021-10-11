namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Design;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.ORM.Models;
    using Constants = Constants;

    public interface IFundOverview : IGlassBase
    {
        [SitecoreField(Constants.FundOverview.FundFieldId)]
        IFund Fund { get; set; }

        [SitecoreField(Constants.FundOverview.LatestUpdateCTA_FieldId, SitecoreFieldType.GeneralLink, "Fund Overview")]
        Link LatestUpdateCTA { get; set; }

        [SitecoreField(Constants.FundOverview.DownloadCTA_FieldId, SitecoreFieldType.GeneralLink, "Fund Overview")]
        Link DownloadCTA { get; set; }

        [SitecoreField(Constants.FundOverview.Alignment_FieldId)]
        ILookupValue Alignment { get; set; }
    }
}