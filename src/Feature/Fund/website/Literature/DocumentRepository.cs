namespace LionTrust.Feature.Fund.Literature
{
    using Glass.Mapper.Sc;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Security;
    using Sitecore.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(IDocumentRepository), Lifetime = Lifetime.Transient)]
    public class DocumentRepository : IDocumentRepository
    {
        private readonly string databaseName;
        private readonly ISitecoreService sitecoreService;
        private readonly string templateId;

        public DocumentRepository(ISitecoreService sitecoreService)
        {
            this.databaseName = sitecoreService.Database.Name;
            this.sitecoreService = sitecoreService;
            templateId = new Guid(Foundation.Legacy.Constants.Document.TemplateId).ToString("N");
        }

        public IEnumerable<IDocument> GetRelatedDocuments(IFund fund)
        {
            if (fund == null || fund.DocumentsFolder == null || fund.DocumentsFolder.Children == null)
            {
                return new IDocument[0];
            }

            var links = GetRelatedDouments(fund, databaseName);
            var result = fund.DocumentsFolder.Children.ToList();
            result.AddRange(links.Select(l =>
                            {
                                var options = new GetItemByIdOptions(l.ItemId.Guid);
                                return sitecoreService.GetItem<IDocument>(options);
                            }));

            return result;
        }

        /// <summary>
        /// Get documents that are related to the fund but live in the common location
        /// </summary>
        /// <param name="fund"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        private List<DocumentSearchResult> GetRelatedDouments(IFund fund, string databaseName)
        {
            var indexedFundId = fund.Id.ToString("N");
            var fundId = new ID(fund.Id);
            using (var context = ContentSearchManager
                                                            .GetIndex($"sitecore_{databaseName}_index")
                                                            .CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
            {
                return context.GetQueryable<DocumentSearchResult>()
                    .Where(r => r.AllTemplates.Contains(templateId))
                    .Where(r => !r.Paths.Contains(fundId))
                    .Where(r => r.RelatedFunds != null && r.RelatedFunds.Contains(indexedFundId)).ToList();                
            }
        }
    }
}