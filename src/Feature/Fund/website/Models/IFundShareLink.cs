namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IFundShareLink : IGlassBase
    {
        [SitecoreField(Constants.FundShareLink.Label_FieldId)]
        string ShareLabel { get; set; }

        [SitecoreField(Constants.FundShareLink.Tooltip_FieldId)]
        string ShareTooltip { get; set; }       
    }
}
