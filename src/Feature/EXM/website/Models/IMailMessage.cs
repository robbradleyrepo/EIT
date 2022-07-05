using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;

namespace LionTrust.Feature.EXM.Models
{
    public interface IMailMessage : IExmGlassBase
    {
        [SitecoreField(Constants.SalesforceCampaign.SalesforceCampaignId_FieldID)]
        string SalesforceCampaignId { get; set; }

        [SitecoreField(Constants.MailMessage.IncludedRecipientLists_FieldID)]
        IEnumerable<ISalesforceCampaign> IncludedRecipientLists { get; set; }
    }
}