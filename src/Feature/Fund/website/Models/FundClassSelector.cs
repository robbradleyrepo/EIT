namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;

    public interface IFundClassSelector: IFundGlassBase
    {
        [SitecoreField(Constants.FundClassSelector.FundFieldId)]
        IFund Fund { get; set; }

        [SitecoreField(Constants.FundClassSelector.DefaultClassFieldId)]
        IFundClass DefaultClass { get; set; }

        [SitecoreField(Constants.FundClassSelector.DropDownLabelFieldId)]
        string DropDownLabel { get; set; }
    }
}