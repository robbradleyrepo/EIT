namespace LionTrust.Feature.Fund.CreditRating
{
    using Glass.Mapper.Sc.Web.Mvc;
    using Newtonsoft.Json;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;

    public class CreditRatingController : SitecoreController
    {
        private readonly IMvcContext _context;

        public CreditRatingController(IMvcContext context)
        {
            this._context = context;
        }

        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<ICreditRating>();
            if (datasource == null)
            {
                return null;
            }

            if (datasource.Children != null)
            {
                var data = new { labels = datasource.Children.Select(c => c.RowName), data = datasource.Children.Select(c => (double.TryParse(c.Value, out double val) ? val : 0)), maxValue = datasource.MaxValue };
                datasource.JsonDataObject = JsonConvert.SerializeObject(data);
            }

            return View("/views/fund/creditrating.cshtml", datasource);
        }
    }
}