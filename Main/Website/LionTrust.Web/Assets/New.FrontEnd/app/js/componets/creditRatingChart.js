import {
  Chart,
  ArcElement,
  BarElement,
  PointElement,
  BarController,
  CategoryScale,
  LinearScale,
  LogarithmicScale,
  RadialLinearScale,
  TimeScale,
  TimeSeriesScale,
  Decimation,
  Filler,
  Legend,
  Title,
  // Tooltip
} from "chart.js";

export default () => {
  const ctx = document.getElementById("creditRating").getContext("2d");
  const { labels, data, maxValue } = $("#dataCreditRating").data("chart");
  Chart.defaults.font.size = 20;
  Chart.defaults.font.family = "futura-pt";
  Chart.defaults.font.weight = "300";
  Chart.register(
    ArcElement,
    BarElement,
    PointElement,
    BarController,
    CategoryScale,
    LinearScale,
    LogarithmicScale,
    RadialLinearScale,
    TimeScale,
    TimeSeriesScale,
    Decimation,
    Filler,
    Legend,
    Title
    // Tooltip
  );

  const myChart = new Chart(ctx, {
    type: "bar",
    data: {
      labels,
      datasets: [
        {
          label: "Fund",
          maxBarThickness: 16,
          data,
          backgroundColor: "#10a047",
        },
      ],
    },
    options: {
      layout: {
        left: 0,
        right: 0,
        top: 15,
        bottom: 0,
      },
      maintainAspectRatio: true,
      responsive: true,
      indexAxis: "y",
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            beginAtZero: true,
            crossAlign: 'far'
          },
          grid: {
            borderDash: [3, 3],
            drawTicks: false,
          },
        },
        x: {
          alignToPixels: true,
          max: maxValue,
          // display: false,
          grid: {
            display: false,
          },
          ticks: {
            callback: function (value, index, values) {
              return value + "%";
            },
          },
        },
      },
      plugins: {
        legend: {
          position: "bottom",
          align: "start",
          labels: {
            boxWidth: 15,
          },
        },
      },
    },
  });
};
