namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IPresentationBase : ILegacyGlassBase
    {
        [SitecoreField(Constants.PresentationBase.PageTitle_FieldId, SitecoreFieldType.SingleLineText, "Page standard data")]
        string PageTitle { get; set; }

        [SitecoreField(Constants.PresentationBase.ShortDescription_FieldId, SitecoreFieldType.MultiLineText, "Page standard data")]
        string PageShortDescription { get; set; }
    }
}