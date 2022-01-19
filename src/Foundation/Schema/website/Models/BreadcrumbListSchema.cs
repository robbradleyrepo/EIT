using System.Collections.Generic;

namespace LionTrust.Foundation.Schema.Models
{
    public class BreadcrumbListSchema
    {
        public IEnumerable<BreadcrumbItem> BreadcrumbItems { get; set; }
    }

    public class BreadcrumbItem
    {
        public int Position { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }
}