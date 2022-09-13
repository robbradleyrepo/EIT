namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System;

    public interface ISalesforceSyncSettings : IExmGlassBase
    {
        [SitecoreField(Constants.SalesforceSyncSettings.LastSyncDate_FieldID)]
        DateTime? LastSyncDate { get; set; }

        [SitecoreField(Constants.SalesforceSyncSettings.Enabled_FieldID)]
        bool Enabled { get; set; }

        [SitecoreField(Constants.SalesforceSyncSettings.Running_FieldID)]
        bool Running { get; set; }

        [SitecoreField(Constants.SalesforceSyncSettings.FromAddress_FieldID)]
        string FromAddress { get; set; }

        [SitecoreField(Constants.SalesforceSyncSettings.FromDisplayName_FieldID)]
        string FromDisplayName { get; set; }

        [SitecoreField(Constants.SalesforceSyncSettings.ToAddress_FieldID)]
        string ToAddresses { get; set; }

        [SitecoreField(Constants.SalesforceSyncSettings.Subject_FieldID)]
        string Subject { get; set; }
    }
}