using Sitecore.ContentSearch.Linq.Utilities;
using LionTrust.Foundation.Search.Models.ContentSearch;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Security;
using System.Linq;
using Sitecore.ContentSearch.Linq;
using LionTrust.Foundation.Search.Repositories.Interfaces;

namespace LionTrust.Foundation.Search.Repositories.Implementations
{
    public class ContactListSearchRepository : IContactListSearchRepository
    {
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
    }
}