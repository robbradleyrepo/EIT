namespace LionTrust.Feature.Navigation.Controllers
{
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Navigation.Models;
    using LionTrust.Feature.Navigation.Repositories;
    using Sitecore.Mvc.Controllers;
    using Sitecore.Mvc.Presentation;

    public class NavigationController : SitecoreController
    {
        private readonly INavigationRepository _navigationRepository;
        private readonly IMvcContext _mvcContext;

        public NavigationController(IMvcContext mvcContext) : this(new NavigationRepository(RenderingContext.Current.ContextItem), mvcContext)
        {
        }

        public NavigationController(INavigationRepository navigationRepository, IMvcContext mvcContext)
        {
            this._navigationRepository = navigationRepository;
            this._mvcContext = mvcContext;
        }

        public ActionResult Header()
        {
            NavigationViewModel navigationViewModel = GetNavigationViewModel();

            return View("~/Views/Navigation/Header.cshtml", navigationViewModel);
        }

        public ActionResult Footer()
        {
            var item = RenderingContext.Current.Rendering.Item;
            var homeItem = _navigationRepository.GetNavigationRoot(item);
            IHome homeModel = null;
            if (homeItem != null)
            {
                homeModel = _mvcContext.SitecoreService.GetItem<IHome>(homeItem.ID.Guid);
            }

            return View("~/Views/Navigation/Footer.cshtml", homeModel);
        }

        public ActionResult Menu()
        {
            NavigationViewModel navigationViewModel = GetNavigationViewModel();

            return View("~/Views/Navigation/Menu.cshtml", navigationViewModel);
        }

        private NavigationViewModel GetNavigationViewModel()
        {
            var navigationViewModel = new NavigationViewModel();
            var item = RenderingContext.Current.Rendering.Item;
            var rootItem = _navigationRepository.GetNavigationSiteRoot(item);
            if (rootItem != null)
            {
                navigationViewModel.RootItem = _mvcContext.SitecoreService.GetItem<ISiteRoot>(rootItem.ID.Guid);
            }

            var homeItem = _navigationRepository.GetNavigationRoot(item);
            if (homeItem != null)
            {
                navigationViewModel.HomeItem = _mvcContext.SitecoreService.GetItem<IHome>(homeItem.ID.Guid);
            }

            return navigationViewModel;
        }
    }
}