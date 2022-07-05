using FuseIT.Sitecore.SalesforceConnector.Entities;
using LionTrust.Feature.EXM.ViewModels;
using System;
using System.Collections.Generic;

namespace LionTrust.Feature.EXM.Services.Interfaces
{
    public interface ISalesforceAnalyticsService
    {
        List<ContactViewModel> GetContactWithInteractions(DateTime fromDate);

        List<GenericSalesforceEntity> GetSalesforceEntities(List<ContactViewModel> contacts);

        void SyncData(List<GenericSalesforceEntity> entities);

        //InteractionsViewModel GetInteractionsViewModel();
    }
}
