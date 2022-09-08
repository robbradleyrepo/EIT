using Glass.Mapper.Sc.Configuration.Attributes;

namespace LionTrust.Feature.EXM.Models
{
    public interface IContactList : ISalesforceCampaign
    {
        [SitecoreField(Constants.ContactList.ActiveId_FieldID)]
        bool Active { get; set; }
    }
}