namespace LionTrust.Feature.Banner.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Banner.Models;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;
    using LionTrust.Feature.Banner.ViewModels;
    using LionTrust.Foundation.ORM.Extensions;

    public class BannerWithImageController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public BannerWithImageController(IMvcContext mvcContext)
        {
            _mvcContext = mvcContext;
        }

        public ActionResult Render()
        {
            var data = _mvcContext.GetDataSourceItem<IBannerWithImage>();

            var model = new BannerWithImageViewModel
            {
                ComponentData = data,
                BackgroundImageStyle = !string.IsNullOrEmpty(data?.Image?.Src) ? data.Image.GetSafeBackgroundImageStyle() : null
            };

            return View("~/Views/Banner/BannerWithImage.cshtml", model);
        }
    }
}