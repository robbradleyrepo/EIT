namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using System;

    public interface IFundClass: ILegacyGlassBase
    {
        [SitecoreField(Constants.FundClass.MinimalInitialInvestmentFieldId)]
        string MinimalInitialInvestment { get; set; }

        [SitecoreField(Constants.FundClass.MinimumAdditionalInvestmentFieldId)]
        string MinimumAdditionalInvestment { get; set; }

        [SitecoreField(Constants.FundClass.CitiCodeFieldId)]
        string CitiCode { get; set; }

        [SitecoreField(Constants.FundClass.ClassNameFieldId)]
        string ClassName { get; set; }

        [SitecoreField(Constants.FundClass.ValueFieldId)]
        string Value { get; set; }

        [SitecoreField(Constants.FundClass.ClassLaunchDate_FieldId)]
        DateTime ClassLaunchDate { get; set; }

        [SitecoreField(Constants.FundClass.Comparator1_FieldId)]
        string Comparator1 { get; set; }

        [SitecoreField(Constants.FundClass.Comparator2_FieldId)]
        string Comparator2 { get; set; }

        [SitecoreField(Constants.FundClass.OfferPrice_FieldId)]
        string OfferPrice { get; set; }

        [SitecoreField(Constants.FundClass.SinglePrice_FieldId)]
        string SinglePrice { get; set; }

        [SitecoreField(Constants.FundClass.PriceDate_FieldId)]
        DateTime PriceDate { get; set; }
        
        [SitecoreField(Constants.FundClass.DistributionDate_FieldId)]
        string DistributionDate { get; set; }

        [SitecoreField(Constants.FundClass.ExDividendDate_FieldId)]
        string ExDividendDate { get; set; }

        [SitecoreField(Constants.FundClass.InitialCharge_FieldId)]
        string InitialCharge { get; set; }

        [SitecoreField(Constants.FundClass.ISINCode_FieldId)]
        string ISINCode { get; set; }

        [SitecoreField(Constants.FundClass.OngoingCharges_FieldId)]
        string OngoingCharges { get; set; }

        [SitecoreField(Constants.FundClass.SedolCode_FieldId)]
        string SedolCode { get; set; }

        [SitecoreField(Constants.FundClass.FactsheetFieldId)]
        Image Factsheet { get; set; } 

        [SitecoreField(Constants.FundClass.BenchmarkFieldId)]
        string Benchmark { get; set; }

        [SitecoreField(Constants.FundClass.ManagerInceptionDateFieldId)]
        DateTime ManagerInceptionDateOfFund { get; set; }

        [SitecoreField(Constants.FundClass.TargetBenchmarkYieldFieldId)]
        string TargetBenchmarkYield { get;  set; }

        [SitecoreField(Constants.FundClass.AnnualManagementChargeFieldId)]
        string AnnualManagemnetCharge { get; set; }
    }
}