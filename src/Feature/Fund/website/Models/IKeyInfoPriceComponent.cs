namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IKeyInfoPriceComponent : IFundSelector
    {
        [SitecoreField(Constants.KeyInfoPriceComponent.KeyInformationLabel_FieldId)]
        string KeyInformationLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.ClassLaunchDateLabel_FieldId)]
        string ClassLaunchDateLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.Comparator1Label_FieldId)]
        string Comparator1Label { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.Comparator2Label_FieldId)]
        string Comparator2Label { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.OfferPriceLabel_FieldId)]
        string OfferPriceLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.PriceDateLabel_FieldId)]
        string PriceDateLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.PriceDateTooltipLabel_FieldId)]
        string PriceDateTooltipLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.PriceLabel_FieldId)]
        string PriceLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.SinglePriceLabel_FieldId)]
        string SinglePriceLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.SectorNameLabel_FieldId)]
        string SectorLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.ManagerInceptionDateLabel_FieldId)]
        string ManagerInceptionDateLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.TargetBenchmarkYieldLabel_FieldId)]
        string TargetBenchmarkYieldLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.TargetBenchmarkYieldValue_FieldId)]
        string TargetBenchmarkYieldValue { get; set; }
    }
}