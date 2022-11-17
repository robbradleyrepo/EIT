namespace LionTrust.Feature.Fund.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.FundClass;
    using LionTrust.Feature.Fund.Models;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Mvc.Controllers;

    public class SnapshotController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly IFundClassDetails _fundRepository;

        public SnapshotController(IMvcContext context, IFundClassDetails fundRepository)
        {
            _context = context;
            _fundRepository = fundRepository;
        }

        public ActionResult Render()
        {
            var viewModel = new SnapshotViewModel
            {
                Component = _context.GetDataSourceItem<ISnapshot>()
            };

            var pageData = _context.GetPageContextItem<IPresentationBase>();
            var fund = pageData?.Fund;

            if (fund != null)
            {
                var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, fund);
                var fundClass = fund.Classes.Where(c => c.CitiCode == citiCode).FirstOrDefault();

                if (fundClass != null)
                {
                    viewModel.FundValues = _fundRepository.GetKeyInfoDataOnDemand(fundClass, "0");
                }
            }

            return View("~/views/fund/snapshot.cshtml", viewModel);
        }
    }
}