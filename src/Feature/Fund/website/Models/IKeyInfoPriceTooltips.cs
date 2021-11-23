namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IKeyInfoPriceTooltips : IGlassBase
    {
        [SitecoreField(Constants.KeyInfoPriceTooltips.ClassLaunchDateTooltip_FieldId)]
        string ClassLaunchDateTooltip { get; set; }

        [SitecoreField(Constants.KeyInfoPriceTooltips.Comparator1Tooltip_FieldId)]
        string Comparator1Tooltip { get; set; }

        [SitecoreField(Constants.KeyInfoPriceTooltips.Comparator2Tooltip_FieldId)]
        string Comparator2Tooltip { get; set; }
       
        [SitecoreField(Constants.KeyInfoPriceTooltips.SectorNameTooltip_FieldId)]
        string SectorNameTooltip { get; set; }

        [SitecoreField(Constants.KeyInfoPriceTooltips.ManagerInceptionDateTooltip_FieldId)]
        string ManagerInceptionDateTooltip { get; set; }

        [SitecoreField(Constants.KeyInfoPriceTooltips.TargetBenchmarkYieldTooltip_FieldId)]
        string TargetBenchmarkYieldTooltip { get; set; }

        [SitecoreField(Constants.KeyInfoPriceTooltips.SinglePriceTooltip_FieldId)]
        string SinglePriceTooltip { get; set; }

        [SitecoreField(Constants.KeyInfoPriceTooltips.OfferPriceTooltip_FieldId)]
        string OfferPriceTooltip { get; set; }

        [SitecoreField(Constants.KeyInfoPriceTooltips.PriceDateTooltip_FieldId)]
        string PriceDateTooltip { get; set; }


    }
}