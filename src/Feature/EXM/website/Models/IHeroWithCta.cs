namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeroWithCta : IHeroWitImageBase
    {
        [SitecoreField(Constants.Hero.CtaLink_FieldID)]
        Link CtaLink { get; set; }

        [SitecoreField(Constants.Hero.CtaText_FieldID)]
        string CtaText { get; set; }
    }
}