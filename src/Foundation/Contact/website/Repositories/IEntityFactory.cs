namespace LionTrust.Foundation.Contact.Repositories
{
    using Sitecore.Data.Items;
    using System.Collections.Generic;

    public interface IEntityFactory
    {
        /// <summary>
        /// Build entity. Supported types: string,datetime, int, iproperty(link,image,string)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        T Build<T>(Item item);
        T Build<T>(Item item, IDictionary<string, string> renderingParameters);
    }
}