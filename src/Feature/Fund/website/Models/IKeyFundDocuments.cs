namespace LionTrust.Feature.Fund.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    
    public interface IKeyFundDocuments : IFundGlassBase
    {
        [SitecoreField(Constants.KeyFundDocuments.Title_FieldId, SitecoreFieldType.SingleLineText, "Key Fund Documents")]
        string Title { get; set; }

        [SitecoreField(Constants.KeyFundDocuments.Description_FieldId, SitecoreFieldType.MultiLineText, "Key Fund Documents")]
        string Description { get; set; }

        [SitecoreField(Constants.KeyFundDocuments.KeyDocuments_FieldId, SitecoreFieldType.Treelist, "Key Fund Documents")]
        IEnumerable<IDocument> KeyDocuments { get; set; }

        [SitecoreField(Constants.KeyFundDocuments.ViewAllDocumentsLabel_FieldId, SitecoreFieldType.SingleLineText, "Key Fund Documents")]
        string ViewAllDocumentsLabel { get; set; }
    }
}