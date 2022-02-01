namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeroWithImageBase : IHeroBase
    {
        [SitecoreField(Constants.Hero.Image_FieldID)]
        Image Image { get; set; }
    }
}