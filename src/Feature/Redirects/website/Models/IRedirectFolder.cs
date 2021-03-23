using LionTrust.Foundation.ORM.Models;
using System.Collections.Generic;

namespace LionTrust.Feature.Redirects.Models
{
    public interface IRedirectFolder : IRedirectGlassBase
    {
        IEnumerable<I301Redirect> Children { get; set; }
    }
}
