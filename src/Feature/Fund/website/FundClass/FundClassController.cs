namespace LionTrust.Feature.Fund.FundClass
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    public class FundClassController: SitecoreController
    {
        private readonly IMvcContext _context;

        public FundClassController(IMvcContext context)
        {
            _context = context;
        }

        public ActionResult Render()
        {            
            var datasource = _context.GetDataSourceItem<IFundClassSelector>();
            if (datasource == null)
            {
                return null;
            }

            if (datasource.Fund == null)
            {
                return View("/views/fund/classselector.cshtml", new FundClassViewModel { FundSelector = datasource });
            }

            var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, datasource.Fund);

            return View("/views/fund/classselector.cshtml", new FundClassViewModel
            {
                DefaultCitiCode = string.IsNullOrEmpty(citiCode) ? datasource.Fund.CitiCode : citiCode,
                FundSelector = datasource

            });
        }
    }
}