namespace LionTrust.Foundation.Contact.Models
{
    using System.Collections.Generic;

    public class ScContactFacetData
    {
        public ScContactFacetData()
        {
            SalesforceFundIds = new List<string>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SalesforceEntityId { get; set; }
        public string SalesforceOrgId { get; set; }
        public string RandomGuidFromSalesforceEntity { get; set; }
        public List<string> SalesforceFundIds { get; set; }
    }
}

