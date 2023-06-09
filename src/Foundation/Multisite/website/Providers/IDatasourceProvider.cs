﻿namespace LionTrust.Foundation.Multisite.Providers
{
    using Sitecore.Data.Items;

    public interface IDatasourceProvider
    {
        Item[] GetDatasourceLocations(Item contextItem, string name);

        Item GetDatasourceTemplate(Item contextItem, string name);
    }
}
