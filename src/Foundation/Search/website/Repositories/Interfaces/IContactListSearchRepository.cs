using LionTrust.Foundation.Search.Models.ContentSearch;

namespace LionTrust.Foundation.Search.Repositories.Interfaces
{
    public interface IContactListSearchRepository
    {
        ContentSearchResults<ContactListSearchResultItem> GetAllActiveContactListSearchResultItems(string database = "master");
    }
}