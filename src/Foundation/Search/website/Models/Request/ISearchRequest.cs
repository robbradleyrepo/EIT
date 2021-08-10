namespace LionTrust.Foundation.Search.Models.Request
{
    public interface ISearchRequest
    {
        string DatabaseName { get; set; }

        string SearchTerm { get; set; }

        int Skip { get; set; }

        int Take { get; set; }

        string Country { get; set; }
    }
}