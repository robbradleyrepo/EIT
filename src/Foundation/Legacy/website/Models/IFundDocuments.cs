namespace LionTrust.Foundation.Legacy.Models
{
    using System.Collections.Generic;

    public interface IFundDocuments: ILegacyGlassBase
    {
        IEnumerable<IDocument> Children { get; set; }
    }
}
