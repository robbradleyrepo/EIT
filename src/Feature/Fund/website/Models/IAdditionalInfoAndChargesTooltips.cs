namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IAdditionalInfoAndChargesTooltips : IGlassBase
    {
        [SitecoreField(Constants.AdditionalInfoAndChargesTooltips.MinInitialTooltip_FieldId)]
        string MinInitialTooltip { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesTooltips.MinAdditionalTooltip_FieldId)]
        string MinAdditionalTooltip { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesTooltips.ExDividendDateTooltip_FieldId)]
        string ExDividendDateTooltip { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesTooltips.DistributionDateTooltip_FieldId)]
        string DistributionDateTooltip { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesTooltips.SedolCodeTooltip_FieldId)]
        string SedolCodeTooltip { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesTooltips.ISINCodeTooltip_FieldId)]
        string ISINCodeTooltip { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesTooltips.InitialChargeTooltip_FieldId)]
        string InitialChargeTooltip { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesTooltips.OngoingChargesTooltip_FieldId)]
        string OngoingChargesTooltip { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndChargesTooltips.AnnualManagementChargeTooltip_FieldId)]
        string AnnualManagementChargeTooltip { get; set; }
    }
}