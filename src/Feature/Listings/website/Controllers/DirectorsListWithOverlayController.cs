namespace LionTrust.Feature.Listings.Controllers
{
    using Glass.Mapper.Sc;
    using LionTrust.Feature.Listings.Models;
    using LionTrust.Foundation.Content.Repositories;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;

    public class DirectorsListWithOverlayController : SitecoreController
    {
        private readonly IRenderingRepository _repository;
        private readonly ISitecoreService _sitecoreService;

        public DirectorsListWithOverlayController(IRenderingRepository repository, ISitecoreService sitecoreService)
        {
            _repository = repository;
            _sitecoreService = sitecoreService;
        }

        public ActionResult Render()
        {
            var datasource = _repository.GetDataSourceItem<IDirectorsList>();
            var settings = _sitecoreService.GetItem<IDirectorSettings>(Constants.DirectorSettings.DirectorSettings_Id);

            var model = new DirectorsListViewModel
            {
                Data = datasource,
                LinkedInImage = settings.LinkedInImage,
                Children = datasource.DirectorsList?.Select(x => 
                            new DirectorViewModel {
                                Data = x,
                                Header = settings.Header?.Replace(Constants.DirectorSettings.FirstNameToken, x.FirstName),
                                LinkedInLabel = settings.LinkedInLabel?.Replace(Constants.DirectorSettings.FirstNameToken, x.FirstName).ToUpper()
                            })
            };
            
            return View("~/views/listings/directorslistwithoverlay.cshtml", model);
            
        }
    }
}