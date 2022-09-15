using System;
using System.Collections.Generic;

namespace LionTrust.Feature.EXM.ViewModels
{
    public class EntityViewModel
    {
        public Guid EntityId { get; set; }

        public string SalesforceEntityId { get; set; }

        public string SalesforceEntityType { get; set; }

        public string Email { get; set; }

        public List<InteractionViewModel> Interactions { get; set; }

        public bool IsUnsubscribed { get; set; }

        public IDictionary<Guid, int> Score { get; set; }

        public EntityViewModel()
        {
            Interactions = new List<InteractionViewModel>();
            Score = new Dictionary<Guid, int>()
            {
                { Foundation.Contact.Constants.TeamScore.CashflowSolutionsScore_Id, 0 },
                { Foundation.Contact.Constants.TeamScore.EconomicAdvantageScore_Id, 0 },
                { Foundation.Contact.Constants.TeamScore.GlobalEquityScore_Id, 0 },
                { Foundation.Contact.Constants.TeamScore.GlobalFixedIncomeScore_Id, 0 },
                { Foundation.Contact.Constants.TeamScore.GlobalFundamentalScore_Id, 0 },
                { Foundation.Contact.Constants.TeamScore.GlobalInnovationScore_Id, 0 },
                { Foundation.Contact.Constants.TeamScore.MultiAssetScore_Id, 0 },
                { Foundation.Contact.Constants.TeamScore.SustainableInvestmentScore_Id, 0 },
            };
        }
    }
}