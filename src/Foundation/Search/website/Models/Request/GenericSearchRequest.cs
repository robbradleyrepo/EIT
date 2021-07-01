using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LionTrust.Foundation.Search.Models.Request
{
    public class GenericSearchRequest
    {
        public string DatabaseName { get; set; }

        public DateTime FromDate { get; set; }

        public string Parent { get; set; }

        public IEnumerable<string> ListingType { get; set; }

        public string SearchTerm { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public DateTime ToDate { get; set; }
    }
}