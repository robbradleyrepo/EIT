import Chart from 'chart.js/dist/Chart.js'

export default () => {
  if ($(".capitalisation-chart").length) {
    const ctx = $(".capitalisation-chart")[0].getContext("2d");
    const { labels, data, backgroundColor } = $(".capitalisation-chart").data("chart");
    Chart.defaults.global.defaultFontSize = 20;
    Chart.defaults.global.defaultFontFamily = "futura-pt";
    Chart.defaults.global.defaultFontStyle = "300";
    const myChart = new Chart(ctx, {
      type: "doughnut",
      data: {
        labels,
        datasets: [
          {
            label: "Capitalisation",
            data,
            backgroundColor,
            borderWidth: 1,
          },
        ],
      },
      options: {
        cutoutPercentage: 90,
        circumference: 1.6 * Math.PI,
        rotation: 2.7 * Math.PI,
        responsive: true,
        tooltips: {
          enabled: false
        },
        legend: {
          display: false,
        },
        plugins: {
          datalabels: {
            color: 'transparent',
            align: 'center',
            anchor: 'center',
            labels: {
              title: {
                font: {
                  weight: '300'
                }
              },
            }
          }
        },
        legendCallback: function (chart) {
          const text = [];
          text.push('<ul class="circle-legend">');
          for (let i = 0; i < chart.data.labels.length; i++) {
            text.push(
              '<li><span class="dot" style="background-color:' +
              chart.data.datasets[0].backgroundColor[i] +
              '"></span>'
            );
            text.push(
              '<span class="label">' +
              chart.data.labels[i] + '<span class="persents"> ' + chart.data.datasets[0].data[i] + '% </span></span></li>'
            );
          }

          text.push("</ul>");

          return text.join("");
        }
      },
    });
    $(".capitalisation-legend").prepend(myChart.generateLegend());
  }
};
