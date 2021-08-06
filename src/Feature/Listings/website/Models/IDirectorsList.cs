namespace LionTrust.Feature.Listings.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    
    public interface IDirectorsList : IListingsGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IDirector> DirectorsList { get; set; }

        [SitecoreField(Constants.DirectorList.Title_FieldId)]
        string Title { get; set; }
    }
}