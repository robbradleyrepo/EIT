namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface ITopic: IGlassBase
    {
        [SitecoreField(Constants.Topic.TopicTitleFieldId)]
        string Title { get; set; }
    }
}
