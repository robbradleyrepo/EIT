using LionTrust.Foundation.Content.Repositories;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;
namespace LionTrust.Feature.Newsletter.Promo
{
    public class NewsletterPromoController : SitecoreController
    {
        private readonly IRenderingRepository renderingRepository;

        public NewsletterPromoController(IRenderingRepository renderingRepository)
        {
            this.renderingRepository = renderingRepository;
        }

        public ActionResult Render()
        {            
            var model = renderingRepository.GetDataSourceItem<INewsletterPromoModel>();
            if (model == null)
            {
                return null;
            }

            return View("~/views/newsletter/promo.cshtml", new NewsletterPromoViewModel(model));
        }
    }
}