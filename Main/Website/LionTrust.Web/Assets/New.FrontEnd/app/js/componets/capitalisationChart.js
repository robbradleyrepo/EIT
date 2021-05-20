import Chart from "chart.js";

export default () => {
  if ($("#capitalisationChart")) {
    const ctx = document.getElementById("capitalisationChart").getContext("2d");
    const { labels, data, backgroundColor } = $("#capitalisationChart").data("chart");
    Chart.defaults.global.defaultFontSize = 20;
    Chart.defaults.global.defaultFontFamily = "futura-pt";
    Chart.defaults.global.defaultFontStyle = "300";

    // Chart.defaults.RoundedDoughnut = Chart.helpers.clone(
    //   Chart.defaults.doughnut
    // );
    // Chart.controllers.RoundedDoughnut = Chart.controllers.doughnut.extend({
    //   draw: function (ease) {
    //     var ctx = this.chart.ctx;
    //     var easingDecimal = ease || 1;
    //     var arcs = this.getMeta().data;
    //     Chart.helpers.each(arcs, function (arc, i) {
    //       arc.transition(easingDecimal).draw();

    //       var pArc = arcs[i === 0 ? arcs.length - 1 : i - 1];
    //       var pColor = pArc._view.backgroundColor;

    //       var vm = arc._view;
    //       var radius = (vm.outerRadius + vm.innerRadius) / 2;
    //       var thickness = (vm.outerRadius - vm.innerRadius) / 2;
    //       var startAngle = Math.PI - vm.startAngle - Math.PI / 2;
    //       var angle = Math.PI - vm.endAngle - Math.PI / 2;

    //       ctx.save();
    //       ctx.translate(vm.x, vm.y);

    //       ctx.fillStyle = i === 0 ? vm.backgroundColor : pColor;
    //       ctx.beginPath();
    //       ctx.arc(
    //         radius * Math.sin(startAngle),
    //         radius * Math.cos(startAngle),
    //         thickness,
    //         0,
    //         2 * Math.PI
    //       );
    //       ctx.fill();

    //       ctx.fillStyle = vm.backgroundColor;
    //       ctx.beginPath();
    //       ctx.arc(
    //         radius * Math.sin(angle),
    //         radius * Math.cos(angle),
    //         thickness,
    //         0,
    //         2 * Math.PI
    //       );
    //       ctx.fill();

    //       ctx.restore();
    //     });
    //   },
    // });

    var myChart = new Chart(ctx, {
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
        legendCallback: function (chart) {
            var text = [];
            text.push('<ul class="circle-legend">');
            for (var i = 0; i < chart.data.labels.length; i++) {
              text.push(
                '<li><span class="dot" style="background-color:' +
                  chart.data.datasets[0].backgroundColor[i] +
                  '"></span>'
              );
                text.push(
                  '<span class="label">' +
                    chart.data.labels[i] + '<span class="persents"> '+ chart.data.datasets[0].data[i] +'% </span></span></li>'
                );
            }

            text.push("</ul>");

            return text.join("");
          }
      },
    });
    $("#capitalisationLegend").prepend(myChart.generateLegend());
  }  
};
