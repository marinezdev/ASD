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

function CargarOrdenTrabajo(Id) {

    var Div = "" +
        "<form id='uploadForm" + Id + "' enctype='multipart/form-data'>" +
            "<div class='row'>" +
                "<div class='col-md-12 col-lg-12'>" +
                    "<input type='file' id='fileInput' name='file' style='visibility:hidden' onchange='ValidaUploadFile(" + Id + ")'/>" +
                "</div>" +
                "<div class='col-md-12 col-lg-12'>" +
                    "<button type='button' class='btn btn-primary waves-effect waves-light' onclick='SubirArchivo()'>Subir Archivo</button>" +
                    "<br />" +
                "</div>" +
                "<div id='DivCargaArchivoBarra'>" +
                "</div>" +
            "</div>" +
        "</form>";


    $("#DivCargaArchivo").html(Div);
    $("#ModalOrdenTrabajo").modal("show");
}

function SubirArchivo() {
    $("#fileInput").click();
}

function ValidaUploadFile(Id) {
    var fileInput = document.getElementById('fileInput');

    if (fileInput.files.length > 0) {

        for (i = 0; i < fileInput.files.length; i++) {
            var Formato = "";
            var Carga = false;
            var ext = fileInput.files[i].name.split('.').pop();
            Extencion = fileInput.files[i].name.split('.').pop();

            // Convertimos en min�scula porque
            // la extensi�n del archivo puede estar en may�scula
            ext = ext.toLowerCase();

            if (ext == 'pdf') {
                Carga = true;
            } else {
                Carga = false;
                Formato = ext;
                /*break;*/
            }

            if (Carga) {
                var Div = "<div class='col-md-12 col-lg-12'>" +
                    "<progress id='progressBar' style='width:100%; height: 30px;' value='0' max='100'></progress>" +
                    "</div>";

                $("#DivCargaArchivoBarra").html(Div);

                UploadFile(Id);

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

                toastr.error("Intentas cargar un formato no permitido: <strong>" + Formato + "</strong> Formato de archivo permitido: <strong>.pdf </strong>", "Notificacion");
            }
        }
    }
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
    //window.location.href = '/Ticket/Ticket/Editar?cont=' + Id;
}

function UploadFile(Id) {

    var progressBar = document.getElementById('progressBar');
    var formData = new FormData();
    var fileInput = document.getElementById('fileInput');

    formData.append('file', fileInput.files[0]);
    formData.append('Id', Id);

    fetch('/Ticket/OrdenTrabajo/UploadFile', {
        method: 'POST',
        body: formData,
        headers: {
            // Puedes necesitar configurar los encabezados seg�n tu API
        },
        // Importante: Desactiva el cach� para evitar problemas de lectura de FormData
        cache: 'no-cache',
    })
    .then(response => {
        
        var total = response.headers.get('Content-Length');
        var reader = response.body.getReader();
        var loaded = 0;

        return reader.read().then(function process(result) {
            if (result.done) {

                var IdEstatus = response.headers.get('Estatus');
                const select = document.querySelector("#Ticket_EstatusOrdenTrabajo" + Id);

                select.value = IdEstatus;

                $("#BtnCerrarCargaArchivo").click();

                swal({
                        title: "Orden de trabajo registrada.",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                        showCancelButton: true,
                        confirmButtonColor: "#3070A9",
                        confirmButtonText: "Aceptar",
                        confirmButtonText: "Notificar a proveedor !",
                        cancelButtonText: "No, notificar !",
                        closeOnConfirm: false,
                        closeOnCancel: false
                    },
                    function (isConfirm) {
                        if (isConfirm) {
                            swal.close();
                            NotificarCargaOrdenTrabajo(Id);
                        } else {
                            swal.close();
                        }
                    }
                );


                // Carga completada
                console.log('Carga completada');
                console.log(IdEstatus);
                //console.log("id del ticket: " + Id)

                return;
            }

            loaded += result.value.length;
            
            progressBar.value = (loaded / total) * 100;

            // Continuar leyendo el siguiente fragmento del cuerpo de la respuesta
            return reader.read().then(process);
        });
    })
    .then(data => {
        /*console.log('�xito:', data);*/
    })
    .catch(error => {
       /* console.error('Error:', error);*/
    });
}

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

function CambioEstatusTicket(Id) {
    signaturePad.clear();

    var _Estaus = $("#Ticket_EstatusTicket" + Id + " option:selected").text();

    if (_Estaus == "Validado por sukarne") {
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

        toastr.error("Por el momento no puedes cambiar de estatus: <strong> " + _Estaus + " </strong>", "Notificacion");
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
        const select = document.querySelector("#Ticket_EstatusTicket" + Id);
        select.value = res.ticket.cat_EstatusTicket.id;

    });
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

        fetch("/Ticket/Ticket/UpdateTicket_Procesar_SupervisorCliente", {
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

                    toastr.error("Por el momento no puedes cambiar de estatus: <strong> " + _Estaus + " </strong>", "Notificacion");

                }
            });
    }
}

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

    const Ticket = {
        Id: Id,
        Detalle: $('#Modal_NEstatusOrdenTrabajo_Observaciones').val(),
    }

    const Cat_EstatusOrdenTrabajo = {
        Id: $("#Ticket_EstatusOrdenTrabajo" + Id + " option:selected").val(),
    }

    const _modelo = {
        Ticket: Ticket,
        Cat_EstatusOrdenTrabajo: Cat_EstatusOrdenTrabajo,
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

function CambioFechaAtencion(IdTicket, Id) {

    var Div = "" +
        "<div class='row'>" +
            "<div class='col-xl-6'>" +
                "<button type='button' class='btn btn-default waves-effect waves-light col-xl-12' data-dismiss='modal'><i class='icofont icofont-close-squared-alt'></i><span class='m-l-12'> Cancelar  </span></button>" +
            "</div>" +
            "<div class='col-xl-6' id='BtnContinuarOperacionFecha'>" +
                "<button type='button' class='btn btn-primary waves-effect waves-light col-xl-12' data-original-title='.icofont-home' onclick='AceptarFechaAtencion(" + IdTicket +"," + Id + ")'><i class='icofont icofont-envelope'></i><span class='m-l-12'> Aceptar fecha  </span></button>" +
            "</div>" +
        "</div>";

    var Div2 = "" +
        "<div class='row'>" +
            "<div class='col-xl-6'>" +
                "<button type='button' id='BtnNuevaFechaAtencion' class='btn btn-default waves-effect waves-light col-xl-12' onclick='NuevaFechaAtencion(" + IdTicket + ")'><i class='icofont icofont-calendar'></i><span class='m-l-12'> Nueva fecha de atención </span></button>" +
            "</div>" +
        "</div>";

    $("#DivFechaAtencion").html(Div);
    $("#DivNuevaFechaAtencion").html(Div2);


    $("#ModalFechaAtencion").modal("show");
    $("#Modal_NuevaFecha_FechaAtencion").prop("disabled", true);

    var date = $("#Ticket_NuevaFecha" + IdTicket).text().trim();

    var fechaInput = $("#Modal_NuevaFecha_FechaAtencion");

    // Obtener el valor de la fecha
    var fechaValor = date;

    // Asignar el valor al campo input date
    fechaInput.val(fechaValor);
}

function AceptarFechaAtencion(IdTicket, Id) {

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

    var Notificar = 0;
    if ($('#Modal_FechaAtencion_Notificar').is(':checked')) {
        Notificar = 1;
    }

    const Ticket = {
        id: IdTicket,
    }

    const _modelo = {
        Id: Id,
        Ticket: Ticket,
        Observaciones: $('#Modal_NuevaFecha_Observaciones').val(),
        Notificar: Notificar,
    };

    fetch("/Ticket/NuevaFechaAtencion/UpdateNuevaFechaAtencion_Aceptar", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        if (res != null) {
            if (res.id == -1) {
                toastr.error("No es posible aceptar la fecha de atención, el ticket ya fue atendido", "Notificacion");
            } else if (res.id == 0) {
                toastr.error("No es posible aceptar la fecha de atención", "Notificacion");
            } else if (res.id > 0) {

                $("#ModalFechaAtencion").modal("hide");
                var Div = " ";
                $("#Ticket_NuevaFechaLabel" + IdTicket).html(Div);

                swal({
                    title: "Fecha de atención aceptada.",
                    imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                    showCancelButton: false,
                    confirmButtonColor: "#3070A9",
                    confirmButtonText: "Aceptar",
                    closeOnConfirm: false,
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
            toastr.error("No es posible acpetar la fecha de atención", "Notificacion");
        }
    });
}

function NuevaFechaAtencion(IdTicket) {

    $("#Modal_NuevaFecha_FechaAtencion").removeAttr('disabled');
    $("#BtnNuevaFechaAtencion").hide();
    
    $("#BtnContinuarOperacionFecha").html("<button type='button' class='btn btn-primary waves-effect waves-light col-xl-12' data-original-title='.icofont-home' onclick='CrearNuevaFechaAtencion(" + IdTicket + ")'><i class='icofont icofont-envelope'></i><span class='m-l-12'> Registrar nueva fecha  </span></button>");
}

function CrearNuevaFechaAtencion(Id) {

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
        if ($('#Modal_FechaAtencion_Notificar').is(':checked')) {
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

        fetch("/Ticket/NuevaFechaAtencion/CreateNuevaFechaAtencionSuper", {
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

function NotificarCargaOrdenTrabajo(Id) {
    const _modelo = {
        Id: Id,
    };

    fetch("/Ticket/OrdenTrabajo/Notificacion_NuevaOrdenTrabajo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        console.log(res);
    });
}