﻿namespace LionTrust.Feature.Listings.Controllers
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
            
            var performanceCharts = _renderingRepository.GetDataSourceItem<IPerformanceCharts>();
            if (performanceCharts.PerformanceCharts != null && performanceCharts.PerformanceCharts.Any())
            {
                var count = performanceCharts.PerformanceCharts.Count();
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = PreserveReferencesHandling.None
                };

                viewModel.FirstChart = new PerformanceChartViewModel();
                viewModel.FirstChart.Data = performanceCharts.PerformanceCharts.Take(1).FirstOrDefault();
                this.PopulateChart(viewModel.FirstChart, jsonSerializerSettings);
                if (count == 1)
                {
                    return View("~/Views/Listings/PerformanceCharts.cshtml", viewModel);
                }

                viewModel.SecondChart = new PerformanceChartViewModel();
                viewModel.SecondChart.Data = performanceCharts.PerformanceCharts.Skip(1).Take(1).FirstOrDefault();
                this.PopulateChart(viewModel.SecondChart, jsonSerializerSettings);
                if (count == 2)
                {
                    return View("~/Views/Listings/PerformanceCharts.cshtml", viewModel);
                }

                viewModel.ThirdChart = new PerformanceChartViewModel();
                viewModel.ThirdChart.Data = performanceCharts.PerformanceCharts.Skip(2).Take(1).FirstOrDefault();
                this.PopulateChart(viewModel.ThirdChart, jsonSerializerSettings);
                if (count == 3)
                {
                    return View("~/Views/Listings/PerformanceCharts.cshtml", viewModel);
                }

                viewModel.FourthChart = new PerformanceChartViewModel();
                viewModel.FourthChart.Data = performanceCharts.PerformanceCharts.Skip(3).Take(1).FirstOrDefault();
                this.PopulateChart(viewModel.FourthChart, jsonSerializerSettings);                
            }

            return View("~/Views/Listings/PerformanceCharts.cshtml", viewModel);
        }

        private void PopulateChart(PerformanceChartViewModel viewModel, JsonSerializerSettings serializerSettings)
        {
            var chartModel = new ChartsModel();

            chartModel.YAxeConfig = new YAxeConfig()
            {
                Scale = viewModel.Data.Scale,
                MaxRange = viewModel.Data.MaxRange,
                Ranges = viewModel.Data.Range
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
                            if (chartColumn != null)
                            {
                                if (first)
                                {
                                    chartModel.Datasets.Add(new ChartColumnModel
                                    {
                                        Data = new List<int>() { chartColumn.Value },
                                        Color = chartColumn.Color?.Value,
                                        Label = chartColumn.Title
                                    });
                                }
                                else
                                {
                                    chartModel.Datasets[columnValueCount].Data.Add(chartColumn.Value);
                                    chartModel.Datasets[columnValueCount].Color = chartColumn.Color?.Value;
                                    chartModel.Datasets[columnValueCount].Label = chartColumn.Title;
                                }
                            }

                            columnValueCount++;
                        }
                    }

                    first = false;
                    columnValueCount = 0;
                }
            }            

            viewModel.ChartJson = JsonConvert.SerializeObject(chartModel, serializerSettings);
        }
    }
}