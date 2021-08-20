using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace LionTrust.Feature.Fund.Models
{
    public interface IFundAccessPopUp
    {
        [SitecoreField(Constants.FundAccessPopUp.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.FundAccessPopUp.BackButtonText_FieldId)]
        string BackButtonText { get; set; }

        [SitecoreField(Constants.FundAccessPopUp.PrimaryCTA_FieldId)]
        Link PrimaryCTA { get; set; }

        [SitecoreField(Constants.FundAccessPopUp.SecondaryCTA_FieldId)]
        Link SecondaryCTA { get; set; }
    }
}