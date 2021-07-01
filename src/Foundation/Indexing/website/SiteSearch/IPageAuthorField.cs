namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using Sitecore.Data.Items;

    public interface IPageAuthorField: IFieldHandler
    {        
        string GetAuthorName(Item item);
    }
}
