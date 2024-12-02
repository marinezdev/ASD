$('#MenuDashboard').addClass("active");


$(document).ready(function () {

    var table = $('#mydatatable').DataTable({
        "dom": 'B<"float-left"i><"float-right"f>t<"float-left"l><"float-right"p><"clearfix">',
        "responsive": false,
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
        "order": [[0, "desc"]],
        columnDefs: [{ width: 200, targets: 0 }],
        fixedColumns: true,
        paging: false,
        scrollCollapse: true,
        scrollX: true,
        scrollY: 400
    });

    var table = $('#TableTiemposAtencion').DataTable({
        "dom": 'B<"float-left"i><"float-right"f>t<"float-left"l><"float-right"p><"clearfix">',
        "responsive": false,
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
        "order": false,
        columnDefs: [{ width: 200, targets: 0 }],
        fixedColumns: true,
        searching: false,
        paging: false,
        scrollCollapse: true,
        scrollX: true,
        scrollY: 400
    });
});

function ConsultarTicket(Id) {
    const MyUrl = {
        UrlVariable: Id.toString(),
    };

    fetch("/Administracion/URL/URL_Cifrar", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(MyUrl)
    })
        .then(res => res.json())
        .then(res => {
            window.location.href = '/Ticket/Ticket/Detalle?cont=' + res.url;

        });
}

function ConsultarOperacion() {
    window.location.href = '/Ticket/Ticket/OperacionOrdenTrabajo';
}

function ConsultarTiempos(Id) {

    const _modelo = {
        Id: Id,
    };

    fetch("/Operacion/TicketEtapa/GetTicketEtapa_Consualta_IdTicket", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
       
        /*console.log(res);*/
        $("#ModalTiempoAtencion").modal("show");

        
        var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                            "<table class='table table-striped row-border order-column table-hover'>" +
                                "<thead class='table-dark'>" +
                                        "<tr class='table-dark'>" +
                                            "<th>Etapa</th>" +
                                            "<th>Fecha registro</th>" +
                                            "<th>Fecha inicio</th>" +
                                            "<th>Fecha termino</th>" +
                                            "<th>Atendio</th>" +
                                            "<th>Tiempo de atencion</th>" +
                                        "</tr>" +
                                "</thead>" +
                                "<tbody>";

                
        var TableFooter =       "</tbody>" +
                            "</table>" +
                        "</div>";

        var Tabla = "";

        if (res === null) {
            Tabla = "<tr>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                    "</tr>";
        } else if (res.length > 0) {
            $(res).each(function () {

                

                Tabla += "<tr>" +
                            "<td>" + this.etapa.nombre.toUpperCase() + "</td>" +
                    "<td>" + FormatoFecha(this.ticketEtapa.fechaRegistro) + "</td>" +
                    "<td>" + FormatoFecha(this.ticketEtapa.fechaInicio) + "</td>" +
                    "<td>" + FormatoFecha(this.ticketEtapa.fechaTermino) + "</td>" +
                    "<td>" + this.persona.nombre.toUpperCase() + " " + this.persona.apellidoPaterno.toUpperCase() + " " + this.persona.apellidoMaterno.toUpperCase() + "</td>" +
                    "<td>" + this.tiempoAtencion + "</td>" +
                        "</tr>";
            });
        }

        $("#DivModalTiempoAtencion").html(TableHeader + Tabla + TableFooter);
        
    });

}

function FormatoFecha(fecha) {

    var FechaFormato;

    var opciones = {
        /*    weekday: "long",*/
        year: "numeric",
        month: "numeric",
        day: "numeric",
        hour: "numeric",
        minute: "numeric",
        second: "numeric",
        hour12: true
    };

    if (fecha != "0001-01-01T00:00:00") {
        var fechaRegistroStr = fecha;
        var fechaRegistro = new Date(fechaRegistroStr);
        var FechaFormato = fechaRegistro.toLocaleDateString("es-MX", opciones);
    } else {
        FechaFormato = "";
    }
    return FechaFormato;
}

function CargaTotalFlujo(dbData) {
/*    console.log(dbData);*/
    
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

    $("#TotalOperacion").text(total);
    

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

function CargaTotalEtapas(dbData) {

    var data = [];
    dbData.forEach(function (item) {
        const _modelo = {
            name: item.etapa.nombre,
            y: item.total,
/*            color: item.flujo.color*/
        };
        data.push(_modelo);
    });

    Highcharts.chart('piechart2', {
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
            align: 'right',
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

function CargaTiemposAtencion(dbData) {
   /* console.log(dbData);*/

    var Total = 0;
    dbData.forEach(function (item) {
        Total += item.tiempoAtencionSegundos;
    });

    var data = [];
    dbData.forEach(function (item) {
        var tiempo = ((item.tiempoAtencionSegundos / Total).toFixed(2)) * 100;
        const _modelo = {
            name: item.etapa.nombre,
            y: tiempo,
        };
        data.push(_modelo);
    });

    Highcharts.chart('PorcentajeTiempoAtencion', {
        chart: {
            type: 'pie',
            options3d: {
                enabled: true,
                alpha: 45
            }
        },
        title: {
            text: 'Tiempos de atencion ',
            align: 'left'
        },
        subtitle: {
            text: 'Tickets atendidos, procesados etcetera ',
            align: 'left'
        },
        plotOptions: {
            pie: {
                innerSize: 100,
                depth: 45
            }
        },
        series: [{
            name: 'Porcentaje de atencion ',
            data: data
        }]
    });

}

function CargaEstatusTickets(dbData) {

    var Estatus = [];
    var Total = [];
    var backgroundColor = []
    dbData.forEach(function (item) {
        Estatus.push(item.ticket.cat_EstatusTicket.nombre);
        Total.push(item.total);
        backgroundColor.push(item.ticket.cat_EstatusTicket.color);
    });
    
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
            pointFormat: 'Cars sold: {point.y}'
        },
        title: {
            text: 'Tickets registrados por estatus',
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

function ConsultarTicketsEtapa(NombreEtapa) {

    const _modelo = {
        Nombre: NombreEtapa,
    };

    fetch("/Dhasbord/Home/GetDhasbord_Tiempos_Atencion_Tickets", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        $("#ModalTicketTiempoAtencion").modal("show");

        console.log(res);


        var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                            "<table class='table table-striped row-border order-column table-hover' id='TableListaTickets'>" +
                                "<thead class='table-dark'>" +
                                        "<tr class='table-dark'>" +
                                            "<th style='background-color: #ffffff;'>Folio</th>" +
                                            "<th>Tipo de servicio</th>" +
                                            "<th>Sucursal</th>" +
                                            "<th>Creacion</th>" +
                                            "<th>Fecha atencion</th>" +
                                            "<th>Tiempo de atencion</th>" +
                                            "<th>Cuadrilla</th>" +
                                            "<th>Usuario (asignado)</th>" +
                                            "<th>Status</th>" +
                                            "<th>Prioridad</th>" +
                                            "<th></th>" +
                                        "</tr>" +
                                "</thead>" +
                                "<tbody>";

                
        var TableFooter =       "</tbody>" +
                            "</table>" +
                        "</div>";

        var Tabla = "";

        if (res === null) {
            Tabla = "<tr>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                    "</tr>";
        } else if (res.length > 0) {
            $(res).each(function () {

                var cuadrilla = "";
                var Persona = "";
                if (this.cuadrilla != null) {
                    cuadrilla = this.cuadrilla.nombre;
                }

                if (this.cuadrillaUsuario != null)
                {
                    Persona =  this.cuadrillaUsuario.persona.nombre + " " +  this.cuadrillaUsuario.persona.apellidoPaterno + " " + this.cuadrillaUsuario.persona.apellidoMaterno
                }

                Tabla += "<tr>" +
                            "<td>" + this.folio.folio.toUpperCase() + "</td>" +
                            "<td>" + this.ticket.flujo.nombre+ "</td>" +
                            "<td>" + this.ticket.sucursal.nombre+ "</td>" +
                            "<td>" + this.ticket.fechaRegistro+"</td>" +
                            "<td>" + this.fechaAtencion.fechaAtencion+"</td>" +
                            "<td>" + this.tiempoAtencion + "</td>" +
                            "<td>" + cuadrilla + "</td>" +
                            "<td>" + Persona + "</td>" +
                            "<td><div class='label-main'><label class='label label-md' style='background-color: " + this.ticket.cat_EstatusTicket.color +"'>" + this.ticket.cat_EstatusTicket.nombre +"</label></div></td>" +
                            "<td><div class='label-main'><label class='label label-md' style='background-color: " + this.ticket.cat_Prioridad.color+"'>" + this.ticket.cat_Prioridad.nombre +"</label></div></td>" +
                            "<td></td>" +
                        "</tr>";
            });
        }

        $("#DivModalTicketTiempoAtencion").html(TableHeader + Tabla + TableFooter);

        var table = $('#TableListaTickets').DataTable({
            "dom": 'B<"float-left"i><"float-right"f>t<"float-left"l><"float-right"p><"clearfix">',
            "responsive": false,
            "language": {
                "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
            },
            "order": [[0, "desc"]],
            columnDefs: [{ width: 200, targets: 0 }],
            fixedColumns: true,
            paging: false,
            scrollCollapse: true,
            scrollX: true,
            scrollY: 400
        });

    });
}

function PintaMapa(dbData) {
      (async () => {

        const topology = await fetch(
            'https://code.highcharts.com/mapdata/countries/mx/mx-all.topo.json'
        ).then(response => response.json());

        // Prepare demo data. The data is joined to map using value of 'hc-key'
        // property by default. See API docs for 'joinBy' for more info on linking
        // data and map.
        var data = [];
        data.push(['mx-3622', 3]);

        dbData.forEach(function (item) {

            data.push([item.mapa, item.total, item.id ]);
        });


        // Create the chart
        Highcharts.mapChart('container', {
            chart: {
                map: topology
            },

            title: {
                text: 'Tickets atendidos por estado'
            },

            subtitle: {
                text: 'Sucursales registradas por estado en Mexico</a>'
            },

            mapNavigation: {
                enabled: true,
                buttonOptions: {
                    verticalAlign: 'bottom'
                }
            },

            colorAxis: {
                min: 0,
               /* minColor: '#C0C0C0', // Color inicial del gradiente*/
                maxColor: '#1396e5', // Color final del gradiente
            },

            series: [{
                data: data,
                name: 'Tickets registrados',
                colorByPoint: true,
                colors: ['#00897B', '#00C292', '#29ABE2', '#FF9900', '#FFCC00'],
                states: {
                    hover: {
                        color: '#544fc5'
                    },
                    select: {
                        color: '#FF0000' // Color al seleccionar una regi�n
                    }
                },
                dataLabels: {
                    enabled: true,
                    format: '{point.name}'
                },
                events: {
                    click: function (event) {
                        ContaEstado(event.point.index);
                        /* const id = event.point.customData.id;*/
                        //alert('Has hecho clic en ' + event.point.name + ' con ID ' + event.point.index);

                        // Tu c�digo aqu� se ejecutar� al hacer clic en una regi�n del mapa
                        // alert('Has hecho clic en ' + event.point.name);
                        // general consulta redireccionamiento
                    }
                }
            }]
        });
    })();
}

function ContaEstado(IdEstado) {
    const MyUrl = {
        UrlVariable: IdEstado.toString(),
    };

    fetch("/Administracion/URL/URL_Cifrar", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(MyUrl)
    })
        .then(res => res.json())
        .then(res => {
            window.location.href = '/Ticket/Ticket/TicketEstado?cont=' + res.url;
        });
}

function CargaEncuenta1(dbData) {

    if (dbData != null) {
        var data = [];
        dbData.forEach(function (item) {
            if (item.pregunta1 == 1) {
                const _modelo = {
                    name: "Si",
                    y: item.total,
                    /*            color: item.flujo.color*/
                };
                data.push(_modelo);
            } else {
                const _modelo = {
                    name: "No",
                    y: item.total,
                    /*            color: item.flujo.color*/
                };
                data.push(_modelo);
            }
        });

        Highcharts.chart('Pregunta1', {
            chart: {
                type: 'pie',
                options3d: {
                    enabled: true,
                    alpha: 45
                }
            },
            title: {
                text: 'La unidad de negocio quedo en operacion tras el mantenimiento ',
                align: 'left'
            },
            subtitle: {
                text: 'Calificacion del servicio (si y no) ',
                align: 'left'
            },
            plotOptions: {
                pie: {
                    innerSize: 100,
                    depth: 45
                }
            },
            series: [{
                name: 'Porcentaje de atencion ',
                data: data
            }]
        });
    }
    

}

function CargaEncuenta2(dbData) {
    if (dbData != null) {
        var data = [];
        dbData.forEach(function (item) {
            if (item.pregunta2 == 1) {
                const _modelo = {
                    name: "MALO",
                    y: item.total,
                    /*            color: item.flujo.color*/
                };
                data.push(_modelo);
            } else if (item.pregunta2 == 2) {
                const _modelo = {
                    name: "BUENO",
                    y: item.total,
                    /*            color: item.flujo.color*/
                };
                data.push(_modelo);
            } else if (item.pregunta2 == 3) {
                const _modelo = {
                    name: "EXCELENTE",
                    y: item.total,
                    /*            color: item.flujo.color*/
                };
                data.push(_modelo);
            }
        });

        Highcharts.chart('Pregunta2', {
            chart: {
                type: 'pie',
                options3d: {
                    enabled: true,
                    alpha: 45
                }
            },
            title: {
                text: 'Como evalua el servicio de mantenimiento proporcionado',
                align: 'left'
            },
            subtitle: {
                text: 'Calificacion del servicio (malo, bueno, excelente) ',
                align: 'left'
            },
            plotOptions: {
                pie: {
                    innerSize: 100,
                    depth: 45
                }
            },
            series: [{
                name: 'Porcentaje de atencion ',
                data: data
            }]
        });
    }
}

function CargaEncuenta3(dbData) {

    if (dbData != null) {
        var data = [];
        dbData.forEach(function (item) {
            if (item.pregunta3 == 1) {
                const _modelo = {
                    name: "MALO",
                    y: item.total,
                    /*            color: item.flujo.color*/
                };
                data.push(_modelo);
            } else if (item.pregunta3 == 2) {
                const _modelo = {
                    name: "BUENO",
                    y: item.total,
                    /*            color: item.flujo.color*/
                };
                data.push(_modelo);
            } else if (item.pregunta3 == 3) {
                const _modelo = {
                    name: "EXCELENTE",
                    y: item.total,
                    /*            color: item.flujo.color*/
                };
                data.push(_modelo);
            }
        });

        Highcharts.chart('Pregunta3', {
            chart: {
                type: 'pie',
                options3d: {
                    enabled: true,
                    alpha: 45
                }
            },
            title: {
                text: 'El aspecto y trato de los ingenieros de servicio fueron',
                align: 'left'
            },
            subtitle: {
                text: 'Calificacion del servicio (malo, bueno, excelente) ',
                align: 'left'
            },
            plotOptions: {
                pie: {
                    innerSize: 100,
                    depth: 45
                }
            },
            series: [{
                name: 'Porcentaje de atencion ',
                data: data
            }]
        });
    }
}


