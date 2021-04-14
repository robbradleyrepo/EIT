namespace LionTrust.Foundation.LocalDatasource.Infrastructure.Pipelines
{
    using System.Collections.Generic;

    using LionTrust.Foundation.LocalDatasource.Extensions;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Pipelines.GetDependencies;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    public class GetLocalDatasourceDependencies : BaseProcessor
    {
        public override void Process(GetDependenciesArgs context)
        {
            Assert.IsNotNull(context.Dependencies, "Dependencies is null");
            Item item = context.IndexedItem as SitecoreIndexableItem;
            if (item == null)
            {
                return;
            }

            if (item.IsLocalDatasourceItem())
            {
                this.AddLocalDatasourceParentDependency(item, context.Dependencies);
            }
        }

        private void AddLocalDatasourceParentDependency(Item item, List<IIndexableUniqueId> dependencies)
        {
            var localDatasourceFolder = item.GetParentLocalDatasourceFolder();
            if (localDatasourceFolder?.Parent == null)
            {
                return;
            }

            dependencies.Add((SitecoreItemUniqueId)localDatasourceFolder.Parent.Uri);
        }
    }
}