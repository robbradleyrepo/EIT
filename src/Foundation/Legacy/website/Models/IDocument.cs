namespace LionTrust.Foundation.Legacy.Models
{
    using System;
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface IDocument : ILegacyGlassBase
    {        
        [SitecoreField(Constants.Document.DocumentName_FieldId)]
        string DocumentName { get; set; }

        [SitecoreField(Constants.Document.DownloadLink_FieldId)]
        Link DocumentLink { get; set; }

        [SitecoreField(Constants.Document.DocumentTypes_FieldId)]
        IEnumerable<ILookupItem> DocumentTypes { get; set; }

        [SitecoreField(Constants.Document.RelatedFunds_FieldId)]
        IEnumerable<IFund> RelatedFunds { get; set; }

        [SitecoreField(Constants.Document.Archived_FieldId)]
        bool Archived { get; set; }

        [SitecoreField(Constants.Document.CustomSortOrder_FieldId)]
        int CustomSortOrder { get; set; }

        [SitecoreField(Constants.Document.DocumentKeywords_FieldId)]
        string DocumentKeywords { get; set; }

        [SitecoreField(Constants.Document.ArchivedDate_FieldId)]
        DateTime ArchivedDate { get; set; }

        [SitecoreField(Constants.Document.VideoLink_FieldId)]
        Link VideoLink { get; set; }
    }
}