namespace LionTrust.Feature.Hero.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IHero : IHeroGlassBase
    {
        [SitecoreField(Constants.Hero.HeroTitleFieldId)]
        string HeroTitle { get; set; }

        [SitecoreField(Constants.Hero.HeroImagesFieldId)]
        IEnumerable<Image> HeroImages { get; set; }
    }
}
