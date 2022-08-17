namespace LionTrust.Feature.Article.Fields
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using Priority = Foundation.Indexing.Priority;

    [Service(ServiceType = typeof(IPriority), Lifetime = Lifetime.Singleton)]
    public class ArticlePriorityField : ArticleBaseField, IPriority
    {
        public int GetPriority()
        {
            return (int)Priority.Article;
        }
    }
}