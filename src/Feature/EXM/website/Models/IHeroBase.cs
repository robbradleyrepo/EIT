namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IHeroBase : IHeroLogo
    {
        [SitecoreField(Constants.Hero.Title_FieldID)]
        string Title { get; set; }
    }
}