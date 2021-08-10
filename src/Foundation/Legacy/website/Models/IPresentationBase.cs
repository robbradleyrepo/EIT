namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System.Collections.Generic;

    public interface IPresentationBase : ILegacyGlassBase
    {
        [SitecoreField(Constants.PresentationBase.PageTitle_FieldId, SitecoreFieldType.SingleLineText, "Page standard data")]
        string PageTitle { get; set; }

        [SitecoreField(Constants.PresentationBase.ShortDescription_FieldId, SitecoreFieldType.MultiLineText, "Page standard data")]
        string PageShortDescription { get; set; }

        [SitecoreField(Constants.PresentationBase.Tag_FieldId, SitecoreFieldType.MultiLineText, "Search data")]
        IEnumerable<ITag> PageTags { get; set; }

        [SitecoreField(Constants.PresentationBase.ExcludeInSearchResults_FieldId, SitecoreFieldType.Checkbox, "Search data")]
        bool ExcludeInSearchResults { get; set; }

        [SitecoreField(Constants.FundPage.FundReference_FieldId)]
        IFund Fund { get; set; }
    }
}