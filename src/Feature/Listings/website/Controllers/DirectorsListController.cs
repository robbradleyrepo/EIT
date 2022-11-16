namespace LionTrust.Feature.Listings.Controllers
{
    using Glass.Mapper.Sc;
    using LionTrust.Feature.Listings.Models;
    using LionTrust.Foundation.Content.Repositories;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;

    public class DirectorsListController : SitecoreController
    {
        private readonly IRenderingRepository _repository;
        private readonly ISitecoreService _sitecoreService;

        public DirectorsListController(IRenderingRepository repository, ISitecoreService sitecoreService)
        {
            _repository = repository;
            _sitecoreService = sitecoreService;
        }

        public ActionResult DirectorsListRender()
        {
            var datasource = _repository.GetDataSourceItem<IDirectorsList>();
            var settings = _sitecoreService.GetItem<IDirectorSettings>(Constants.DirectorSettings.DirectorSettings_Id);

            var model = new DirectorsListViewModel
            {
                Data = datasource,
                EmailLabel = settings.EmailLabel,
                DirectLineLabel = settings.DirectLineLabel,
                MobileLabel = settings.MobileLabel,
                Children = datasource.DirectorsList?.Select(x =>
                            new DirectorViewModel
                            {
                                Data = x,
                                EmailLabel = settings.EmailLabel,
                                DirectLineLabel = settings.DirectLineLabel
                            })
            };

            return View("~/views/listings/directorslist.cshtml", model);

        }

        public ActionResult DirectorsListVariantRender()
        {
            var datasource = _repository.GetDataSourceItem<IDirectorsList>();
            var settings = _sitecoreService.GetItem<IDirectorSettings>(Constants.DirectorSettings.DirectorSettings_Id);

            var model = new DirectorsListViewModel
            {
                Data = datasource,
                EmailLabel = settings.EmailLabel,
                DirectLineLabel = settings.DirectLineLabel,
                MobileLabel = settings.MobileLabel,
                Children = datasource.DirectorsList?.Select(x =>
                            new DirectorViewModel
                            {
                                Data = x,
                                EmailLabel = settings.EmailLabel,
                                DirectLineLabel = settings.DirectLineLabel
                            })
            };

            return View("~/views/listings/directorslistvariant.cshtml", model);
        }

        public ActionResult DirectorsListWithOverlayRender()
        {
            var datasource = _repository.GetDataSourceItem<IDirectorsList>();
            var settings = _sitecoreService.GetItem<IDirectorSettings>(Constants.DirectorSettings.DirectorSettings_Id);

            var model = new DirectorsListViewModel
            {
                Data = datasource,
                EmailLabel = settings.EmailLabel,
                DirectLineLabel = settings.DirectLineLabel,
                MobileLabel = settings.MobileLabel,
                Children = datasource.DirectorsList?.Select(x => 
                            new DirectorViewModel {
                                Data = x,
                                Header = settings.Header?.Replace(Constants.DirectorSettings.FirstNameToken, x.FirstName),
                                ImageOverlay = x.ImageOverlay ?? x.Image,
                                ViewMoreLabel = settings.ViewMoreLabel,
                                EmailLabel = settings.EmailLabel,
                                DirectLineLabel = settings.DirectLineLabel,
                                LinkedInLabel = settings.LinkedInLabel?.Replace(Constants.DirectorSettings.FirstNameToken, x.FirstName).ToUpper(),
                                LinkedInImage = settings.LinkedInImage
                            })
            };
            
            return View("~/views/listings/directorslistwithoverlay.cshtml", model);            
        }
    }
}