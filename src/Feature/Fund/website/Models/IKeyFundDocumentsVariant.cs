namespace LionTrust.Feature.Fund.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Legacy.Models;
    
    public interface IKeyFundDocumentsVariant : IFundGlassBase
    {
        [SitecoreField(Constants.KeyFundDocumentsVariant.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.KeyFundDocumentsVariant.CtaLink_FieldId)]
        Link CtaLink { get; set; }

        [SitecoreField(Constants.KeyFundDocumentsVariant.KeyDocuments_FieldId)]
        IEnumerable<IDocument> KeyDocuments { get; set; }

        [SitecoreField(Constants.KeyFundDocumentsVariant.ViewAllDocumentsLabel_FieldId)]
        string ViewAllDocumentsLabel { get; set; }

        [SitecoreField(Constants.KeyFundDocumentsVariant.DownloadLabel_FieldId)]
        string DownloadLabel { get; set; }
    }
}