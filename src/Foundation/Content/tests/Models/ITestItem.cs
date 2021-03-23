using LionTrust.Foundation.ORM.Models;

namespace LionTrust.Foundation.Content.Tests.Models
{
    public interface ITestItem : IGlassBase
    {
        string Title { get; set; }
    }
}
