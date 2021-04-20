namespace LionTrust.Feature.Navigation.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IBreadcrumbDetailsModel: IGlassBase
    {
        [SitecoreParent()]
        IBreadcrumbDetailsModel Parent { get; set; }

        [SitecoreField(Constants.Breadcrumb.IncludeInBreadcrumb)]
        bool IncludeInBreadcrumb { get; set; }

        [SitecoreField(Constants.Breadcrumb.BreadcrumbTitle)]
        string BreadcrumbTitle { get; set; }
    }
}