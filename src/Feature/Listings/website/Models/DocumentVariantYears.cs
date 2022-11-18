namespace LionTrust.Feature.Listings.Models
{
    using System;
    using System.Collections.Generic;

    public class DocumentVariantYears
    {
        public Guid Id { get; set; }

        public string Year { get; set; }

        public bool IsLatestYear { get; set; }

        public List<IDocumentVariant> Documents { get; set; }
    }
}