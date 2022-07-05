using System;
using System.Collections.Generic;

namespace LionTrust.Feature.EXM.ViewModels
{
    public class ContactViewModel
    {
        public Guid ContactId { get; set; }

        public string SalesforceContactId { get; set; }

        public string Email { get; set; }

        public List<InteractionViewModel> Interactions { get; set; }

        public bool IsUnsubscribed { get; set; }

        public int Score { get; set; }

        public ContactViewModel()
        {
            Interactions = new List<InteractionViewModel>();
            Score = 0;
        }
    }
}