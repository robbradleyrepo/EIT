using LionTrust.Foundation.Search.Models.ContentSearch;
using Sitecore.ListManagement.Services.Model;

namespace LionTrust.Foundation.Search.Repositories.Interfaces
{
    public interface IContactListSearchRepository
    {
        ContentSearchResults<ContactListSearchResultItem> GetAllActiveContactListSearchResultItems(string database = "master");

        ListModel GetListModel(ContactListSearchResultItem item, string alias = "master");
    }
}