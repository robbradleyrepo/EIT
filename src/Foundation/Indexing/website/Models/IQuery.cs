﻿namespace LionTrust.Foundation.Indexing.Models
{
    using System.Collections.Generic;

    public interface IQuery
    {
        string QueryText { get; set; }
        int NoOfResults { get; set; }
        Dictionary<string, string[]> Facets { get; set; }
        int Page { get; set; }
        bool SimilarResults { get; set; }
    }
}