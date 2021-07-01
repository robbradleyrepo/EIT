namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using Sitecore.Data.Items;

    public interface ILegacyContentField: IFieldHandler
    {
        string GetData(Item item);
    }
}
