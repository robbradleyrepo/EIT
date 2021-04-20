namespace LionTrust.Feature.Listings.Models
{
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface IAccordionModel: IGlassBase
    {
        IEnumerable<IAccordionRowModel> Children { get; set; }
    }
}
