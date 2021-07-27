import Chart from "chart.js";

export default () => {
  const charts = document.getElementsByClassName("stucked-chart__item");
  if (charts) {
    charts.forEach((chart) => {
      const ctx = chart.getContext("2d");
      const data = $(chart).data("chart");
      const { labels, datasets, config } = data;
      // add colors array to dataset
      const genericDataset = datasets.map((set, i) => {
        const backgroundColor = [];
        for (let item in set.data) {
          backgroundColor.push(set.color);
        }
        return { ...set, backgroundColor };
      });

      Chart.defaults.global.defaultFontSize = 20;
      Chart.defaults.global.defaultFontFamily = "futura-pt";
      //   Chart.defaults.global.defaultFontStyle = "300";

      const myChart = new Chart(ctx, {
        type: "bar",
        data: {
          labels,
          datasets: genericDataset,
        },
        options: {
          layout: {
            left: 0,
            right: 0,
            top: 15,
            bottom: 0,
          },
          maintainAspectRatio: false,
          aspectRatio:0,
          scales: {
            xAxes: [
              {
                maxBarThickness: 60,
                stacked: true,
                gridLines: {
                  display: false,
                },
              },
            ],
            yAxes: [
              {
                stacked: true,
                gridLines: {
                  color: "#dee2e6",
                  borderDash: [3, 3],
                },
                ticks: {
                  callback: function (value, index, values) {
                    if (config?.scale === "%") return value + config.scale;
                    if (config?.scale === "£") return config.scale + value;
                    return value;
                  },
                  beginAtZero: true,
                  //   stepSize: 10,
                  // max: 100,
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
              borderRadius: "50%",
            },
          },
          tooltips: {
            enabled: false,
          },
          legend: {
            display: false,
          },
          legendCallback: function ({ data }) {
            const text = [];
            text.push('<ul class="stucked-legend">');
            for (let i = 0; i < data.datasets.length; i++) {
              if (data.datasets[i].label) {
                text.push(
                  '<li><span class="dot" style="background-color:' +
                    data.datasets[i].backgroundColor[0] +
                    '"></span>'
                );
                text.push(
                  '<span class="label">' +
                    data.datasets[i].label +
                    "</span></li>"
                );
              }
            }
            text.push("</ul>");
            return text.join("");
          },
        },
      });
      // add legend to chart bottom
      $(chart).after(myChart.generateLegend());
    });
  }
};
