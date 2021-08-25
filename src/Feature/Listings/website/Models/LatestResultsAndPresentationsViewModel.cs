namespace LionTrust.Feature.Listings.Models
{
    using System.Collections.Generic;

    public class LatestResultsAndPresentationsViewModel
    {
        public ILatestResultsAndPresentations Data { get; set; }
        public IEnumerable<IDocumentVariant> LatestDocuments { get; set; }
    }
}