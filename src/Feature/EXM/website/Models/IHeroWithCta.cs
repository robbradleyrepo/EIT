namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeroWithCta : IBaseHero
    {
        [SitecoreField(Constants.HeroWithCTA.Image_FieldID)]
        Image Image { get; set; }

        [SitecoreField(Constants.HeroWithCTA.Cta_FieldID)]
        Link CTA { get; set; }
    }
}