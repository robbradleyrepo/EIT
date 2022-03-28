namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IKeyInfoPriceOnDemandComponent : IFundGlassBase
    {
        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.KeyInformationLabel_FieldId)]
        string KeyInformationLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.BenchmarkIndexLabel_FieldId)]
        string BenchmarkIndexLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.AICSectorLabel_FieldId)]
        string AICSectorLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.NetAssetValuePerShareLabel_FieldId)]
        string NetAssetValuePerShareLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.PremiumDiscountLabel_FieldId)]
        string PremiumDiscountLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.HistoricDividendPerShareLabel_FieldId)]
        string HistoricDividendPerShareLabel { get; set; }        

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.HistoricDividendPerShareValue_FieldId)]
        string HistoricDividendPerShareValue { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.HistoricSharePriceYieldLabel_FieldId)]
        string HistoricSharePriceYieldLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.HistoricSharePriceYieldValue_FieldId)]
        string HistoricSharePriceYieldValue { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.OngoingChargesLabel_FieldId)]
        string OngoingChargesLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.OngoingChargesValue_FieldId)]
        string OngoingChargesValue { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.ActiveSharesLabel_FieldId)]
        string ActiveSharesLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.ActiveSharesValue_FieldId)]
        string ActiveSharesValue { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.GearingGrossLabel_FieldId)]
        string GearingGrossLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.GearingGrossValue_FieldId)]
        string GearingGrossValue { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.GearingNetLabel_FieldId)]
        string GearingNetLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.GearingNetValue_FieldId)]
        string GearingNetValue { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.HoldingsLabel_FieldId)]
        string HoldingsLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceOnDemandComponent.HoldingsValue_FieldId)]
        string HoldingsValue { get; set; }
    }
}