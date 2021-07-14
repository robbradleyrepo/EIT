namespace LionTrust.Feature.Listings.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using LionTrust.Feature.Listings.DataManagers.Interfaces;
    using LionTrust.Foundation.Core.ActionResults;
    using Sitecore.Mvc.Controllers;

    public class GenericListingApiController : SitecoreController
    {
        private readonly IGenericListingDataManager _genericListingDataManager;

        public GenericListingApiController(IGenericListingDataManager genericListingDataManager)
        {
            this._genericListingDataManager = genericListingDataManager;
        }

        public ActionResult GetFacets(string articleListingFacetConfig)
        {
            Guid config;
            if (!string.IsNullOrEmpty(articleListingFacetConfig))
            {
                var success = Guid.TryParse(articleListingFacetConfig, out config);
                if (!success)
                {
                    return Content("Configuration ID could not be parsed as a Guid");
                }
                var response = _genericListingDataManager.GetGenericListingFilterFacets(config);
                return new JsonCamelCaseResult(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpNotFoundResult();
            }           
        }

        public ActionResult GetFilteredResults(string parentId, string listingType = "", List<int> months = null, List<int> years = null, string searchTerm = "", string database = "web", int page = 1)
        {
            var response = this._genericListingDataManager.GetGenericListingResponse(database, parentId, listingType, months?.ToList(), years?.ToList(), searchTerm, page);
            if (response.StatusCode != 200)
            {
                return new HttpStatusCodeResult(response.StatusCode, response.StatusMessage);
            }
            var responseJson = Json(response);
            return new JsonCamelCaseResult(response, JsonRequestBehavior.AllowGet);
        }
    }
}