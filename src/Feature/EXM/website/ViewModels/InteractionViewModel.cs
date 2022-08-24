using LionTrust.Foundation.Contact.Enums;
using System;

namespace LionTrust.Feature.EXM.ViewModels
{
    public class InteractionViewModel
    {
        public Guid EntityId { get; set; }

        public EntityType EntityType { get; set; }

        public string SalesforceEntityId { get; set; }

        public Guid SitecoreCampaignId { get; set; }

        public string SalesforceCampaignId { get; set; }

        public string Email { get; set; }

        public string MessageName { get; set; }

        public string MessageLink { get; set; }

        public string ContactList { get; set; }

        public DateTime Date { get; set; }

        public InteractionType Type { get; set; }

        public string Link { get; set; }

        public int Score { get; set; }
    }
}