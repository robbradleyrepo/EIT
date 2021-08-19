namespace LionTrust.Foundation.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.Investor.TemplateId)]
    public interface IInvestor : IGlassBase
    {
        [SitecoreField(Constants.Investor.Name_FieldId)]
        string InvestorName { get; set; }

        [SitecoreField(Constants.Investor.ProfileCard_FieldId)]
        IProfileCard ProfileCard { get; set; }

        [SitecoreField(Constants.Investor.PatternCard_FieldId)]
        IGlassBase PatternCard { get; set; }

        [SitecoreField(Constants.Investor.ExcludedCountires_FieldId)]
        IEnumerable<ICountry> ExcludedCountires { get; set; }

        [SitecoreField(Constants.Investor.Header_FieldId)]
        IGlassBase Header { get; set; }
    }
}