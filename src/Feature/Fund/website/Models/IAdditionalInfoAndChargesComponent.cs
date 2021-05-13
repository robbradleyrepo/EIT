namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IAdditionalInfoAndChargesComponent : IFundGlassBase
    {
        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.AdditionalInfoLabel_FieldId)]
        string AdditionalInfoLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.BloombergCodeLabel_FieldId)]
        string BloombergCodeLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.ChargesLabel_FieldId)]
        string ChargesLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.DistributionDateLabel_FieldId)]
        string DistributionDateLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.ExDividendDateLabel_FieldId)]
        string ExDividendDateLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.IncludedOFCLabel_FieldId)]
        string IncludedOFCLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.InitialChargeLabel_FieldId)]
        string InitialChargeLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.ISINCodeLabel_FieldId)]
        string ISINCodeLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.MinAdditionalLabel_FieldId)]
        string MinAdditionalLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.MinInitialLabel_FieldId)]
        string MinInitialLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.OngoingChargesLabel_FieldId)]
        string OngoingChargesLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesComponent.SedolCodeLabel_FieldId)]
        string SedolCodeLabel { get; set; }
    }
}