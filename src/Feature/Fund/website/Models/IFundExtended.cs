namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;

    public interface IFundExtended : IFund
    {
        [SitecoreField(Constants.AdditionalInfoAndCharges.BloombergCode_FieldId)]
        string BloombergCode { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndCharges.DistributionDate_FieldId)]
        string DistributionDate { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndCharges.ExDividendDate_FieldId)]
        string ExDividendDate { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndCharges.IncludedOFC_FieldId)]
        string IncludedOFC { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndCharges.InitialCharge_FieldId)]
        string InitialCharge { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndCharges.ISINCode_FieldId)]
        string ISINCode { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndCharges.OngoingCharges_FieldId)]
        string OngoingCharges { get; set; }

        [SitecoreField(Constants.AdditionalInfoAndCharges.SedolCode_FieldId)]
        string SedolCode { get; set; }
    }
}