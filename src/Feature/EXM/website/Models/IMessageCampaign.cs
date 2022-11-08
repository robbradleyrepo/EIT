using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace LionTrust.Feature.EXM.Models
{
    public interface IMessageCampaign : ISalesforceCampaign
    {
        [SitecoreField(Constants.EmailCampaign.TeamId_FieldID)]
        Guid? Team { get; set; }

        [SitecoreField(Constants.EmailCampaign.KeepDefaultSender_FieldID)]
        bool KeepDefaultSender { get; set; }
    }
}