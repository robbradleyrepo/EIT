using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using LionTrust.Foundation.ORM.Models;

namespace LionTrust.Feature.Breadcrumb.Models
{    
    public interface IBreadcrumbDetailsModel: IGlassBase
    {
        [SitecoreParent()]
        IBreadcrumbDetailsModel Parent { get; set; }

        [SitecoreField("Breadcrumb_IncludeInBreadcrumb")]
        bool IncludeInBreadcrumb { get; set; }

        [SitecoreField("Breadcrumb_BreadcrumbTitle")]
        string BreadcrumbTitle { get; set; }

    }
}