namespace LionTrust.Feature.Listings.Models
{
    using System.Collections.Generic;

    public class DocumentListerVariantViewModel
    {
        public List<DocumentVariantYears> DocumentsByYears { get; set; }
        public IDocumentListerVariants Data { get; set; }
    }
}