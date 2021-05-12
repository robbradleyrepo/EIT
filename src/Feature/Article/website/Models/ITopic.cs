namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ITopic: IArticleGlassBase
    {
        [SitecoreField(Constants.Topic.TopicTitleFieldId)]
        string Title { get; set; }
    }
}
