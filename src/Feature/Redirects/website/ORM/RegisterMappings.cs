namespace LionTrust.Feature.Redirects.ORM
{
    using Glass.Mapper.Sc.Pipelines.AddMaps;
    using LionTrust.Foundation.ORM.Extensions;

    public class RegisterMappings : AddMapsPipeline  {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("LionTrust.Feature.Redirects");
        }
    }
}
