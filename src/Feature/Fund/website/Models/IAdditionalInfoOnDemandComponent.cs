namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;    

    public interface IAdditionalInfoOnDemandComponent : IFundGlassBase
    {
        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.AdditionalInfoLabel_FieldId)]
        string AdditionalInfoLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.DividendDistributionsLabel_FieldId)]
        string DividendDistributionsLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.DividendDistributionsValue_FieldId)]
        string DividendDistributionsValue { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.NovemberInterimsLabel_FieldId)]
        string NovemberInterimsLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.NovemberInterimsValue_FieldId)]
        string NovemberInterimsValue { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.FebruaryInterimsLabel_FieldId)]
        string FebruaryInterimsLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.FebruaryInterimsValue_FieldId)]
        string FebruaryInterimsValue { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.MayInterimsLabel_FieldId)]
        string MayInterimsLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.MayInterimsValue_FieldId)]
        string MayInterimsValue { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.JulyInterimsLabel_FieldId)]
        string JulyInterimsLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.JulyInterimsValue_FieldId)]
        string JulyInterimsValue { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.YearEndLabel_FieldId)]
        string YearEndLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.YearEndValue_FieldId)]
        string YearEndValue { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.ISINCodeLabel_FieldId)]
        string ISINCodeLabel { get; set; }

        [SitecoreField(Constants.AdditionalInfoOnDemandComponent.SedolCodeLabel_FieldId)]
        string SedolCodeLabel { get; set; }       
    }
}