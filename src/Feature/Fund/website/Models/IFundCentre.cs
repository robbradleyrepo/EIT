using Glass.Mapper.Sc.Configuration.Attributes;

namespace LionTrust.Feature.Fund.Models
{
    public interface IFundCentre
    {
        [SitecoreField(Constants.FundCentre.IFrameRootUrl_FieldId)]
        string FundCentreIFrameRootUrl { get; set; }
    }
}