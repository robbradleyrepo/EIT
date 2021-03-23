using Glass.Mapper.Sc.Pipelines.AddMaps;
using LionTrust.Foundation.ORM.Extensions;

namespace LionTrust.Feature.Redirects.ORM
{
    public class RegisterMappings : AddMapsPipeline  {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("LionTrust.Feature.Redirects");
        }
    }
}
