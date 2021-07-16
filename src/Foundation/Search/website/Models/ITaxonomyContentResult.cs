namespace LionTrust.Foundation.Search.Models
{
    using System;
    using System.Collections.Generic;

    public interface ITaxonomyContentResult
    {
        IEnumerable<string> Authors { get; set; }

        //Fund Category
        string Category { get; set; }

        string Content { get; set; }

        //Fund Team
        string Date { get; set; }

        string Fund { get; set; }

        IEnumerable<string> Team { get; set; }

        string Title { get; set; }

        string Subtitle { get; set; }

        IEnumerable<string> Topics { get; set; }

        string Url { get; set; }

        string FundId { get; set; }
    }
}