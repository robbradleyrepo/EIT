namespace LionTrust.Feature.Navigation.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Navigation.Models;
    using LionTrust.Feature.Navigation.Repositories;
    using LionTrust.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Data;
    using Sitecore.Mvc.Presentation;

    public class NavigationController : Controller
    {
        private readonly INavigationRepository _navigationRepository;
        private readonly IMvcContext _mvcContext;

        public NavigationController(INavigationRepository navigationRepository, IMvcContext mvcContext)
        {
            this._navigationRepository = navigationRepository;
            this._mvcContext = mvcContext;
        }

        public ActionResult Header()
        {
            var navigationViewModel = new NavigationViewModel();
            var item = RenderingContext.Current.Rendering.Item;
            var rootItem = _navigationRepository.GetSiteRootIdentity(item); 
            if (rootItem != null)
            {
                navigationViewModel.RootItem = _mvcContext.SitecoreService.GetItem<ISiteRoot>(rootItem.ID.Guid);                
            }
            
            var homeItem = _navigationRepository.GetNavigationRoot(item);            
            if (homeItem != null)
            {
                navigationViewModel.HomeItem = _mvcContext.SitecoreService.GetItem<IHome>(homeItem.ID.Guid);
            }
            
            return View("~/Views/Navigation/Header.cshtml", navigationViewModel);
        }
    }
}