namespace LionTrust.Feature.Listings.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using LionTrust.Feature.Listings.Models;
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

            if (viewModel.Data == null
                || viewModel.Data.ChartColumnFolder == null
                || !viewModel.Data.ChartColumnFolder.Any()
                || viewModel.Data.ChartRangeValueFolder == null
                || !viewModel.Data.ChartRangeValueFolder.Any())
            {
                return null;
            }

            viewModel.ChartColumnFolder = viewModel.Data.ChartColumnFolder.FirstOrDefault();
            viewModel.ChartRangeValueFolder = viewModel.Data.ChartRangeValueFolder.FirstOrDefault();
            if (viewModel.ChartColumnFolder == null
                || viewModel.ChartColumnFolder.ChartColumns == null
                || !viewModel.ChartColumnFolder.ChartColumns.Any())
            {
                return null;
            }

            if (viewModel.ChartRangeValueFolder != null
                && viewModel.ChartRangeValueFolder.ChartRanges != null
                && viewModel.ChartRangeValueFolder.ChartRanges.Any())
            {
                chartModel.Ranges = viewModel.ChartRangeValueFolder.ChartRanges.Select(x => !string.IsNullOrEmpty(x.Value) ? x.Value : x.Name);
            }
                       
            chartModel.Labels = new List<string>();
            chartModel.Datasets = new List<ChartColumnModel>(viewModel.ChartColumnFolder.ChartColumns.Count());
            var columnValueCount = 0;
            var first = true;

            foreach (var chart in viewModel.ChartColumnFolder.ChartColumns)
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