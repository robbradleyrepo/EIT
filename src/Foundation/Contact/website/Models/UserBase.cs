using System.Collections.Generic;

namespace LionTrust.Foundation.Contact.Models
{
    public abstract class UserBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsUKResident { get; set; }
        public string Company { get; set; }
        public IEnumerable<string> SalesforceFundIds { get; set; }
    }
}
