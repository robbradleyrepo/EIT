using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LionTrust.Foundation.Search.Models.Request
{
    public class GenericSearchRequest : ISearchRequest
    {
        public string DatabaseName { get; set; }

        public string Parent { get; set; }

        public IEnumerable<string> Type { get; set; }

        public IEnumerable<int> Years { get; set; }

        public IEnumerable<int> Months { get; set; }

        public string SearchTerm { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}