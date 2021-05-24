namespace LionTrust.Feature.Fund.Controllers
{    
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    public class FundHeaderController: SitecoreController
    {
        private readonly IMvcContext _context;

        public FundHeaderController(IMvcContext context)
        {
            this._context = context;
        }

        public ActionResult Render()
        {
            var data = _context.GetDataSourceItem<IFundHeader>();
            
            if (data == null)
            {
                return null;
            }

            var result = new FundHeaderViewModel(data);           
            return View("/views/fund/fundheader.cshtml", result);
        }
    }
}