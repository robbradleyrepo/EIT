namespace LionTrust.Feature.Listings.Models
{
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface IAccordionModel: IListingsGlassBase
    {
        IEnumerable<IAccordionRowModel> Children { get; set; }
    }
}
