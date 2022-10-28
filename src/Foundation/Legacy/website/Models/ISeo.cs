using Glass.Mapper.Sc.Configuration.Attributes;
using LionTrust.Foundation.ORM.Models;

namespace LionTrust.Foundation.Legacy.Models
{
    public interface ISeo : ILegacyGlassBase
    {
        [SitecoreField(Constants.Seo.LegacyPresentationBase_BrowserTitle_FieldId)]
        string BrowserTitle { get; set; }

        [SitecoreField(Constants.Seo.LegacyPresentationBase_MetaDescription_FieldId)]
        string MetaDescription { get; set; }

        [SitecoreField(Constants.Seo.LegacyPresentationBase_MetaKeywords_FieldId)]
        string MetaKeywords { get; set; }

        [SitecoreField(Constants.Seo.LegacyPresentationBase_MetaRobots_FieldId)]
        IRobot MetaRobots { get; set; }
    }
}