namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IHero : IHeroBase
    {
        [SitecoreField(Constants.Hero.Description_FieldID)]
        string Description { get; set; }
    }
}