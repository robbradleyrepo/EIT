using System;

namespace LionTrust.Foundation.Contact.Models
{
    public class ScoreViewModel
    {
        public Guid Id { get; set; }

        public string SalesforceFieldId { get; set; }

        public int Score { get; set; }

        public ScoreViewModel()
        {
            Score = 0;
        }
    }
}