using FuseIT.Sitecore.SalesforceConnector.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LionTrust.Foundation.Contact.Services
{
    public interface ISalesforceCampaign
    {
        List<CampaignMember> GetCampaignMembers(string connStringname, string campaignIdString);

        Task<int> CreateSitecoreContactsFromSFCampaignMembers(string connStringName, List<CampaignMember> campaignMembers, Guid sitecoreContactListId);
    }
}