using Glass.Mapper.Sc.Pipelines.AddMaps;
using LionTrust.Foundation.ORM.Extensions;

namespace LionTrust.Feature.Hero.ORM
{
    public class RegisterMappings : AddMapsPipeline  {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("LionTrust.Feature.Hero");
        }
    }
}
