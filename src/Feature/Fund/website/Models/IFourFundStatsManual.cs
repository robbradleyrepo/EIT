namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFourFundStatsManual : IFundGlassBase
    {
        [SitecoreField(Constants.FourFundStatsManual.Heading_FieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.FourFundStatsManual.FirstTitle_FieldId)]
        string FirstTitle { get; set; }

        [SitecoreField(Constants.FourFundStatsManual.FirstValue_FieldId)]
        string FirstValue { get; set; }

        [SitecoreField(Constants.FourFundStatsManual.SecondTitle_FieldId)]
        string SecondTitle { get; set; }

        [SitecoreField(Constants.FourFundStatsManual.SecondValue_FieldId)]
        string SecondValue { get; set; }

        [SitecoreField(Constants.FourFundStatsManual.ThirdTitle_FieldId)]
        string ThirdTitle { get; set; }

        [SitecoreField(Constants.FourFundStatsManual.ThirdValue_FieldId)]
        string ThirdValue { get; set; }

        [SitecoreField(Constants.FourFundStatsManual.FourthTitle_FieldId)]
        string FourthTitle { get; set; }

        [SitecoreField(Constants.FourFundStatsManual.FourthValue_FieldId)]
        string FourthValue { get; set; }
    }
}