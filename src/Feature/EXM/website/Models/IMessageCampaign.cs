using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace LionTrust.Feature.EXM.Models
{
    public interface IMessageCampaign : ISalesforceCampaign
    {
        [SitecoreField(Constants.TeamCampaign.TeamId_FieldID)]
        Guid? Team { get; set; }
    }
}