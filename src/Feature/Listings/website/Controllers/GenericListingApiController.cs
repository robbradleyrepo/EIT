namespace LionTrust.Feature.Listings.Controllers
{
    using System;
    using System.Web.Mvc;
    using LionTrust.Feature.Listings.DataManagers.Interfaces;
    using Sitecore.Mvc.Controllers;

    public class GenericListingApiController : SitecoreController
    {
        private readonly IGenericListingDataManager _genericListingDataManager;

        public GenericListingApiController(IGenericListingDataManager genericListingDataManager)
        {
            this._genericListingDataManager = genericListingDataManager;
        }

        public ActionResult Facets(string articleListingFacetConfig)
        {
            Guid config;
            if (string.IsNullOrEmpty(articleListingFacetConfig))
            {
                config = new Guid(Constants.Defaults.GenericListingFacetsConfig);
            }
            else
            {
                var success = Guid.TryParse(articleListingFacetConfig, out config);
                if (!success)
                {
                    return Content("Configuration ID could not be parsed as a Guid");
                }
            }

            var response = _genericListingDataManager.GetGenericListingFilterFacets(config);
            if (response == null)
            {
                return new HttpNotFoundResult();
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFilteredResults(string listingType, int? month, int? year, string searchTerm, string database = "web", int page = 1)
        {
            var response = this._genericListingDataManager.GetGenericListingResponse(database, listingType, month, year, searchTerm, page);
            if (response.StatusCode != 200)
            {
                return new HttpStatusCodeResult(response.StatusCode, response.StatusMessage);
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}