namespace LionTrust.Feature.Banner.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Banner.Models;
    using LionTrust.Foundation.Contact.Services;
    using Sitecore.Analytics;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    public class MyFundsLeadBannerController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;
        private readonly IPersonalizedContentService _personalizedContentService;

        public MyFundsLeadBannerController(IMvcContext context, IPersonalizedContentService personalizedContentService)
        {
            _mvcContext = context;
            _personalizedContentService = personalizedContentService;
        }

        public ActionResult Render()
        {
            var dataSource = _mvcContext.GetDataSourceItem<IMyFundsLeadBanner>();

            if (!Sitecore.Context.PageMode.IsExperienceEditor && dataSource == null || Tracker.Current == null || !Tracker.IsActive || Tracker.Current.Contact == null)
            {
                return null;
            }

            var contactData = _personalizedContentService.GetContactFacetData();
            var viewModel = new MyFundsLeadBannerViewModel(dataSource);

            if (contactData != null)
            {
                viewModel.ContactName = $"{contactData.FirstName} {contactData.LastName}";
            }

            return View("~/Views/Banner/MyFundsLeadBanner.cshtml", viewModel);
        }
    }
}