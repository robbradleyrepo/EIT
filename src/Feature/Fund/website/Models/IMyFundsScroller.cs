namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IMyFundsScroller : IFundScroller
    {
        [SitecoreField(Constants.MyFundsScroller.MaxFundsFieldId)]
        int MaxFunds { get; set; }
    }
}