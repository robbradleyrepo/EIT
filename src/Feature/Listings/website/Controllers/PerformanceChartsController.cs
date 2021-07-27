namespace LionTrust.Feature.Listings.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using LionTrust.Feature.Listings.Models.PerformanceCharts;
    using LionTrust.Foundation.Content.Repositories;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Sitecore.Mvc.Controllers;

    public class PerformanceChartsController : SitecoreController
    {
        private readonly IRenderingRepository _renderingRepository;

        public PerformanceChartsController(IRenderingRepository repository)
        {
            this._renderingRepository = repository;
        }

        public ActionResult Render()
        {
            var viewModel = new PerformanceChartsViewModel();
            viewModel.Data = _renderingRepository.GetDataSourceItem<IPerformanceCharts>();
            var chartModel = new ChartsModel();

            chartModel.YAxeConfig = new YAxeConfig(){
                Scale = viewModel.Data.Scale 
            };
            chartModel.Labels = new List<string>();            
            var columnValueCount = 0;
            var first = true;

            if (viewModel.Data != null && viewModel.Data.ChartColumns != null)
            {
                chartModel.Datasets = new List<ChartColumnModel>(viewModel.Data.ChartColumns.Count());
                foreach (var chart in viewModel.Data.ChartColumns)
                {
                    chartModel.Labels.Add(!string.IsNullOrEmpty(chart.Title) ? chart.Title : chart.Name);
                    if (chart.ChartValues != null && chart.ChartValues.Any())
                    {
                        foreach (var chartColumn in chart.ChartValues)
                        {
                            if (first)
                            {
                                chartModel.Datasets.Add(new ChartColumnModel
                                {
                                    Data = new List<int>() { chartColumn.Value },
                                    Color = chartColumn.Color.Value,
                                    Label = chartColumn.Title
                                });
                            }
                            else
                            {
                                chartModel.Datasets[columnValueCount].Data.Add(chartColumn.Value);
                                chartModel.Datasets[columnValueCount].Color = chartColumn.Color.Value;
                                chartModel.Datasets[columnValueCount].Label = chartColumn.Title;
                            }

                            columnValueCount++;
                        }
                    }

                    first = false;
                    columnValueCount = 0;
                }
            }

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None
            };

            viewModel.ChartJson = JsonConvert.SerializeObject(chartModel, jsonSerializerSettings);
            return View("~/Views/Listings/PerformanceCharts.cshtml", viewModel);
        }
    }
}