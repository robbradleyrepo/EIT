using FuseIT.S4S.SitecoreSalesforceListBuilder.Models;
using System.Threading.Tasks;

namespace LionTrust.Feature.EXM.Repositories.Interfaces
{
    public interface ISalesforceCampaignRepository
    {
        Task<SalesforceCampaignEntity> ImportCampaignsToSitecore(string id, SalesforceCampaignEntity info);
    }
}