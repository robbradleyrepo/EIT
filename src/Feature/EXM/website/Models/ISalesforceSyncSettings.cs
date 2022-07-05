namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System;

    public interface ISalesforceSyncSettings : IExmGlassBase
    {
        [SitecoreField(Constants.SalesforceSyncSettings.LastSyncDate_FieldID)]
        DateTime? LastSyncDate { get; set; }
    }
}