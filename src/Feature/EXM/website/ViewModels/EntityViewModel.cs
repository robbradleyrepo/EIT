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

        public int Score { get; set; }

        public EntityViewModel()
        {
            Interactions = new List<InteractionViewModel>();
            Score = 0;
        }
    }
}