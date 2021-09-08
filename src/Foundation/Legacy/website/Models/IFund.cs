namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Onboarding.Models;
    using System;
    using System.Collections.Generic;

    public interface IFund : IFundOverviewData, ILegacyGlassBase
    {
        [SitecoreField(Constants.Fund.CitiCodeFieldId)]
        string CitiCode { get; set; }

        [SitecoreField(Constants.Fund.ClassesFieldId)]
        IEnumerable<IFundClass> Classes { get; set; }

        [SitecoreField(Constants.Fund.FundManagersFieldId, SectionName ="Fund Info")]
        IEnumerable<IAuthor> FundManagers { get; set; }

        [SitecoreField(Constants.Fund.LaunchDate_FieldId, SitecoreFieldType.Date,  "Fund Info")]
        DateTime LaunchDate { get; set; }

        [SitecoreField(Constants.Fund.FundSize_FieldId)]
        string FundSize { get; set; }

        [SitecoreField(Constants.Fund.NumberOfHoldings_FieldId)]
        string NumberOfHoldings { get; set; }

        [SitecoreField(Constants.Fund.BenchmarkName_FieldId)]
        string BenchmarkName { get; set; }

        [SitecoreField(Constants.Fund.HistoricYield_FieldId)]
        string HistoricYield { get; set; }

        [SitecoreField(Constants.Fund.RiskWarning_FieldId)]
        string RiskWarning { get; set; }
        
        [SitecoreQuery("./Documents", IsRelative = true)]
        IFundDocuments DocumentsFolder { get; set; }

        [SitecoreField(Constants.Fund.FundNameFieldId)]
        string FundName { get; set; }

        [SitecoreField(Constants.Fund.OverviewDescriptionFieldId)]
        string OverviewDescription { get; set; }

        [SitecoreField(Constants.FundAccess.ExcludedCountires_FieldId)]
        IEnumerable<ICountry> ExcludedCountries { get; set; }

        [SitecoreField(Constants.FundPage.Page_FieldId)]
        Link Page { get; set; }
    }
}