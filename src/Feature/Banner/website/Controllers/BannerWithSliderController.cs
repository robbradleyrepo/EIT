namespace LionTrust.Feature.Banner.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Banner.Models;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;
    using System.Linq;
    using LionTrust.Foundation.Onboarding.Helpers;

    public class BannerWithSliderController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public BannerWithSliderController(IMvcContext mvcContext)
        {
            _mvcContext = mvcContext;
        }

        public ActionResult Render()
        {
            var data = _mvcContext.GetDataSourceItem<IBannerWithSlider>();

            if(data == null)
            {
                return null;
            }

            data.Images = data.Images.Where(i => OnboardingHelper.HasAccess(i.Fund?.ExcludedCountries));

            return View("~/Views/Banner/BannerWithSlider.cshtml", data);
        }
    }
}