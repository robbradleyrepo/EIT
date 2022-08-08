using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;

namespace LionTrust.Feature.EXM.Models
{
    public interface IMailMessage : ISalesforceCampaign
    {
        [SitecoreField(Constants.MailMessage.IncludedRecipientLists_FieldID)]
        IEnumerable<IContactList> IncludedRecipientLists { get; set; }
    }
}