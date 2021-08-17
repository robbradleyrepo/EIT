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

        public ActionResult Render(string @ref)
        {
            var dataSource = _mvcContext.GetDataSourceItem<IMyFundsLeadBanner>();

            if (!Sitecore.Context.PageMode.IsExperienceEditor && dataSource == null || Tracker.Current == null || !Tracker.IsActive || Tracker.Current.Contact == null)
            {
                return null;
            }

            var contactData = _personalizedContentService.GetContactFacetData(@ref);
            var viewModel = new MyFundsLeadBannerViewModel(dataSource);

            if (!Sitecore.Context.PageMode.IsExperienceEditor && contactData == null)
            {
                return null;
            }
            else if (contactData != null)
            {
                viewModel.ContactName = $"{contactData.FirstName} {contactData.LastName}";

                if (viewModel.Content.Cta != null)
                {
                    viewModel.Content.Cta.Query = $"{Foundation.Contact.Constants.QueryStringNames.EmailPreferencefParams.RefQueryStringKey}={@ref}";
                }
            }

            return View("~/Views/Banner/MyFundsLeadBanner.cshtml", viewModel);
        }
    }
}