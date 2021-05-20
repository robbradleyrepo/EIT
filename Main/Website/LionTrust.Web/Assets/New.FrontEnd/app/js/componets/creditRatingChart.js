import Chart from "chart.js";

export default () => {
  if (document.getElementById("creditRating")) {
    const ctx = document.getElementById("creditRating").getContext("2d");
    const { labels, data, maxValue } = $("#dataCreditRating").data("chart");
    Chart.defaults.global.defaultFontSize = 20;
    Chart.defaults.global.defaultFontFamily = "futura-pt";
    Chart.defaults.global.defaultFontStyle = "300";

    const myChart = new Chart(ctx, {
      type: "horizontalBar",
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
        // indexAxis: "y",
        scales: {
          xAxes: [
            {
              stacked: true,
              gridLines: {
                display: false,
              },
              ticks: {
                callback: function (value, index, values) {
                  return value + "%";
                },
                beginAtZero: true,
                stepSize: 10,
                max: maxValue,
              },
            },
          ],
          yAxes: [
            {
              gridLines: {
                color: "#dee2e6",
                borderDash: [3, 3],
              },
            },
          ],          
        },
        legend: {
            position: "bottom",
            align: "start",
            labels: {
              boxWidth: 18,
              boxHeight: 15,
            },
          },
          tooltips: {
              enabled: false
          }
      },
    });
  }
};
