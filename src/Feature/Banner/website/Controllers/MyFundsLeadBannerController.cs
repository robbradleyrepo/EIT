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
        private readonly IContactService _contactService;

        public MyFundsLeadBannerController(IMvcContext context, IContactService contactService)
        {
            _mvcContext = context;
            _contactService = contactService;
        }

        public ActionResult Render()
        {
            var dataSource = _mvcContext.GetDataSourceItem<IMyFundsLeadBanner>();

            if (!Sitecore.Context.PageMode.IsExperienceEditor && dataSource == null || Tracker.Current == null || !Tracker.IsActive || Tracker.Current.Contact == null)
            {
                return null;
            }

            var contactData = _contactService.GetCurrentSitecoreContactFacetData(Tracker.Current.Contact.ContactId.ToString());
            var viewModel = new MyFundsLeadBannerViewModel(dataSource);

            if (!Sitecore.Context.PageMode.IsExperienceEditor && contactData == null)
            {
                return null;
            }
            else if (contactData != null)
            {
                viewModel.ContactName = $"{contactData.FirstName} {contactData.LastName}";
            }

            return View("~/Views/Banner/MyFundsLeadBanner.cshtml", viewModel);
        }
    }
}