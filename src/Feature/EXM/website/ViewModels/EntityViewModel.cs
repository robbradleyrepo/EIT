using LionTrust.Foundation.Contact.Models;
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

        public IDictionary<Guid, ScoreViewModel> Scores { get; set; }

        public EntityViewModel()
        {
            Interactions = new List<InteractionViewModel>();
            Scores = new Dictionary<Guid, ScoreViewModel>();
        }
    }
}