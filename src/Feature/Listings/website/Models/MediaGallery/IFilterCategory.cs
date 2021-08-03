namespace LionTrust.Feature.Listings.Models.MediaGallery
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;

    public interface IFilterCategory : IListingsGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IFundTeam> FundTeams { get; set; }
    }
}