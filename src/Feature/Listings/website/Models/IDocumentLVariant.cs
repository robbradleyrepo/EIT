namespace LionTrust.Feature.Listings.Models
{
    using System;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface IDocumentVariant : IListingsGlassBase
    {
        [SitecoreField(Constants.DocumentVariant.Title_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.DocumentVariant.PressReleaseDocument_FieldID)]
        Link PressReleaseDocument { get; set; }

        [SitecoreField(Constants.DocumentVariant.ReportDocument_FieldID)]
        Link ReportDocument { get; set; }

        [SitecoreField(Constants.DocumentVariant.PresentationLink_FieldID)]
        Link PresentationLink { get; set; }

        [SitecoreField(Constants.DocumentVariant.Date_FieldID)]
        DateTime Date { get; set; }

        [SitecoreField(Foundation.Legacy.Constants.Document.Created_FieldId)]
        DateTime Created { get; set; }
    }
}