using System;
using System.Collections.Generic;

namespace LionTrust.Foundation.Schema.Models
{
    public class OrganizationSchema
    {
        public string Url { get; set; }
        public string LogoUrl { get; set; }
        public int LogoHeight { get; set; }
        public int LogoWidth { get; set; }
        public string Name { get; set; }
        public string ContactType { get; set; }
        public string AreaServed { get; set; }
        public string Description { get; set; }
        public List<Uri> SameAs { get; set; }
        public string Telephone { get; set; }       
        public string Email { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string PostalCode { get; set; }
    }
}