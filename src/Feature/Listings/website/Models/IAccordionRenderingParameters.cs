namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Design;

    [SitecoreType(TemplateId = "{7D79C77D-C8A7-4027-80EB-4D7C316358B0}")]
    public interface IAccordionRenderingParameters
    {
        [SitecoreField(Listings.Constants.AccordionRenderingParameters.ThemeFieldId)]
        ILookupValue Theme { get; set; }

        [SitecoreField(Listings.Constants.AccordionRenderingParameters.TextAlignFieldId)]
        ILookupValue TextAlign { get; set; }

        [SitecoreField(Listings.Constants.AccordionRenderingParameters.AccordionAlignFieldId)]
        ILookupValue AccordionAlign { get; set; }
    }
}
