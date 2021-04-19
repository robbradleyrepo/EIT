namespace LionTrust.Feature.Breadcrumb.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IBreadcrumbDetailsModel: IGlassBase
    {
        [SitecoreParent()]
        IBreadcrumbDetailsModel Parent { get; set; }

        [SitecoreField(Constants.FieldIds.IncludeInBreadcrumb)]
        bool IncludeInBreadcrumb { get; set; }

        [SitecoreField(Constants.FieldIds.BreadcrumbTitle)]
        string BreadcrumbTitle { get; set; }
    }
}