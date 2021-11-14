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
            formatter: (value, ctx) => {
              let sum = 0;
              let dataArr = ctx.chart.data.datasets[0].data;
              let percentage;
              dataArr.map(data => {
                sum += data;
              });
              if(sum === 0)
              {
                sum = 1;
                 percentage = 0 + "%";
              }
              else
              {
                percentage = (value * 100 / sum).toFixed(2) + "%";
              }
             
              return percentage;
            },
            color: 'transparent',
            align: 'end',
            anchor: 'end',
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
