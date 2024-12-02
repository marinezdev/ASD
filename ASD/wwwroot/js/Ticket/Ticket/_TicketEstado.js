$('#MenuOperacion').addClass("active");


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
        //scrollX: true,
        //scrollY: 400,
        layout: {
            topStart: {
                buttons: [
                    { // Copy button customization
                        extend: 'copy',
                        text: 'Copy to Clipboard', // Change button text
                        className: 'custom-copy-btn', // Add custom class
                        exportOptions: {
                            // Optional: Configure specific export options for copy
                            modifier: {
                                page: 'current' // Only copy the current page
                            }
                        }
                    },
                    { // CSV button customization
                        extend: 'csv',
                        text: 'Download CSV', // Change button text
                        className: 'custom-csv-btn' // Add custom class
                    },
                    { // Excel button customization
                        extend: 'excel',
                        text: 'Export to Excel', // Change button text
                        className: 'custom-excel-btn', // Add custom class
                        exportOptions: {
                            // Optional: Configure specific export options for excel
                            modifier: {
                                selected: true // Only export selected rows
                            }
                        }
                    },
                    { // PDF button customization
                        extend: 'pdf',
                        text: 'Generate PDF', // Change button text
                        className: 'custom-pdf-btn', // Add custom class
                        exportOptions: {
                            // Optional: Configure specific export options for pdf
                            modifier: {
                                pageSize: 'A4' // Set the page size
                            }
                        }
                    },
                    { // Print button customization
                        extend: 'print',
                        text: 'Print Table', // Change button text
                        className: 'custom-print-btn' // Add custom class
                    }
                ]
            }
        }
    });

});

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


            var TableFooter = "</tbody>" +
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