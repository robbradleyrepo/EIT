namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;

    public interface IKeyInfoPriceComponent : IFundGlassBase
    {
        [SitecoreField(Constants.KeyInfoPriceComponent.FundClassFieldId)]
        IFundClass FundClass { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.KeyInformationLabel_FieldId)]
        string KeyInformationLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.Benchmark1Label_FieldId)]
        string Benchmark1Label { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.ClassLaunchDateLabel_FieldId)]
        string ClassLaunchDateLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.ComparatorLabel_FieldId)]
        string ComparatorLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.DurationLabel_FieldId)]
        string DurationLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.FundSizeLabel_FieldId)]
        string FundSizeLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.NetUnderlyingYieldLabel_FieldId)]
        string NetUnderlyingYieldLabel { get; set; }

        [SitecoreField(Constants.KeyInfoPriceComponent.NumberOfHoldingsLabel_FieldId)]
        string NumberofHoldingsLabel { get; set; }

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
    }
}