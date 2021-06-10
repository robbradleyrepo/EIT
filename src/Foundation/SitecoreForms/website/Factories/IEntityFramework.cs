namespace LionTrust.Foundation.SitecoreForms.Factories
{
    using System.Collections.Generic;

    using Sitecore.Data.Items;

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