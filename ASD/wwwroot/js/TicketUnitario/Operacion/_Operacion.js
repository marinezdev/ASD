$('#MenuOperacion').addClass("active");
$('#MenuOperacionTickets').addClass("active");
let signaturePad;

$(document).ready(function () {
    // Single Search Select
    $(".js-example-basic-single").select2();


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
    const canvas = $("#Firma");
    signaturePad = new SignaturePad(canvas[0]);
});

function EditarTicket(Id) {
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
            window.location.href = '/Ticket/Ticket/Editar?cont=' + res.url;
        });
}
/**
 * ***********************************
 * Modulo cambio de estatus ticket
 */

function CambioEstatusTicket(Id) {
    signaturePad.clear();

    var _Estaus = $("#Ticket_EstatusTicket" + Id + " option:selected").text();

    console.log(_Estaus);

    if (_Estaus == "Validado por asae") {
        $("#TituloEstatusTicket").text(_Estaus);

        var Div = "<button type='button' class='btn btn-default waves-effect waves-light' data-dismiss='modal' onclick='CancelarCambioEstatus(" + Id + ")'>" +
            "<i class='icofont icofont-close-squared-alt'></i><span class='m-l-12'> Cerrar </span>" +
            "</button>" +
            "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='CambioEstatus(" + Id + ")'>" +
            "<i class='icofont icofont-save'></i><span class='m-l-12'> Actualizar  </span>" +
            "</button>" +
            "";

        $("#DivEstatusTicket").html(Div);
        $("#ModalCambioEstatus").modal({
            backdrop: "static"
        });

    } else if (_Estaus == "Atendido") {

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
                window.location.href = "/TicketUnitario/Ticket/Atender?cont=" + res.url; // Cambia la URL por la correcta
            });
    } else if (_Estaus == "Programado") {
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
                    window.location.href = "/TicketUnitario/Ticket/Programar?cont=" + res.url; // Cambia la URL por la correcta
                });
    } else {
        CancelarCambioEstatus(Id);

        toastr.options = {
            "closeButton": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "500",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "iconClass": "toast-error"
        };

        toastr.error("Por el momento no puedes cambiar de estatus: <strong> " + _Estaus + " </strong>", "Notificacion");
    }

}


function CambioEstatus(Id) {

    if (signaturePad.isEmpty()) {
        toastr.options = {
            "closeButton": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "iconClass": "toast-error"
        };

        toastr.error("Por favor, proporcione una firma de confirmación.", "Notificacion");
    }
    else {

        var Notificar = 0;
        if ($('#Modal_NEstatusTicket_Notificar').is(':checked')) {
            Notificar = 1;
        }

        const Cat_EstatusTicket = {
            Id: $("#Ticket_EstatusTicket" + Id + " option:selected").val(),
        }

        const _modelo = {
            Id: Id,
            Cat_EstatusTicket: Cat_EstatusTicket,
            Detalle: $('#Modal_NEstatusTicket_Observaciones').val(),
            Notificar: Notificar,
        };

        fetch("/Ticket/Ticket/UpdateTicket_Procesar_SupervisorAsae", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(_modelo)
        })
            .then(res => res.json())
            .then(res => {

                if (res.id > 0) {
                    const ticket = {
                        Id: Id,
                        Firma: signaturePad.toDataURL()
                    };
                    fetch("/Administracion/FirmaElectronica/Ticket_Procesar_Supervisor_Signature", {
                        method: "POST",
                        headers: { "Content-Type": "application/json; charset=utf-8" },
                        body: JSON.stringify(ticket)
                    })
                        .then(res => res.json())
                        .then(res => {
                            if (res.id > 0) {
                                $("#ModalCambioEstatus").modal("hide");

                                swal({
                                    title: "Ticket validado.",
                                    imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                                    showCancelButton: false,
                                    confirmButtonColor: "#3070A9",
                                    confirmButtonText: "Aceptar",
                                    closeOnConfirm: false,
                                },
                                    function (isConfirm) {
                                        if (isConfirm) {
                                            swal.close();

                                            $('#Modal_NEstatusTicket_Observaciones').val("");

                                        }
                                    }
                                );
                            } else {
                                CancelarCambioEstatus(Id);

                                toastr.options = {
                                    "closeButton": true,
                                    "progressBar": true,
                                    "positionClass": "toast-top-right",
                                    "showDuration": "500",
                                    "hideDuration": "1000",
                                    "timeOut": "5000",
                                    "extendedTimeOut": "1000",
                                    "iconClass": "toast-error"
                                };

                                toastr.error("Por el momento no puedes cambiar de estatus firma", "Notificacion");
                            }

                        });

                } else {

                    CancelarCambioEstatus(Id);

                    toastr.options = {
                        "closeButton": true,
                        "progressBar": true,
                        "positionClass": "toast-top-right",
                        "showDuration": "500",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "iconClass": "toast-error"
                    };

                    toastr.error("Por el momento no puedes cambiar de estatus 1", "Notificacion");

                }
            });
    }

    
}
function CancelarCambioEstatus(Id) {

    const _modelo = {
        Id: Id,
    };

    fetch("/Ticket/Ticket/GetTicket", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {

        console.log(res);

        const select = document.querySelector("#Ticket_EstatusTicket" + Id);
        select.value = res.ticket.cat_EstatusTicket.id;

    });
}

/***************************************** */
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
            window.location.href = '/TicketUnitario/Ticket/Detalle?cont=' + res.url;

        });
}

function CambioAsignacionEmpresa(Id) {

    const Ticket = {
        Id: Id,
    }

    const Cat_AsignacionEmpresa = {
        Id: $("#Ticket_AsignacionEmpresa" + Id + " option:selected").val(),
    }

    const _modelo = {
        Ticket: Ticket,
        Cat_AsignacionEmpresa: Cat_AsignacionEmpresa,
    };

    fetch("/Ticket/TicketAsignacionEmpresa/CreateTicketAsignacionEmpresa", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        if (res.id == 0) {
            CancelarCambioeAsignacionEmpresa(Id);
        }
    });
}

function CancelarCambioeAsignacionEmpresa(Id) {

    const _modelo = {
        Id: Id,
    };

    fetch("/Ticket/Ticket/GetTicket", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {

        const select = document.querySelector("#Ticket_AsignacionEmpresa" + Id);
        select.value = res.cat_AsignacionEmpresa.id;


    });
}

/***************************************** */
/***************************************** */

function CargaNuevoEstatusTicket(Id) {

    const _modelo = {
        Id: Id,
    };

    fetch("/Ticket/Ticket/GetTicket", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {

        console.log(res);

        const select = document.querySelector("#Ticket_EstatusTicket" + Id);
        select.value = res.ticket.cat_EstatusTicket.id;

     
        if (res.ticket.cat_EstatusTicket.nombre == "Programado") {
            $("#Ticket_Cuadrilla" + Id + " ").prop('disabled', true);
            $("#Ticket_UsuarioAtencion" + Id + " ").prop('disabled', true);
        } else {
            $("#Ticket_Cuadrilla" + Id + " ").prop('disabled', false);
            $("#Ticket_UsuarioAtencion" + Id + " ").prop('disabled', false);
        }

    });
}

/***************************************** */
/***************************************** */

function CambioFechaAtencion(Id) {
    var Div = "" +
        "<div class='row'>" +
            "<div class='col-xl-6'>" +
                "<button type='button' class='btn btn-default waves-effect waves-light col-xl-12' data-dismiss='modal' onclick=''><i class='icofont icofont-close-squared-alt'></i><span class='m-l-12'> Cancelar  </span></button>" +
            "</div>" +
            "<div class='col-xl-6'>" +
        "<button type='button' class='btn btn-primary waves-effect waves-light col-xl-12' data-original-title='.icofont-home' onclick='NuevaFechaAtencion(" + Id + ")'><i class='icofont icofont-envelope'></i><span class='m-l-12'> Registrar nueva fecha   </span></button>" +
            "</div>" +
        "</div>";

    $("#DivFechaAtencion").html(Div);
    $("#ModalFechaAtencion").modal({
        backdrop: "static"
    });
}

function NuevaFechaAtencion(Id) {
    toastr.options = {
        "closeButton": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "showDuration": "500",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "iconClass": "toast-error"
    };

    if (ValidarNuevaFechaAtencion()) {

        var Notificar = 0;
        if ($('#Modal_NuevaFechaAtencion_Notificar').is(':checked')) {
            Notificar = 1;
        }

        const Ticket = {
            Id: Id,
        };

        const _model = {
            Ticket: Ticket,
            FechaAtencion: $("#Modal_NuevaFecha_FechaAtencion").val(),
            Observaciones: $("#Modal_NuevaFecha_Observaciones").val(),
            Notificar: Notificar
        };

        fetch("/Ticket/NuevaFechaAtencion/CreateNuevaFechaAtencion_TicketUnitario", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(_model)
        })
        .then(res => res.json())
        .then(res => {

            if (res != null) {
                if (res.id == -1) {
                    toastr.error("No es posible cambiar la fecha de atención, el ticket ya fue atendido", "Notificacion");
                } else if (res.id == 0) {
                    toastr.error("No es posible cambiar la fecha de atención", "Notificacion");
                } else if (res.id > 0) {

                    $("#ModalFechaAtencion").modal("hide");
                    var Div = "";

                    $("#Ticket_NuevaFechaLabel" + Id).html(Div);
                    $("#Ticket_NuevaFecha" + Id).html($("#Modal_NuevaFecha_FechaAtencion").val());

                    swal({
                        title: "Nueva fecha de atención registrada.",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                        showCancelButton: false,
                        confirmButtonColor: "#3070A9",
                        confirmButtonText: "Aceptar",
                        closeOnConfirm: false,
                        /* html: "<p> La nueva fecha aparecerá cuando sea aceptada por sukarne.</p>",*/
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();

                                $('#Modal_NuevaFecha_FechaAtencion').val("");
                                $('#Modal_NuevaFecha_Observaciones').val("");
                            }
                        }
                    );
                }
            } else {
                toastr.error("No es posible cambiar la fecha de atención", "Notificacion");
            }
        });
    } else {
        
        toastr.error("Completa el formulario", "Notificacion");
    }
}

function ValidarNuevaFechaAtencion() {

    var result = false;

    $('#Modal_NuevaFecha_FechaAtencion').css("border", "1px solid red");

    if ($('#Modal_NuevaFecha_FechaAtencion').val().length > 0) {
        $('#Modal_NuevaFecha_FechaAtencion').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_NuevaFecha_FechaAtencion').val().length > 0) {
        result = true;
    }

    return result;
}

/***************************************** */
/***************************************** */

function CambioPrioridad(Id) {
    swal({
        title: "Confirmación.",
        text: "Confirmación de cambio de estatus al ticket.",
        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
        showCancelButton: true,
        confirmButtonColor: "#3070A9",
        confirmButtonText: "Aceptar",
        closeOnConfirm: true,
    },
        function (isConfirm) {
            if (isConfirm) {
                const Cat_Prioridad = {
                    Id: $("#Ticket_Prioridad" + Id + " option:selected").val(),
                }

                const Ticket = {
                    Id: Id,
                    Cat_Prioridad: Cat_Prioridad
                }
                const _modelo = {
                    Ticket: Ticket,
                };

                fetch("/Ticket/Ticket/UpdateTicket_Actualizar_IdPrioridad", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(_modelo)
                })
                    .then(res => res.json())
                    .then(res => {

                        if (res.id > 0) {

                            swal({
                                title: "El ticket cambio de estatus.",
                                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                                showCancelButton: false,
                                confirmButtonColor: "#3070A9",
                                confirmButtonText: "Aceptar",
                                closeOnConfirm: false,
                            },
                                function (isConfirm) {
                                    if (isConfirm) {
                                        swal.close();
                                    }
                                }
                            );


                        } else {
                            swal({
                                title: "Estatus no actulizado",
                                text: "No es posible actualizar el estatus del ticket. !",
                                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                                buttons: true,
                                dangerMode: true,
                            }).then((willDelete) => {
                                CancelarCambioPrioridad(Id)
                            });
                        }
                    });
            } else {
                CancelarCambioPrioridad(Id)
            }
        }
    );
}

function CancelarCambioPrioridad(Id) {
    const _modelo = {
        Id: Id,
    };

    fetch("/Ticket/Ticket/GetTicket", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {

        console.log(res);

        const select = document.querySelector("#Ticket_Prioridad" + Id);
        select.value = res.ticket.cat_Prioridad.id;

    });
}