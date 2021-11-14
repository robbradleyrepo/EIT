namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IHoldingsTable: IFundSelector
    {
        [SitecoreField(Constants.HoldingsTable.HeadingFieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.HoldingsTable.InformationFieldId)]
        string Information { get; set; }
    }
}
