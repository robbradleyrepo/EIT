using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using Sitecore.Mvc.Controllers;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailHeroController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailHeroController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult Hero()
        {
            var model = _mvcContext.GetDataSourceItem<IHero>();
            return View("~/Views/EXM/Hero.cshtml", model);
        }

        public ActionResult HeroLogo()
        {
            var model = _mvcContext.GetDataSourceItem<IHeroLogo>();
            return View("~/Views/EXM/HeroLogo.cshtml", model);
        }

        public ActionResult HeroWithCta()
        {
            var model = _mvcContext.GetDataSourceItem<IHeroWithCta>();
            return View("~/Views/EXM/HeroWithCta.cshtml", model);
        }

        public ActionResult HeroWithDescriptionAndCta()
        {
            var model = _mvcContext.GetDataSourceItem<IHeroWithDescriptionAndCta>();
            return View("~/Views/EXM/HeroWithDescriptionAndCta.cshtml", model);
        }
    }
}