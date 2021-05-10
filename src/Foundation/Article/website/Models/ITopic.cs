namespace LionTrust.Foundation.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Article;
    using LionTrust.Foundation.Article.Models;

    public interface ITopic: IArticleGlassBase
    {
        [SitecoreField(Constants.Topic.TopicTitleFieldId)]
        string Title { get; set; }
    }
}
