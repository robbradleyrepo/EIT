using LionTrust.Feature.EXM.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LionTrust.Feature.EXM.Services.Interfaces
{
    public interface ISalesforceAnalyticsService
    {
        Task<List<EntityViewModel>> GetEntityWithInteractions(DateTime fromDate);

        bool SyncEngagementHistory(List<EntityViewModel> entities);

        bool SyncScore(List<EntityViewModel> entities);
    }
}
