$('#MenuTicket2').addClass("active");

$(document).ready(function () {

    var table = $('#mydatatable').DataTable({
        "dom": 'B<"float-left"i><"float-right"f>t<"float-left"l><"float-right"p><"clearfix">',
        "responsive": false,
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
        /*"order": [[0, "desc"]],*/
        "order": [],
        columnDefs: [{ width: 200, targets: 0 }],
        fixedColumns: true,
        paging: false,
        scrollCollapse: true,
        scrollX: true,
        scrollY: 400
    });
});

function CargaImagenes(_model) {
    fetch("/Ticket/SucursalImg/GetSucursalImg_Seleccionar_IdTicket", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_model)
    })
    .then(res => res.json())
    .then(res => {
        if (res != null) {
            const imageData = res.filter(item => item.cat_SucursalImagen);
            const imageMap = {
                "Tienda/Sucursal": $("#image1"),
                "Mostrador": $("#image2"),
                "Orden de trabajo": $("#imagenOrdenTrabajo")
            };

            for (const item of imageData) {
                const imageContainer = imageMap[item.cat_SucursalImagen.nombre];
                if (imageContainer) {
                    imageContainer.html(`
                            <div class="card">
                                <div class="card-block text-center">
                                    <div class="row">
                                        <div class="col-sm-12 text-center">
                                            <a href="${item.nmArchivo}" data-lightbox="roadtrip" style="text-align:center">
                                                <img src="${item.nmArchivo}" class="img-fluid" alt="" width="40%" />
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        `);
                }
            }
        }
    });
}

function CargaVideo(_model) {
    fetch("/Ticket/SucursalVideo/GetSucursalVideo_Seleccionar_IdTikcet", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_model)
    })
    .then(res => res.json())
    .then(res => {
        if (res != null) {
            const videoData = res.filter(item => item.cat_SucursalVideo);
            const videoMap = {
                "2": $("#VideoRecorridoDespues"),
                "1": $("#VideoRecorridoAntes")
            };

            for (const item of videoData) {
                const videoContainer = videoMap[item.cat_SucursalVideo.id];
                if (videoContainer) {
                    videoContainer.html(`
                        <div class="card">
                            <div class="card-block text-center">
                                <div class="row">
                                    <div class="col-sm-12 text-center">
                                        <video width="100%" controls >
                                            <source src="${item.nmArchivo}" type="video/mp4">
                                        </video>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `);
                }
            }
        }
    });
}

function CargaRutina(_model) {
    fetch("/Ticket/TicketEquipoImagen/GetTicketEquipoImagen_Seleccionar_IdTicketEquipo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_model)
    })
    .then(res => res.json())
    .then(res => {
        if (res != null) {
            const html = res.map(equipo => {
                const rutinas = equipo.ticketEquipoRutina?.map(rutina => {
                    const isChecked = rutina.estatus === 1;
                    return `
                        <div class="row">
                            <div class="checkbox checkbox-primary">
                                <div class="col-md-12">
                                    <input type="checkbox" id="chkremeberme${rutina.Id}" ${isChecked ? 'checked' : ''} disabled>
                                    <label for="chkremeberme${rutina.Id}" class="m-b-10">${rutina.cat_EquipoRutina.nombre}</label>
                                </div>
                            </div>
                        </div>
                    `;
                }).join('');

                return `
                    <div class="accordion-panel">
                        <div class="accordion-heading" role="tab" id="heading${equipo.id}">
                            <h3 class="card-title accordion-title">
                                <a class="accordion-msg" data-toggle="collapse" data-parent="#accordion" href="#collapse${equipo.id}" aria-expanded="false" aria-controls="collapse${equipo.id}">
                                    ${equipo.equipo.serie}
                                </a>
                            </h3>
                        </div>
                        <div id="collapse${equipo.id}" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading${equipo.id}">
                            <div class="accordion-content accordion-desc">
                                ${rutinas}
                            </div>
                        </div>
                    </div>
                `;
            }).join('');

            $("#CheckEquipoRutina").html(html);
        }

        if (res != null) {
            var html = "";
            var labelEquipoImg = "";
            
            for (let i = 0; i < res.length; i++) {
             /*   console.log(res);*/

                if (res[i].ticketEquipoImagen != null) {

                    for (let a = 0; a < res[i].ticketEquipoImagen.length; a++) {
                        var imagen = "";
                        var imagenvs = "";
                        if (res[i].ticketEquipoImagen[a] != null) {

                            if (res[i].ticketEquipoImagen[a].imagenVS != null) {
                                imagenvs += `
                                <div id="image${res[i].ticketEquipoImagen[a].id}">
                                    <a href='${res[i].ticketEquipoImagen[a].imagenVS}' data-lightbox='roadtrip'>
                                        <img src='${res[i].ticketEquipoImagen[a].imagenVS}' class='img-fluid' alt='' />
                                    </a>
                                </div>
                                `;
                            }

                            if (res[i].ticketEquipoImagen[a].imagen != null) {
                                imagen += `
                                <div class='card'>
                                    <div class='card-block'>
                                        <div class='row'>
                                            <div class='col-md-6 col-xs-6 text-center'>
                                                <p>Foto antes</p>
                                                <a href='${res[i].ticketEquipoImagen[a].imagen}' data-lightbox='roadtrip'>
                                                    <img src='${res[i].ticketEquipoImagen[a].imagen}' class='img-fluid' alt='' />
                                                </a>
                                            </div>
                                            <div class='col-md-6 col-xs-6 text-center'>
                                                <p>Foto Despues</p>
                                                ${imagenvs}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                `;
                            }
                        }

                        labelEquipoImg += `
                        <div class="row">
                            <div class="col-md-12">
                                <label class="m-b-10">Imagen: <strong> ${res[i].ticketEquipoImagen[a].cat_EquipoImagen.nombre} </strong></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                ${imagen}
                            </div>
                        </div>
                        `;
                    }

                    
                }

                html += `
                <div class="accordion-panel">
                    <div class=" accordion-heading" role="tab" id="headingImg${res[i].id}">
                        <h3 class="card-title accordion-title">
                            <a class="accordion-msg" data-toggle="collapse" data-parent="#accordion" href="#collapseImg${res[i].id}" aria-expanded="false" aria-controls="collapseImg${res[i].id}">
                                ${res[i].equipo.serie}
                            </a>
                        </h3>
                    </div>
                    <div id="collapseImg${res[i].id}" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingImg${res[i].id}">
                        <div class="accordion-content accordion-desc">
                            ${labelEquipoImg}
                        </div>
                    </div>
                </div>
                `;
            }
            $("#ImagenesEquipoImg").html(html);
        }

    });
}

function CargaGrafica1(dbData) {
    console.log(dbData);
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
            text: 'Ticket procesado o en proceso ',
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

function Visualizar(IdArchivo, IdTicket) {
    var protocol = window.location.protocol;
    var host = window.location.host;

    var image = new Image();
    var src = protocol + "//" + host + "/Administracion/PDF/PDFViewArchivoAdicional?cont=" + IdTicket + "&archivo=" + IdArchivo;
    image.src = src;

    $("#ModalViewOrdenTrabajo").modal("show");

    $('#pdfFrame').attr('src', src);
}
 
function VisualizarImagen(IdArchivo, IdTicket) {
    var protocol = window.location.protocol;
    var host = window.location.host;

    var src = protocol + "//" + host + "/Administracion/PDF/ImagenViewArchivoAdicional?cont=" + IdTicket + "&archivo=" + IdArchivo;

    $("#ModalViewImagen").modal("show");
    $('#imageFrame').attr('src', src);
}


function Descargar(IdArchivo, IdTicket) {
    var protocol = window.location.protocol;
    var host = window.location.host;

    var src = protocol + "//" + host + "/Administracion/PDF/DescargaViewArchivoAdicional?cont=" + IdTicket + "&archivo=" + IdArchivo;

    window.location.href = src;  
}


function NoAtendido(Id) {
    swal({
        title: "¿Estás seguro?",
        text: "Esta acción no se puede deshacer. ¿Deseas continuar?",
        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si,No atendido!",
        closeOnConfirm: false
    },
        function () {
            const ticket = {
                Id: Id,
            };

            fetch("/TicketUnitario/Ticket/Ticket_Procesar_NoAtendido", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(ticket)
            })
                .then(response => response.json())
                .then(response => {
                    if (response.id === 1) {
                        window.location.href = "/TicketUnitario/Ticket/MisTickets";
                    }
                    else {
                        swal({
                            title: "Error",
                            text: "No es posible realizar la operación seleccionada!",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        })
                            .then(willDelete => {
                            });
                    }
                });
        });

}