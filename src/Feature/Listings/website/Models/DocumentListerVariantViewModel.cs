namespace LionTrust.Feature.Listings.Models
{
    using System.Collections.Generic;

    public class DocumentListerVariantViewModel
    {
        public string FirstYear { get; set; }
        public List<string> Years { get; set; }
        public List<DocumentVariantYears> DocumentsByYears { get; set; }
        public IDocumentListerVariants Data { get; set; }
    }
}