using Glass.Mapper.Sc.Maps;
using LionTrust.Feature.Hero.Models;

namespace LionTrust.Feature.Hero.ORM
{
    public class GlassMappings : SitecoreGlassMap<IHero>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
                config.TemplateId(Constants.Hero.TemplateId);
                config.Field(f => f.HeroTitle).FieldName("Hero Title");
                config.Field(f => f.HeroImages).FieldName("Hero Images");
            });
        }
    }

    public class GlassBaseMappings : SitecoreGlassMap<IHeroGlassBase>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
            });
        }
    }
}
