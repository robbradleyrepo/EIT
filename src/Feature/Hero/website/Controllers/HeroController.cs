namespace LionTrust.Feature.Hero.Controllers
{
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Hero.Mediators;
    using LionTrust.Feature.Hero.Models;
    using LionTrust.Foundation.Core.Exceptions;
    using Sitecore.Mvc.Controllers;

    public class HeroController : SitecoreController
    {
        private readonly IHeroMediator _heroMediator;
        private readonly IMvcContext _mvcContext;

        public HeroController(IHeroMediator heroMediator, IMvcContext mvcContext)
        {
            _heroMediator = heroMediator;
            _mvcContext = mvcContext;
        }

        public ActionResult FundManagerHero()
        {
            var model = _mvcContext.GetContextItem<IFundManagerHero>();

            return View(Feature.Hero.Constants.Views.FundManagerHeroView, model);
        }

        public ActionResult Hero()
        {
            var mediatorResponse = _heroMediator.RequestHeroViewModel();

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.HeroResponse.DataSourceError:
                case MediatorCodes.HeroResponse.ViewModelError:
                    return View("~/views/Hero/Error.cshtml");
                case MediatorCodes.HeroResponse.Ok:
                    return View(mediatorResponse.ViewModel);
                default:
                    throw new InvalidMediatorResponseCodeException(mediatorResponse.Code);
            }
        }
    }
}
