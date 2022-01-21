namespace LionTrust.Feature.Listings.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
   
    public interface IDocumentListerVariants : IListingsGlassBase
    {
        [SitecoreField(Constants.DocumentListerVariant.SelectYearLabel_FieldId)]
        string SelectYearLabel { get; set; }

        [SitecoreField(Constants.DocumentListerVariant.PressReleaseLabel_FieldId)]
        string PressReleaseLabel { get; set; }

        [SitecoreField(Constants.DocumentListerVariant.ReportLabel_FieldId)]
        string ReportLabel { get; set; }

        [SitecoreField(Constants.DocumentListerVariant.PresentationLabel_FieldId)]
        string PresentationLabel { get; set; }

        [SitecoreChildren]
        IEnumerable<IDocumentVariant> DocumentVariants { get; set; }
    }
}