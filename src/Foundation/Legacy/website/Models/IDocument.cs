namespace LionTrust.Foundation.Legacy.Models
{
    using System;
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface IDocument : ILegacyGlassBase
    {        
        [SitecoreField(Constants.Document.DocumentName_FieldId, SitecoreFieldType.SingleLineText, "Document content")]
        string DocumentName { get; set; }

        [SitecoreField(Constants.Document.DownloadLink_FieldId, SitecoreFieldType.GeneralLink, "Document content")]
        Link DocumentLink { get; set; }

        [SitecoreField(Constants.Document.DocumentTypes_FieldId, SitecoreFieldType.Multilist, "Document content")]
        IEnumerable<ILookupItem> DocumentTypes { get; set; }

        [SitecoreField(Constants.Document.RelatedFunds_FieldId, SitecoreFieldType.Multilist, "Document content")]
        IEnumerable<IFund> RelatedFunds { get; set; }

        [SitecoreField(Constants.Document.Archived_FieldId, SitecoreFieldType.Checkbox, "Document content")]
        bool Archived { get; set; }

        [SitecoreField(Constants.Document.CustomSortOrder_FieldId, SitecoreFieldType.Integer, "Document content")]
        int CustomSortOrder { get; set; }

        [SitecoreField(Constants.Document.DocumentKeywords_FieldId, SitecoreFieldType.SingleLineText, "Document content")]
        string DocumentKeywords { get; set; }

        [SitecoreField(Constants.Document.ArchivedDate_FieldId, SitecoreFieldType.Date, "Archive")]
        DateTime ArchivedDate { get; set; }
    }
}