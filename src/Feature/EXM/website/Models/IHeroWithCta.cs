namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeroWithCta : IHeroWitImageBase
    {
        [SitecoreField(Constants.Hero.Cta_FieldID)]
        Link CTA { get; set; }
    }
}