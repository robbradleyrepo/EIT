namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface ICta : IExmGlassBase
    {
        [SitecoreField(Constants.Cta.CtaLink_FieldID)]
        Link CtaLink { get; set; }

        [SitecoreField(Constants.Cta.CtaText_FieldID)]
        string CtaText { get; set; }
    }
}