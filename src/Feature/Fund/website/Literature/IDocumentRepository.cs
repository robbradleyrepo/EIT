namespace LionTrust.Feature.Fund.Literature
{
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    public interface IDocumentRepository
    {
        IEnumerable<IDocument> GetRelatedDocuments(IFund fund);
    }
}
