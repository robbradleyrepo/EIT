using Sitecore.ContentSearch.Linq.Utilities;
using LionTrust.Foundation.Search.Models.ContentSearch;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Security;
using System.Linq;
using Sitecore.ContentSearch.Linq;
using LionTrust.Foundation.Search.Repositories.Interfaces;
using Sitecore.ListManagement.Services.Model;
using Sitecore.Marketing.Definitions.ContactLists;
using Sitecore.ListManagement;
using Sitecore.ListManagement.XConnect.Segmentation;
using Sitecore.ListManagement.XConnect;

namespace LionTrust.Foundation.Search.Repositories.Implementations
{
    public class ContactListSearchRepository : IContactListSearchRepository
    {
        private readonly ISegmentationService _segmentationService;
        private readonly IContactSourceFactory _contactSourceFactory;

        public ContactListSearchRepository(ISegmentationService segmentationService, IContactSourceFactory contactSourceFactory)
        {
            _segmentationService = segmentationService;
            _contactSourceFactory = contactSourceFactory;
        }

        public ContentSearchResults<ContactListSearchResultItem> GetAllActiveContactListSearchResultItems(string database = "master")
        {
            using (IProviderSearchContext context = ContentSearchManager
                                                            .GetIndex($"sitecore_marketingdefinitions_{database}")
                                                            .CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
            {
                var predicate = PredicateBuilder.True<ContactListSearchResultItem>();
                predicate = predicate.And(x => x.TemplateId == Constants.ContactList.TemplateID);
                predicate = predicate.And(x => x.Active == true);

                var query = context.GetQueryable<ContactListSearchResultItem>()
                                 .Where(predicate)
                                 .Select(x => new ContactListSearchResultItem
                                 {
                                     CreatedDate = x.CreatedDate,
                                     CreatedBy = x.CreatedBy,
                                     Name = x.Name,
                                     ItemId = x.ItemId,
                                     Type = x.Type,
                                     TypeName = x.TypeName,
                                     Updated = x.Updated
                                 })
                                 .OrderByDescending(x => x.Updated);

                var results = query.GetResults();

                if (results == null)
                {
                    return null;
                }

                return new ContentSearchResults<ContactListSearchResultItem> { SearchResults = results, TotalResults = results.TotalSearchResults };
            }
        }

        public ListModel GetListModel(ContactListSearchResultItem item, string alias = "master")
        {
            var model = new ListModel
            {
                Created = Sitecore.DateUtil.ToIsoDate(item.CreatedDate.ToLocalTime()),
                CreatedBy = item.CreatedBy,
                Id = item.ItemId.Guid.ToString("D"),
                Name = item.Name,
                Type = item.Type,
                TypeName = item.TypeName,
                Updated = Sitecore.DateUtil.ToIsoDate(item.Updated.ToLocalTime()),
            };

            var contactList = new ContactList(new ContactListDefinition(item.ItemId.Guid, alias, Sitecore.Context.Language.CultureInfo, item.Name, item.CreatedDate, item.CreatedBy));
            model.Recipients = _segmentationService.GetCount(_contactSourceFactory.GetSource(contactList.ContactListDefinition)).ToString();

            return model;
        }
    }
}