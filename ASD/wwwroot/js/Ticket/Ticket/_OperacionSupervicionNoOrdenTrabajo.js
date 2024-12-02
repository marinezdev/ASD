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

        toastr.error("Por el momento no puedes cambiar de estatus: <strong> " + _Estaus +" </strong>", "Notificacion");
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
            window.location.href = '/Ticket/Ticket/Detalle?cont=' + res.url;

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

function CambioCuadrillaTicket(Id) {

    const _modelo = {
        Id: $("#Ticket_Cuadrilla" + Id + " option:selected").val(),
    };

    fetch("/Empresa/CuadrillaUsuario/GetCuadrillaUsuario_Seleccionar_IdCuadrilla", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        $("#Ticket_UsuarioAtencion" + Id).empty();

        var option = $(document.createElement('option'));
        option.text("Seleccionar");
        option.val(0);

        $("#Ticket_UsuarioAtencion" + Id).append(option);

        if (res === null) {

        } else if (res.length > 0) {

            $(res).each(function () {
                var option = $(document.createElement('option'));

                option.text(this.persona.nombre.toUpperCase() + " " + this.persona.apellidoPaterno.toUpperCase() + " " + this.persona.apellidoMaterno.toUpperCase());
                option.val(this.usuario.id);

                $("#Ticket_UsuarioAtencion" + Id).append(option);
            });
        }

    });
}

function CambioUsuarioAtencion(Id) {

    $("#Modal_NAsignacion_Notificar").prop("checked", false);

    $("#TituloCuadrilla").text($("#Ticket_Cuadrilla" + Id + " option:selected").text());
    $("#TituloCuadrillaUsaurio").text($("#Ticket_UsuarioAtencion" + Id + " option:selected").text());

    var Div = "" +
        "<div class='row'>" +
            "<div class='col-xl-6'>" +
                "<button type='button' class='btn btn-default waves-effect waves-light col-xl-12' data-dismiss='modal' onclick='CancelarCuadrilla(" + Id + ")'><i class='icofont icofont-close-squared-alt'></i><span class='m-l-12'> Cancelar  </span></button>" +
            "</div>" +
            "<div class='col-xl-6'>" +
                "<button type='button' class='btn btn-primary waves-effect waves-light col-xl-12' data-original-title='.icofont-home' onclick='AsignarCuadrilla(" + Id + ")'><i class='icofont icofont-envelope'></i><span class='m-l-12'> Asignar  </span></button>" +
            "</div>" +
        "</div>";

    $("#DivAsignacionTicket").html(Div);
    $("#ModalAsignarTicket").modal({
        backdrop: "static"
    });
}

function AsignarCuadrilla(Id) {

    var Notificar = 0;
    if ($('#Modal_NAsignacion_Notificar').is(':checked')) {
        Notificar = 1;
    }

    const Ticket = {
        Id: Id
    }

    const Cuadrilla = {
        Id: $("#Ticket_Cuadrilla" + Id + " option:selected").val()
    }

    const Usuario = {
        Id: $("#Ticket_UsuarioAtencion" + Id + " option:selected").val()
    }

    const _modelo = {
        Ticket: Ticket,
        Cuadrilla: Cuadrilla,
        Usuario: Usuario,
        Observaciones: $("#Modal_NAsignacion_Observaciones").val(),
        Notificar: Notificar
    };
    

    fetch("/Ticket/TicketCuadrilla/CreateTicketCuadrilla", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {

        console.log(res);

        if (res.ticket.id > 0) {

            CargaNuevoEstatusTicket(Id);

            $("#ModalAsignarTicket").modal("hide");
            
            swal({
                title: "Ticket asignado.",
                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                showCancelButton: false,
                confirmButtonColor: "#3070A9",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
            },
                function (isConfirm) {
                    if (isConfirm) {
                        swal.close();

                        $('#Modal_NAsignacion_Observaciones').val("");

                    }
                }
            );

        } else {

            CancelarCuadrilla(Id);
        }

    });
}

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

function CancelarCuadrilla(Id) {

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
          const select = document.querySelector("#Ticket_Cuadrilla" + Id);
          const selectU = document.querySelector("#Ticket_UsuarioAtencion" + Id);
         
        if (res.cuadrilla == null) {
            select.options[0].selected = true;
            selectU.options[0].selected = true;

        } else {
            select.value = res.cuadrilla.id; 

            const _modelo = {
                Id: res.cuadrilla.id,
            };

            fetch("/Empresa/CuadrillaUsuario/GetCuadrillaUsuario_Seleccionar_IdCuadrilla", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_modelo)
            })
            .then(res2 => res2.json())
            .then(res2 => {
                $("#Ticket_UsuarioAtencion" + Id).empty();

                var option = $(document.createElement('option'));
                option.text("Seleccionar");
                option.val(0);

                $("#Ticket_UsuarioAtencion" + Id).append(option);

                if (res2 === null) {

                } else if (res2.length > 0) {

                    $(res2).each(function () {
                        var option = $(document.createElement('option'));

                        option.text(this.persona.nombre.toUpperCase() + " " + this.persona.apellidoPaterno.toUpperCase() + " " + this.persona.apellidoMaterno.toUpperCase());
                        option.val(this.usuario.id);

                        $("#Ticket_UsuarioAtencion" + Id).append(option);
                    });

                    selectU.value = res.cuadrillaUsuario.usuario.id; 
                }

            });
        }
    });
}
function CambioFechaAtencion(Id) {
    var Div = "" +
        "<div class='row'>" +
            "<div class='col-xl-6'>" +
                "<button type='button' class='btn btn-default waves-effect waves-light col-xl-12' data-dismiss='modal' onclick=''><i class='icofont icofont-close-squared-alt'></i><span class='m-l-12'> Cancelar  </span></button>" +
            "</div>" +
            "<div class='col-xl-6'>" +
        "<button type='button' class='btn btn-primary waves-effect waves-light col-xl-12' data-original-title='.icofont-home' onclick='NuevaFechaAtencion(" + Id + ")'><i class='icofont icofont-envelope'></i><span class='m-l-12'> Solicitar nueva fecha   </span></button>" +
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

        fetch("/Ticket/NuevaFechaAtencion/CreateNuevaFechaAtencion", {
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

                    var Div = "<div class='label-main'><label class='label bg-warning'> Por validar </label></div>";

                    $("#Ticket_NuevaFechaLabel" + Id).html(Div);
                    $("#Ticket_NuevaFecha" + Id).html($("#Modal_NuevaFecha_FechaAtencion").val());

                    swal({
                        title: "Nueva fecha de atención registrada.",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                        showCancelButton: false,
                        confirmButtonColor: "#3070A9",
                        confirmButtonText: "Aceptar",
                        closeOnConfirm: false,
                        html: "<p>La nueva fecha aparecerá cuando sea aceptada por sukarne.</p>",
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

/**
 * 
 * Modulo view orden de trabajo
 */

function MostrarOrdenTrabajo(Id) {

    const _modelo = {
        Id: Id,
    };

    fetch("/Ticket/OrdenTrabajo/MostrarOrdenTrabajo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
        .then(res => res.json())
        .then(res => {

            console.log(res);

            if (res.id > 0) {
                var protocol = window.location.protocol;
                var host = window.location.host;

                var image = new Image();
                var src = protocol + "//" + host + "/Administracion/PDF/PDFView?cont=" + Id;
                image.src = src;

                $("#ModalViewOrdenTrabajo").modal("show");

                $('#pdfFrame').attr('src', src);

            } else {
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

                toastr.error("No se ha registrado una orden de trabajo", "Notificacion");
            }
        });
}

/**
 * ***********************************
 * Modulo cambio de estatus orden de trabajo
 */
function CambioEstatusOrdenTrabajoTicket(Id) {

    if ($("#Ticket_EstatusOrdenTrabajo" + Id + " option:selected").val() > 1) {
        $("#TituloEstatusOrdenTrabajo").text($("#Ticket_EstatusOrdenTrabajo" + Id + " option:selected").text());
    }


    var Div = "<button type='button' class='btn btn-default waves-effect waves-light' data-dismiss='modal' onclick='CancelarCambioEstatusOrdenTrabajo(" + Id + ")'>" +
        "<i class='icofont icofont-close-squared-alt'></i><span class='m-l-12'> Cerrar </span>" +
        "</button>" +
        "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='CambioEstatusOrdenTrabajo(" + Id + ")'>" +
        "<i class='icofont icofont-save'></i><span class='m-l-12'> Actualizar  </span>" +
        "</button>" +
        "";

    $("#DivEstatusOrdenTrabajo").html(Div);

    $("#ModalCambioEstatusOrdenTrabajo").modal({
        backdrop: "static"
    });
}

function CancelarCambioEstatusOrdenTrabajo(Id) {

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

            const select = document.querySelector("#Ticket_EstatusOrdenTrabajo" + Id);

            if (res.cat_EstatusOrdenTrabajo != null) {
                select.value = res.cat_EstatusOrdenTrabajo.id;
            } else {
                select.options[0].selected = true;
            }

        });
}

function CambioEstatusOrdenTrabajo(Id) {

    var Notificar = 0;
    if ($('#Modal_NEstatusOrdenTrabajo_Notificar').is(':checked')) {
        Notificar = 1;
    }

    const Ticket = {
        Id: Id,
        Observaciones: $('#Modal_NEstatusOrdenTrabajo_Observaciones').val(),
    }

    const Cat_EstatusOrdenTrabajo = {
        Id: $("#Ticket_EstatusOrdenTrabajo" + Id + " option:selected").val(),
    }

    const _modelo = {
        Ticket: Ticket,
        Cat_EstatusOrdenTrabajo: Cat_EstatusOrdenTrabajo,
        Notificar: Notificar,
    };

    fetch("/Ticket/OrdenTrabajo/UpdateOrdenTrabajo_ActualizarEstatus", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
        .then(res => res.json())
        .then(res => {

            if (res.id > 0) {
                $("#ModalCambioEstatusOrdenTrabajo").modal("hide");

                swal({
                    title: "Orden de trabajo actualizada.",
                    imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                    showCancelButton: false,
                    confirmButtonColor: "#3070A9",
                    confirmButtonText: "Aceptar",
                    closeOnConfirm: false,
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            swal.close();

                            $('#Modal_NEstatusOrdenTrabajo_Observaciones').val("");

                        }
                    }
                );

                // habilitar si cambio a acpetada
                var text = $("#Ticket_EstatusOrdenTrabajo" + Id + " option:selected").text();

                if (text == "Aceptada") {
                    $("#Ticket_Cuadrilla" + Id + " ").prop('disabled', false);
                    $("#Ticket_UsuarioAtencion" + Id + " ").prop('disabled', false);
                    /*$("#Ticket_EstatusTicket" + Id + " ").prop('disabled', false);*/
                    $("#Ticket_Prioridad" + Id + " ").prop('disabled', false);
                    $("#Ticket_EstatusOrdenTrabajo" + Id + " ").prop('disabled', true);
                } else {
                    $("#Ticket_Cuadrilla" + Id + " ").prop('disabled', true);
                    $("#Ticket_UsuarioAtencion" + Id + " ").prop('disabled', true);
                   /* $("#Ticket_EstatusTicket" + Id + " ").prop('disabled', true);*/
                    $("#Ticket_Prioridad" + Id + " ").prop('disabled', true);
                }

            } else {
                swal({
                    title: "Orden de trabajo no actulizada",
                    text: "No es posible actualizar la orden de trabajo. !",
                    imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                    buttons: true,
                    dangerMode: true,
                }).then((willDelete) => {
                    CancelarCambioEstatusOrdenTrabajo(Id);
                });
            }
        });
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