namespace LionTrust.Foundation.Legacy.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    
    public interface IDocumentFolder : ILegacyGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IDocument> DocumentList { get; set; }

        [SitecoreField(Constants.DocumentFolder.NameText_FieldId)]
        string NameText { get; set; }

        [SitecoreField(Constants.DocumentFolder.DownloadText_FieldId)]
        string DownloadText { get; set; }

        [SitecoreField(Constants.DocumentFolder.DownloadLinkText_FieldId)]
        string DownloadLinkText { get; set; }
    }
}