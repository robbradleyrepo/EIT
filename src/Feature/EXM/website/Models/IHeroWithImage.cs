namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeroWitImage : IHeroWitImageBase
    {
        [SitecoreField(Constants.Hero.RibbonImage_FieldID)]
        Image RibbonImage { get; set; }
    }
}