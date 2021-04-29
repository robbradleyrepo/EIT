using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace LionTrust.Feature.Navigation.Models
{
    public interface ICopyWithCta: INavigationGlassBase
    {
        [SitecoreField(Constants.CopyWithCta.CopyFieldId)]
        string Copy { get; set; }

        [SitecoreField(Constants.CopyWithCta.CtaFieldId)]
        Link Cta { get; set; }
    }
}
