using Glass.Mapper.Sc.Pipelines.AddMaps;
using LionTrust.Foundation.ORM.Extensions;

namespace LionTrust.Foundation.ORM.Mappings
{
    public class RegisterMappings : AddMapsPipeline  {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("LionTrust.Foundation.ORM");
        }
    }
}
