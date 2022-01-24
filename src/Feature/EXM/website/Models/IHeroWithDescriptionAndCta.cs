namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IHeroWithDescriptionAndCta : IHero, IHeroWithCta
    {
        [SitecoreField(Constants.HeroWithDescriptionAndCTA.SubTitle_FieldID)]
        string SubTitle { get; set; }
    }
}