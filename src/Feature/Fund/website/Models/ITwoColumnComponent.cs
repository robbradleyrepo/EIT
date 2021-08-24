namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ITwoColumnComponent : IFundGlassBase
    {
        [SitecoreField(Constants.TwoColumnComponent.AddMargins_FieldId)]
        bool AddMargins { get; set; }

        [SitecoreField(Constants.TwoColumnComponent.LeftColumnSize_FieldId)]
        int LeftColumnSize { get; set; }

        [SitecoreField(Constants.TwoColumnComponent.LeftPaddingSize_FieldId)]
        int LeftPaddingSize { get; set; }

        [SitecoreField(Constants.TwoColumnComponent.RightColumnSize_FieldId)]
        int RightColumnSize { get; set; }

        [SitecoreField(Constants.TwoColumnComponent.RightPaddingSize_FieldId)]
        int RightPaddingSize { get; set; }
    }
}