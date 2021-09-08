namespace LionTrust.Feature.Fund.FundStats
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.FundClass;
    using LionTrust.Feature.Fund.Models;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;

    public class FundStatsController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly IFundStatsDetails _fundRepository;
        public FundStatsController(IMvcContext context, IFundStatsDetails fundRepository)
        {
            _context = context;
            _fundRepository = fundRepository;
        }

        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<IFourFundStats>();
            if (datasource == null)
            {
                return null;
            }

            if (datasource.Fund == null)
            {
                return View("/views/fund/FourFundStats.cshtml", new FundStatsViewModel { FundSelector = datasource });
            }

            var pageData = _context.GetPageContextItem<IFundSelector>();
            var fund = datasource.Fund != null ? datasource.Fund : pageData?.Fund;
            var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, fund);
            var viewModel = new FundStatsViewModel()
            {
                FundSelector = datasource
            };
            
            if (fund != null)
            {
                var fundClass = fund.Classes.Where(c => c.CitiCode == citiCode).FirstOrDefault();
                if (fundClass != null)
                {
                    var fundValues = _fundRepository.GetFundStatsDetails(fundClass);
                    if (fundValues != null)
                        viewModel.FundValues = fundValues;
                }
            }

            return View("/views/fund/FourFundStats.cshtml", viewModel);
        }
    }
}