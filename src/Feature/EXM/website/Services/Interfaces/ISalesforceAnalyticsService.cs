using FuseIT.Sitecore.SalesforceConnector.Entities;
using LionTrust.Feature.EXM.ViewModels;
using System;
using System.Collections.Generic;

namespace LionTrust.Feature.EXM.Services.Interfaces
{
    public interface ISalesforceAnalyticsService
    {
        List<EntityViewModel> GetEntityWithInteractions(DateTime fromDate);

        bool SyncEngagementHistory(List<EntityViewModel> entities);

        bool SyncScore(List<EntityViewModel> entities);
    }
}
