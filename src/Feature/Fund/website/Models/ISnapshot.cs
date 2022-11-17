namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface ISnapshot : IFundGlassBase
    {
        [SitecoreField(Constants.Snapshot.SnapshotTitle_FieldId)]
        string SnapshotTitle { get; set; }

        [SitecoreField(Constants.Snapshot.SnapshotSubtitle_FieldId)]
        string SnapshotSubtitle { get; set; }

        [SitecoreField(Constants.Snapshot.SharePriceLabel_FieldId)]
        string SharePriceLabel { get; set; }

        [SitecoreField(Constants.Snapshot.NetAssetValuePerShareLabel_FieldId)]
        string NetAssetValuePerShareLabel { get; set; }

        [SitecoreField(Constants.Snapshot.PremiumDiscountLabel_FieldId)]
        string PremiumDiscountLabel { get; set; }

        [SitecoreField(Constants. Snapshot.GrossAssetsLabel_FieldId)]
        string GrossAssetsLabel { get; set; }

        [SitecoreField(Constants. Snapshot.GrossAssetsValue_FieldId)]
        string GrossAssetsValue { get; set; }

        [SitecoreField(Constants.Snapshot.GearingGrossLabel_FieldId)]
        string GearingGrossLabel { get; set; }

        [SitecoreField(Constants.Snapshot.GearingGrossValue_FieldId)]
        string GearingGrossValue { get; set; }

        [SitecoreField(Constants.Snapshot.YieldLabel_FieldId)]
        string YieldLabel { get; set; }

        [SitecoreField(Constants.Snapshot.YieldValue_FieldId)]
        string YieldValue { get; set; }

        [SitecoreField(Constants.Snapshot.Column1_FieldId)]
        string Column1 { get; set; }

        [SitecoreField(Constants.Snapshot.Column2_FieldId)]
        string Column2 { get; set; }

        [SitecoreField(Constants.Snapshot.Column3_FieldId)]
        string Column3 { get; set; }

        [SitecoreField(Constants.Snapshot.SnapshotIcon_FieldId)]
        Image Icon { get; set; }

        [SitecoreField(Constants.Snapshot.BackgroundImage_FieldId)]
        Image BackgroundImage { get; set; }
    }
}