$('#MenuOperacion').addClass("active");
$('#MenuOperacionEscalacion').addClass("active");

$(document).ready(function () {

    var table = $('#mydatatable').DataTable({
        "dom": 'B<"float-left"i><"float-right"f>t<"float-left"l><"float-right"p><"clearfix">',
        "responsive": false,
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
        "order": [[0, "desc"]],
        columnDefs: [{ width: 300, targets: 0 }],
        //fixedColumns: true,
        paging: true,
        //scrollCollapse: true,
        //scrollX: true,
        //scrollY: 400
    });
    const canvas = $("#Firma");
    signaturePad = new SignaturePad(canvas[0]);
});

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

function navegacion(id) {
    if (id == 1) {
        $("#DivFlujo").addClass("Selecionado");
        $("#DivTiempoAtencion").removeClass("Selecionado");
        $("#DivEscalacion").removeClass("Selecionado");

        $("#DivFlujoContenido").show();
        $("#DivTiempoAtencionContenido").hide();
        $("#DivEscalacionContenido").hide(); 

    } else if (id == 2) {
        $("#DivFlujo").removeClass("Selecionado");
        $("#DivTiempoAtencion").addClass("Selecionado");
        $("#DivEscalacion").removeClass("Selecionado");

        $("#DivFlujoContenido").hide();
        $("#DivTiempoAtencionContenido").show();
        $("#DivEscalacionContenido").hide(); 

    } else if (id == 3) {
        $("#DivFlujo").removeClass("Selecionado");
        $("#DivTiempoAtencion").removeClass("Selecionado");
        $("#DivEscalacion").addClass("Selecionado");

        $("#DivFlujoContenido").hide();
        $("#DivTiempoAtencionContenido").hide();
        $("#DivEscalacionContenido").show(); 
    }
}


/************************************************************************************************************** */
/************************************************************************************************************** */

function AbrirModalResponsable() {

    if ($("#Escalacion_TipoServicio option:selected").val() > 0) {
        $("#Modal_Responsable_Usuario").val("0");
        $("#Modal_Responsable_Dia").val("1");
        $("#ModalResponsable").modal("show");

    } else {
        toastr.error("Selecciona un Tipo de servicio", "Notificacion");
    }
}

function RegistrarResponsable() {
    if (ValidarNuevoResponsable()) {

        const Flujo = {
            Id: $("#Escalacion_TipoServicio option:selected").val(),
        }

        const Usuario = {
            Id : $("#Modal_Responsable_Usuario option:selected").val(),
        }

        const _model = {
            Flujo: Flujo,
            Usuario: Usuario,
            Dias: $('#Modal_Responsable_Dia').val(),
        }

        fetch("/Ticket/Escalacion/CreateEscalacion", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(_model)
        })
        .then(res => res.json())
        .then(res => {

            if (res.id > 0) {

                CargaResponsables();


                $("#btnCerrarModalResponsable").click();
                toastr.success("Nuevo responsable registrado.", "Notificacion");

            } else if (res.id = 0) {
                toastr.error("la operacion no se pudo ejecutar, intentelo mas tarde.", "Notificacion");
            } else if (res.id = -1) {
                toastr.error("El usuario que intentas agregar ya se encuentra registrado", "Notificacion");
            }

        });

    } else {
        toastr.error("Completa el formulario ", "Notificacion");
    }
}

function ValidarNuevoResponsable() {

    var result = false;

    $('#Modal_Responsable_Usuario').css("border", "1px solid red");
    $('#Modal_Responsable_Dia').css("border", "1px solid red");


    if ($("#Modal_Responsable_Usuario option:selected").val() > 0) {
        $('#Modal_Responsable_Usuario').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_Responsable_Dia').val().length > 0) {
        $('#Modal_Responsable_Dia').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($("#Modal_Responsable_Usuario option:selected").val() > 0) {
        if ($('#Modal_Responsable_Dia').val().length > 0) {
            result = true;
        }
    }

    return result;
}

function CargaResponsables() {

    if ($("#Escalacion_TipoServicio option:selected").val()> 0) {

        const Flujo = {
            Id: $("#Escalacion_TipoServicio option:selected").val(),
        }

        const _model = {
            Flujo: Flujo
        }

        fetch("/Ticket/Escalacion/GetEscalacion_Seleccionar_IdFlujo", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(_model)
        })
        .then(res => res.json())
            .then(res => {
                console.log(res);
                
            var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                                "<table class='table text-center table-striped'>" +
                                    "<thead class='thead-dark'>" +
                                        "<tr>" +
                                            "<th class='text-center'>Nombre</th>" +
                                            "<th class='text-center'>Rol</th>" +
                                            "<th class='text-center'>Email</th>" +
                                            "<th class='text-center'>Teléfono</th>" +
                                            "<th class='text-center'>Días</th>" +
                                            "<th class='text-center'></th>" +
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

                    var email = "";
                    var telefono = "";

                    if (this.email != null) {
                        email = this.email.email;
                    }

                    if (this.telefono != null) {
                        telefono = this.telefono.telefono;
                    }

                    Tabla += "<tr>" +
                                "<td>" +
                                this.persona.nombre + " " + this.persona.apellidoPaterno + " " + this.persona.apellidoMaterno +
                                "</td>" +
                                "<td>" +
                                this.cat_Rol.nombre +
                                "</td>" +
                                "<td>" +
                                email +
                                "</td>" +
                                "<td>" +
                                telefono +
                                "</td>" +
                                "<td>" +
                                this.escalacion.dias +
                                "</td>" +
                                "<td>" +
                        "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarResponsable(" + this.escalacion.id + ")'>" +
                                    "<i class='icofont icofont-trash'></i>" +
                                    "</button>" +
                                "</td>" +
                            "</tr>";
                });
            }

            $("#TabResponsables").html(TableHeader + Tabla + TableFooter);
        });
    }
    
}

function EliminarResponsable(id) {

    swal({
        title: "Confirmacion de movimiento.",
        text: "Eliminar responsable",
        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/delete.png",

        showCancelButton: true,
        cancelButtonColor: "#39444e",
        cancelButtonText: "Cancelar",
        
        confirmButtonColor: "#3070A9",
        confirmButtonText: "Aceptar",
        closeOnConfirm: false,
    },
    function (isConfirm) {
        if (isConfirm) {
            swal.close(); 
            const _model = {
                Id: id,
            }

            fetch("/Ticket/Escalacion/DeleteEscalacion", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
            .then(res => res.json())
            .then(res => {

                if (res.id > 0) {

                    CargaResponsables();

                    toastr.success("Responsable eliminado.", "Notificacion");

                } else if (res.id = -1) {
                    toastr.error("La operacion no se pudo ejecutar, intentelo mas tarde.", "Notificacion");
                }

            });

        } else {
            swal.close(); 
        }
    });
}

/************************************************************************************************************** */
/************************************************************************************************************** */

function AbrirModalHorario() {

    if ($("#Horario_TipoServicio option:selected").val() > 0) {
        $("#ModalHorario").modal("show");
        $("#Modal_horario").val("");

    } else {
        toastr.error("Selecciona un Tipo de servicio", "Notificacion");
    }
}

function RegistrarHorario() {
    if (ValidarHorario()) {

        const Flujo = {
            Id: $("#Horario_TipoServicio option:selected").val(),
        }

        const _model = {
            Flujo: Flujo,
            Hora: $('#Modal_horario').val(),
        }

        console.log(_model);

        fetch("/Ticket/EscalacionTiempo/CreateEscalacionTiempo", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(_model)
        })
        .then(res => res.json())
        .then(res => {

            if (res.id > 0) {

                CargaHorarios();

                $("#btnCerrarModalHorario").click();
                toastr.success("Nuevo horario registrado.", "Notificacion");

            } else if (res.id = 0) {
                toastr.error("la operacion no se pudo ejecutar, intentelo mas tarde.", "Notificacion");
            } else if (res.id = -1) {
                toastr.error("El horario que intentas agregar ya se encuentra registrado", "Notificacion");
            }

        });
    } else {
        toastr.error("Completa el formulario ", "Notificacion");
    }
}

function ValidarHorario() {
    var result = false;

    $('#Modal_horario').css("border", "1px solid red");

    if ($('#Modal_horario').val().length > 0) {
        $('#Modal_horario').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_horario').val().length > 0) {
        result = true;
    }

    return result;
}

function CargaHorarios() {
    if ($("#Horario_TipoServicio option:selected").val() > 0) {

        const Flujo = {
            Id: $("#Horario_TipoServicio option:selected").val(),
        }

        const _model = {
            Flujo: Flujo
        }

        fetch("/Ticket/EscalacionTiempo/GetEscalacionTiempo_Seleccionar_IdFlujo", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(_model)
        })
            .then(res => res.json())
            .then(res => {
                console.log(res);

                var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                    "<table class='table text-center table-striped'>" +
                        "<thead class='thead-dark'>" +
                            "<tr>" +
                                "<th class='text-center'>Horario</th>" +
                                "<th class='text-center'></th>" +
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
                            "</tr>";
                } else if (res.length > 0) {
                    $(res).each(function () {
                        Tabla += "<tr>" +
                            "<td>" +
                                this.hora +
                            "</td>" +
                            "<td>" +
                                "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarHorario(" + this.id + ")'>" +
                                "<i class='icofont icofont-trash'></i>" +
                                "</button>" +
                            "</td>" +
                            "</tr>";
                    });
                }

                $("#TabHorario").html(TableHeader + Tabla + TableFooter);
            });
    }
}

function EliminarHorario(id) {

    swal({
        title: "Confirmacion de movimiento.",
        text: "Eliminar horario",
        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/delete.png",

        showCancelButton: true,
        cancelButtonColor: "#39444e",
        cancelButtonText: "Cancelar",

        confirmButtonColor: "#3070A9",
        confirmButtonText: "Aceptar",
        closeOnConfirm: false,
    },
    function (isConfirm) {
        if (isConfirm) {
            swal.close();
            const _model = {
                Id: id,
            }

            fetch("/Ticket/EscalacionTiempo/DeleteEscalacionTiempo", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
                .then(res => res.json())
                .then(res => {

                    if (res.id > 0) {

                        CargaHorarios();

                        toastr.success("Horario eliminado.", "Notificacion");

                    } else if (res.id = -1) {
                        toastr.error("La operacion no se pudo ejecutar, intentelo mas tarde.", "Notificacion");
                    }

                });

        } else {
            swal.close();
        }
    });
}


/************************************************************************************************************** */
/************************************************************************************************************** */

function ConsultarEscalacion(id) {

    $("#CardMenuConfiguracion").hide();
    $("#CardAgregarUsuario").show();

    $("#Ticket_Responsable_Usuario").val("0");
    $("#Ticket_Responsable_Dia").val("1");

    CargaResponsablesTicket(id);

    var html = "<button type='button' class='btn btn-primary waves-effect waves-light m-r-10' data-toggle='tooltip' data-placement='top' title='' data-original-title='Agregar responsable a la lista' onclick='RegistrarReponsableTicket(" + id + ")'>" +
                    "<i class='ti-layers-alt'></i> Agregar " +
               "</button>";

    $("#DivBtnRegistrarResponsableTicket").html(html);
}

function AbrirConfiguracion() {
    $("#CardAgregarUsuario").hide();
    $("#CardMenuConfiguracion").show();
}

function RegistrarReponsableTicket(IdTicket) {
    if (ValidarResponsableTicket()) {
        const Usuario = {
            Id: $("#Ticket_Responsable_Usuario option:selected").val(),
        }

        const Ticket = {
            Id: IdTicket,
        }

        const _model = {
            Ticket: Ticket,
            Usuario: Usuario,
            Dias: $('#Ticket_Responsable_Dia').val(),
        }

        fetch("/Ticket/TicketEscalacion/CreateTicketEscalacion", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(_model)
        })
            .then(res => res.json())
            .then(res => {

                if (res.id > 0) {

                    CargaResponsablesTicket(IdTicket);

                    $("#Ticket_Responsable_Usuario").val("0");
                    $("#Ticket_Responsable_Dia").val("1");

                    toastr.success("Nuevo responsable registrado.", "Notificacion");

                } else if (res.id = 0) {
                    toastr.error("la operacion no se pudo ejecutar, intentelo mas tarde.", "Notificacion");
                } else if (res.id = -1) {
                    toastr.error("El usuario que intentas agregar ya se encuentra registrado", "Notificacion");
                }

            });
    } else {
        toastr.error("Completa el formulario ", "Notificacion");
    }
}

function CargaResponsablesTicket(IdTicket) {
        
    const Ticket = {
        Id: IdTicket,
    }

    const _model = {
        Ticket: Ticket
    }

    fetch("/Ticket/TicketEscalacion/GetTicketEscalacion_Seleccionar_IdTicket", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_model)
    })
    .then(res => res.json())
    .then(res => {
        console.log(res);

        var TableHeader = "<div class='col-sm-12 table-responsive'>" +
            "<table class='table text-center table-striped'>" +
            "<thead class='thead-dark'>" +
            "<tr>" +
            "<th class='text-center'>Nombre</th>" +
            "<th class='text-center'>Rol</th>" +
            "<th class='text-center'>Emil</th>" +
            "<th class='text-center'>Telefono</th>" +
            "<th class='text-center'>Dias</th>" +
            "<th class='text-center'></th>" +
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

                var email = "";
                var telefono = "";

                if (this.email != null) {
                    email = this.email.email;
                }

                if (this.telefono != null) {
                    telefono = this.telefono.telefono;
                }

                Tabla += "<tr>" +
                    "<td>" +
                    this.persona.nombre + " " + this.persona.apellidoPaterno + " " + this.persona.apellidoMaterno +
                    "</td>" +
                    "<td>" +
                    this.cat_Rol.nombre +
                    "</td>" +
                    "<td>" +
                    email +
                    "</td>" +
                    "<td>" +
                    telefono +
                    "</td>" +
                    "<td>" +
                    this.ticketEscalacion.dias +
                    "</td>" +
                    "<td>" +
                    "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarResponsableTicket(" + this.ticketEscalacion.id + ", " + IdTicket +")'>" +
                    "<i class='icofont icofont-trash'></i>" +
                    "</button>" +
                    "</td>" +
                    "</tr>";
            });
        }

        $("#TabResponsablesTicket").html(TableHeader + Tabla + TableFooter);
    });
    

}

function ValidarResponsableTicket() {
    var result = false;

    $('#Ticket_Responsable_Usuario').css("border", "1px solid red");
    $('#Ticket_Responsable_Dia').css("border", "1px solid red");


    if ($("#Ticket_Responsable_Usuario option:selected").val() > 0) {
        $('#Ticket_Responsable_Usuario').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Ticket_Responsable_Dia').val().length > 0) {
        $('#Ticket_Responsable_Dia').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($("#Ticket_Responsable_Usuario option:selected").val() > 0) {
        if ($('#Ticket_Responsable_Dia').val().length > 0) {
            result = true;
        }
    }

    return result;
}

function EliminarResponsableTicket(id, IdTicket) {

    swal({
        title: "Confirmacion de movimiento.",
        text: "Eliminar responsable",
        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/delete.png",

        showCancelButton: true,
        cancelButtonColor: "#39444e",
        cancelButtonText: "Cancelar",

        confirmButtonColor: "#3070A9",
        confirmButtonText: "Aceptar",
        closeOnConfirm: false,
    },
    function (isConfirm) {
        if (isConfirm) {
            swal.close();
            const _model = {
                Id: id,
            }

            fetch("/Ticket/TicketEscalacion/DeleteTicketEscalacion", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
            .then(res => res.json())
            .then(res => {
                if (res.id > 0) {

                    CargaResponsablesTicket(IdTicket);

                    toastr.success("Responsable eliminado.", "Notificacion");

                } else if (res.id = -1) {
                    toastr.error("La operacion no se pudo ejecutar, intentelo mas tarde.", "Notificacion");
                }
            });

        } else {
            swal.close();
        }
    });
}