namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHeroLogo : IExmGlassBase
    {
        [SitecoreField(Constants.Hero.Logo_FieldID)]
        Image Logo { get; set; }

        [SitecoreField(Constants.Hero.LogoLink_FieldID)]
        Link LogoLink { get; set; }
    }
}