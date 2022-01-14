namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IHero : IBaseHero
    {
        [SitecoreField(Constants.Hero.Description_FieldID)]
        string Description { get; set; }
    }
}