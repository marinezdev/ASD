$('#MenuDashboard').addClass("active");

function CargaTotalFlujo(dbData) {
        //console.log(dbData);

    var data = [];
    var total = 0;
    dbData.forEach(function (item) {
        total += item.total;
        const _modelo = {
            name: item.flujo.nombre,
            y: item.total,
            color: item.flujo.color
        };
        data.push(_modelo);
    });

    $("#TotalTickets").text(total);


    Highcharts.chart('piechart', {
        chart: {
            type: 'pie',
            options3d: {
                enabled: true,
                alpha: 45,
                beta: 0
            }
            //  backgroundColor:'#fff'
        },
        title: false,
        //title: {
        //    text: 'Porcentaje de tickets registrados por tipo de servicio '
        //},
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        legend: {
            align: 'center',
            margin: 0,
            floating: true,
            layout: 'vertical',
            verticalAlign: 'top',
            labelFormatter: function () {
                let name = this.name.length > 20 ? this.name.substr(0, 20) + '...' : this.name;
                /* console.log(this)*/
                return `${name}   ${this.y}`
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                depth: 35,
                dataLabels: {
                    enabled: true,
                    format: '{point.name}'
                },
                showInLegend: true
            }
        },
        series: [{
            type: 'pie',
            name: 'Ticket',
            data: data
        }]
    });
}

function CargaEstatus(dbData) {
    //console.log(dbData);

    var Estatus = [];
    var Total = [];
    var backgroundColor = [];
    var TotalOperacion = 0;
    dbData.forEach(function (item) {
        TotalOperacion += item.total;
        Estatus.push(item.cat_EstatusTicket.nombre);
        Total.push(item.total);
        backgroundColor.push(item.cat_EstatusTicket.color);
    });

    $("#TotalOperacion").text(TotalOperacion);

    // Set up the chart
    const chart = new Highcharts.Chart({
        chart: {
            renderTo: 'GraficaEstatusticket',
            type: 'column',
            options3d: {
                enabled: true,
                alpha: 3,
                beta: -20,
                depth: 300,
                viewDistance: 50
            }
        },
        xAxis: {
            categories: Estatus
        },
        yAxis: {
            title: {
                enabled: false
            }
        },
        tooltip: {
            headerFormat: '<b>{point.key}</b><br>',
            pointFormat: 'Tickets : {point.y}'
        },
        title: false,
        //title: {
        //    text: 'Tickets en operacion por estatus',
        //    align: 'left'
        //},
        subtitle: {
            text: 'Grafica de barras, tickets en operacion por estatus',
            align: 'left'
        },
        //subtitle: {
        //    text: 'Source: ' +
        //        '<a href="https://ofv.no/registreringsstatistikk"' +
        //        'target="_blank">OFV</a>',
        //    align: 'left'
        //},
        legend: {
            enabled: false
        },
        plotOptions: {
            column: {
                depth: 25
            }
        },
        series: [{
            data: Total,
            colorByPoint: true,
            colors: backgroundColor,
        }]
    });

    function showValues() {
        document.getElementById('alpha-value').innerHTML = chart.options.chart.options3d.alpha;
        document.getElementById('beta-value').innerHTML = chart.options.chart.options3d.beta;
        document.getElementById('depth-value').innerHTML = chart.options.chart.options3d.depth;
    }

    // Activate the sliders
    document.querySelectorAll('#sliders input').forEach(input => input.addEventListener('input', e => {
        chart.options.chart.options3d[e.target.id] = parseFloat(e.target.value);
        showValues();
        chart.redraw(false);
    }));
}

function CargaPrioridad(dbData) {
    console.log(dbData);

    var data = [];
    var total = 0;
    dbData.forEach(function (item) {
        total += item.total;
        const _modelo = {
            name: item.cat_Prioridad.nombre,
            y: item.total,
            color: item.cat_Prioridad.color
        };
        data.push(_modelo);
    });

    $("#TotalTickets").text(total);


    Highcharts.chart('piechartPriporidad', {
        chart: {
            type: 'pie',
            options3d: {
                enabled: true,
                alpha: 45,
                beta: 0
            }
            //  backgroundColor:'#fff'
        },
        title: false,
        //title: {
        //    text: 'Tickets en operacion ',
        //},
        subtitle: {
            text: 'Porcentaje de tickets en operacion por porcentajes',
            align: 'left'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        legend: {
            align: 'center',
            margin: 0,
            floating: true,
            layout: 'vertical',
            verticalAlign: 'top',
            labelFormatter: function () {
                let name = this.name.length > 20 ? this.name.substr(0, 20) + '...' : this.name;
                /* console.log(this)*/
                return `${name}   ${this.y}`
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                depth: 35,
                dataLabels: {
                    enabled: true,
                    format: '{point.name}'
                },
                showInLegend: true
            }
        },
        series: [{
            type: 'pie',
            name: 'Ticket',
            data: data
        }]
    });

}