namespace LionTrust.Feature.Article.Fields
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;

    [Service(ServiceType = typeof(ISearchResultType), Lifetime = Lifetime.Singleton)]
    public class ArticleSearchResultTypeField : ArticleBaseField, ISearchResultType
    {
        public string GetSearchResultType()
        {
            return  Sitecore.Globalization.Translate.Text("Article");
        }
    }
}