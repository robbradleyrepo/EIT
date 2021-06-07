namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IHoldingsTable: IFundSelector
    {
        [SitecoreField(Constants.HoldingsTable.HeadingFieldId)]
        string Heading { get; set; }
    }
}
