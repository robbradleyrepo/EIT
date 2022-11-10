namespace LionTrust.Feature.Navigation.Repositories
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Navigation.Models;
    using LionTrust.Foundation.Schema.Models;
    using Sitecore.Data.Items;

    public interface INavigationRepository
    {
        Item GetNavigationRoot(Item contextItem);

        OrganizationSchema GetOrganizationData(IHomeBase home, IMvcContext mvcContext);        
    }
}