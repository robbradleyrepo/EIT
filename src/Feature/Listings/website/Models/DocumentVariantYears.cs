namespace LionTrust.Feature.Listings.Models
{
    using System;
    using System.Collections.Generic;

    public class DocumentVariantYears
    {
        public string Year { get; set; }
        public List<IDocumentVariant> Documents { get; set; }
    }
}