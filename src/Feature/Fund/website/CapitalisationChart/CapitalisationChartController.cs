namespace LionTrust.Feature.Fund.CapitalisationChart
{
    using Glass.Mapper.Sc.Web.Mvc;
    using Newtonsoft.Json;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;
    public class CapitalisationChartController : SitecoreController
    {
        private readonly IMvcContext _context;

        public CapitalisationChartController(IMvcContext context)
        {
            this._context = context;
        }
        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<ICapitalisationChart>();

            if (datasource == null)
            {
                return null;
            }

            if (datasource.Children != null)
            {
                var data = new { labels = datasource.Children.Select(c => c.RowName), data = datasource.Children.Select(c => c.Value), backgroundColor = datasource.Children.Select(c => c?.BackgroundColour?.Value) };
                datasource.JsonDataObject = JsonConvert.SerializeObject(data);
            }

            return View("/views/fund/CapitalisationChart.cshtml", datasource);
           
        }
    }
}