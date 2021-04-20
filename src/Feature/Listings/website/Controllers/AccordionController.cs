namespace LionTrust.Feature.Listings.Controllers
{    
    using LionTrust.Feature.Listings.Models;
    using LionTrust.Foundation.Content.Repositories;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;

    public class AccordionController : SitecoreController
    {
        private readonly IRenderingRepository repository;

        public AccordionController(IRenderingRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Render()
        {
            var renderingsParameters = repository.GetRenderingParameters<IAccordionRenderingParameters>();
            var datasource = repository.GetDataSourceItem<IAccordionModel>();
            if (datasource == null)
            {
                return null;
            }

            if (!datasource.Children.Any() && Sitecore.Context.PageMode.IsExperienceEditor)
            {
                return View("~/views/listings/emptyaccordion.cshtml", new AccordionViewModel { Data = datasource, RenderingData = renderingsParameters });
            }

            return View("~/views/listings/accordion.cshtml", new AccordionViewModel { Data = datasource, RenderingData = renderingsParameters });
            
        }
    }
}