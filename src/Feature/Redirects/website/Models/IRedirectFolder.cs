namespace LionTrust.Feature.Redirects.Models
{
    using System.Collections.Generic;

    using LionTrust.Foundation.ORM.Models;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IRedirectFolder : IRedirectGlassBase
    {
        [SitecoreQuery(Constants.RedirectsQuery, IsRelative = true)]
        IEnumerable<I301Redirect> Children { get; set; }
    }
}
