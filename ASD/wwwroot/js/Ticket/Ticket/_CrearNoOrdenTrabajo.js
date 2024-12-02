$('#MenuOperacion').addClass("active");
$('#MenuOperacionNuevoTicket').addClass("active");

$(document).ready(function () {
    // Single Search Select
    $(".js-example-basic-single").select2();

    $("#NuevoTicket").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateForm()) {

            const Flujo = {
                Id: $("#Ticket_TipoServicio option:selected").val(),
            }

            const Cat_TipoServicio = {
                Id: $("#Ticket_TipoServicioCorrectivo option:selected").val(),
            }

            const TicketTipoServicio = {
                Cat_TipoServicio: Cat_TipoServicio,
                Valor: $('#Ticket_TipoServicioCantidad').val(),
            }

            const Sucursal = {
                Id : $("#Ticket_Sucursal option:selected").val()
            }

            const Cat_Prioridad = {
                Id : $("#Ticket_Prioridad option:selected").val(),
            }

            const Ticket = {
                Flujo : Flujo,
                Cat_Prioridad: Cat_Prioridad,
                Sucursal: Sucursal,
                Titulo: $('#Ticket_Titulo').val(),
                Detalle: $('#Ticket_Detalle').val(),
            }

            const FechaAtencion = {
                FechaAtencion: $('#Ticket_FechaProgramada').val(),
            }

            const _model = {
                Ticket: Ticket,
                TicketTipoServicio: TicketTipoServicio,
                FechaAtencion: FechaAtencion,
            };

            fetch("/Ticket/Ticket/CreateTicketNoOrdenTrabajo", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
            .then(res => res.json())
            .then(res => {
                if (res.id > 0) {
                    if (res.ticket.id > 0) {
                        swal({
                            title: "Nuevo ticket registrado.",
                            text: "Folio de seguimiento: " + res.folio.folio,
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                            showCancelButton: false,
                            confirmButtonColor: "#3070A9",
                            confirmButtonText: "Aceptar",
                            closeOnConfirm: false,
                        },
                            function (isConfirm) {
                                if (isConfirm) {
                                    location.reload();
                                }
                            });
                    } else {
                        swal({
                            title: "Ticket no registrado",
                            text: "No es posible registrar el ticket. !",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    }
                } else {
                    swal({
                        title: "Ticket registrado no",
                        text: "Debes de agregar un equipo a la lista. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                }
            });
        } 
    });

});

function ValidateForm() {
    var result = false;
    var correctivo = false;
    var Formulario = false;
    var posicion = "";

    $('#select2-Ticket_TipoServicio-container').css("border", "1px solid red");

    $('#Ticket_TipoServicioCorrectivo').css("border", "1px solid red");
    $('#Ticket_TipoServicioCantidad').css("border", "1px solid red");

    $('#Ticket_Titulo').css("border", "1px solid red");
    $('#select2-Ticket_Prioridad-container').css("border", "1px solid red");
    $('#Ticket_FechaProgramada').css("border", "1px solid red");
    $('#select2-Ticket_Sucursal-container').css("border", "1px solid red");

    if ($("#Ticket_TipoServicio option:selected").val() > 0) {
        $('#select2-Ticket_TipoServicio-container').css("border", "0px solid #d9dee3");
    }

    if ($("#Ticket_TipoServicioCorrectivo option:selected").val() > 0) {
        $('#Ticket_TipoServicioCorrectivo').css("border", "0px solid #d9dee3");
    }

    if ($('#Ticket_TipoServicioCantidad').val().length > 0) {
        $('#Ticket_TipoServicioCantidad').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Ticket_Titulo').val().length > 0) {
        $('#Ticket_Titulo').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($("#Ticket_Prioridad option:selected").val() > 0) {
        $('#select2-Ticket_Prioridad-container').css("border", "0px solid #d9dee3");
    }

    if ($('#Ticket_FechaProgramada').val().length > 0) {
        $('#Ticket_FechaProgramada').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($("#Ticket_Sucursal option:selected").val() > 0) {
        $('#select2-Ticket_Sucursal-container').css("border", "0px solid #d9dee3");
    }

    if ($("#Ticket_TipoServicio option:selected").val() == 2) {
        if ($("#Ticket_TipoServicioCorrectivo option:selected").val() > 0) {
            if ($('#Ticket_TipoServicioCantidad').val().length > 0) {
                correctivo = true;
            }
        }
    } else {
        correctivo = true;
    }

    if ($("#Ticket_TipoServicio option:selected").val() > 0) {
        if ($('#Ticket_Titulo').val().length > 0) {
            if ($("#Ticket_Prioridad option:selected").val() > 0) {
                if ($('#Ticket_FechaProgramada').val().length > 0) {
                    if ($("#Ticket_Sucursal option:selected").val() > 0) {

                        Formulario = true;

                    } else {
                        posicion = $("#Ticket_Sucursal").offset().top;
                    }
                } else {
                    posicion = $("#Ticket_FechaProgramada").offset().top;
                }
            } else {
                posicion = $("#Ticket_Prioridad").offset().top;
            }
        } else {
            posicion = $("#Ticket_Titulo").offset().top;
        }
    } else {
        posicion = $("#Ticket_TipoServicio").offset().top;
    }

    if (correctivo) {
        if (Formulario) {
            result = true;
        }
    } else {
        posicion = $("#Ticket_TipoServicioCorrectivo").offset().top;
    }

    if (!result) {
        $("html, body").animate({
            scrollTop: posicion - 100
        }, 500);
    }
    

    return result;
}


///////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////// AGREGAR EQUIPO ///////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////

function AgregarTicketEquipo() {
    if (ValidarTicketEquipo()) {

        const Sucursal = {
            Id: $("#Ticket_Sucursal option:selected").val(),
            Nombre: $("#Ticket_Sucursal option:selected").text(),
        }
        
        const _model = {
            Sucursal: Sucursal,
            Id: $("#Ticket_Equipo option:selected").val(),
            Serie: $("#Ticket_Equipo option:selected").text(),
        };

        fetch("/Ticket/TicketEquipo/AddListTicketEquipo", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(_model)
        })
        .then(res => res.json())
            .then(res => {
                
                var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                                    "<table class='table text-center table-striped'>" +
                                        "<thead class='thead-dark'>" +
                                            "<tr>" +
                                                "<tr>" +
                                                    "<th class='text-center'>Sucursal</th>" +
                                                    "<th class='text-center'>Equipo</th>" +
                                                    "<th class='text-center'></th>" +
                                                "</tr>" +
                                        "</thead>" +
                                        "<tbody>";

                
                var TableFooter =       "</tbody>" +
                                    "</table>" +
                                  "</div>";

                var Tabla = "";
                if (res === null) {
                    Tabla =  "<tr>" +
                                "<td>"  +
                                "</td>" +
                                "<td>"  +
                                "</td>" +
                                "<td>"  +
                                "</td>" +
                            "</tr>";
                } else if (res.length > 0) {
                    $(res).each(function () {
                        Tabla += "<tr>" +
                                    "<td>" +
                                        this.sucursal.nombre +
                                    "</td>" +
                                    "<td>" +
                                        this.serie +
                                    "</td>" +
                                    "<td>" +
                                        "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarTicketEquipo(" + this.id + ")'>" +
                                            "<i class='icofont icofont-trash'></i>" +
                                        "</button>" +
                                    "</td>" +
                                "</tr>";
                    });
                }

                $("#TabTicketEquipos").html(TableHeader + Tabla + TableFooter);

                CargaEquipos();
        });
    }
}
function EliminarTicketEquipo(Id) {

    const _model = {
        Id: Id,
    };

    fetch("/Ticket/TicketEquipo/DeleteListTicketEquipo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_model)
    })
    .then(res => res.json())
    .then(res => {
                
        var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                            "<table class='table text-center table-striped'>" +
                                "<thead class='thead-dark'>" +
                                    "<tr>" +
                                        "<tr>" +
                                            "<th class='text-center'>Sucursal</th>" +
                                            "<th class='text-center'>Equipo</th>" +
                                            "<th class='text-center'></th>" +
                                        "</tr>" +
                                "</thead>" +
                                "<tbody>";

                
        var TableFooter =       "</tbody>" +
                            "</table>" +
                            "</div>";

        var Tabla = "";
        if (res === null) {
            Tabla =  "<tr>" +
                        "<td>"  +
                        "</td>" +
                        "<td>"  +
                        "</td>" +
                        "<td>"  +
                        "</td>" +
                    "</tr>";
        } else if (res.length > 0) {
            $(res).each(function () {
                Tabla += "<tr>" +
                            "<td>" +
                                this.sucursal.nombre +
                            "</td>" +
                            "<td>" +
                                this.serie +
                            "</td>" +
                            "<td>" +
                                "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarTicketEquipo(" + this.id + ")'>" +
                                    "<i class='icofont icofont-trash'></i>" +
                                "</button>" +
                            "</td>" +
                        "</tr>";
            });
        }

        $("#TabTicketEquipos").html(TableHeader + Tabla + TableFooter);

    });
}
function ValidarTicketEquipo() {
    var result = false;

    $('#select2-Ticket_Equipo-container').css("border", "1px solid red");

    if ($("#Ticket_Equipo option:selected").val() > 0) {
        $('#select2-Ticket_Equipo-container').css("border", "0px solid #d9dee3");
    }

    if ($("#Ticket_Equipo option:selected").val() > 0) {
        result = true;
    }

    return result;
}

///////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////// NUEVA SUCURSAL ///////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////

$(document).ready(function () {
    $("#NuevaSucursal").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateFormSucural()) {

            var NewIdSucursal = 0;
            const Cat_Colonia = {
                Id: $("#NSucursal_Colonia option:selected").val(),
            }

            const Cat_TipoSucursal = {
                Id: $("#NSucursal_TipoSucursal option:selected").val(),
            }
            const Empresa = {
                Id: 0,
            }

            const _model = {
                Empresa: Empresa,
                Cat_Colonia: Cat_Colonia,
                Cat_TipoSucursal: Cat_TipoSucursal,
                Nombre: $('#NSucursal_NombreSucursal').val(),
                Direccion: $('#NSucursal_Direccion').val(),
            };

            console.log(_model);

            fetch("/Empresa/Sucursal/CreateSucursal", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
            .then(res => res.json())
            .then(res => {
                    if (res.id == -1) {
                        swal({
                            title: "Sucursal no registrada",
                            text: "La Sucursal que intentas agregar ya se encuentra registrada. !",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id == 0) {
                        swal({
                            title: "Sucursal no registrada",
                            text: "No es posible registrar la Sucursal. !",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id > 0) {
                        NewIdSucursal = res.id;
                        swal({
                            title: "Nueva Sucursal registrada.",
                            text: $('#NSucursal_NombreSucursal').val(),
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                            showCancelButton: false,
                            confirmButtonColor: "#3070A9",
                            confirmButtonText: "Aceptar",
                            closeOnConfirm: false,
                        },
                            function (isConfirm) {
                                if (isConfirm) {
                                    swal.close();

                                    fetch("/Empresa/Sucursal/GetSucursal_Seleccionar", {
                                        method: "POST",
                                        headers: { "Content-Type": "application/json; charset=utf-8" },
                                    })
                                    .then(res => res.json())
                                    .then(res => {
                                        if (res.length > 0) {

                                            $("#Ticket_Sucursal").empty();

                                            var option = $(document.createElement('option'));
                                            option.text("SELECCIONAR");
                                            option.val(0);

                                            $("#Ticket_Sucursal").append(option);

                                            $(res).each(function () {
                                                var option = $(document.createElement('option'));

                                                option.text(this.nombre.toUpperCase());
                                                option.val(this.id);

                                                $("#Ticket_Sucursal").append(option);
                                            });

                                            $('#Ticket_Sucursal option[value="' + NewIdSucursal + '"]').attr("selected", true);

                                            $('#NSucursal_NombreSucursal').val("");
                                            $('#NSucursal_Direccion').val("");

                                            $("#NSucursal_Poblacion").empty();
                                            $("#NSucursal_CP").empty();
                                            $("#NSucursal_Colonia").empty();

                                            $("#Ticket_Equipo").empty();

                                            var option = $(document.createElement('option'));
                                            option.text("SELECCIONAR");
                                            option.val(0);

                                            var option2 = $(document.createElement('option'));
                                            option2.text("SELECCIONAR");
                                            option2.val(0);

                                            var option3 = $(document.createElement('option'));
                                            option3.text("SELECCIONAR");
                                            option3.val(0);

                                            var option4 = $(document.createElement('option'));
                                            option4.text("SELECCIONAR");
                                            option4.val(0);


                                            $("#NSucursal_Poblacion").append(option);
                                            $("#NSucursal_CP").append(option2);
                                            $("#NSucursal_Colonia").append(option3);
                                            $("#Ticket_Equipo").append(option4);

                                            $("#TabTicketEquipos").html("");

                                            $('#FormNuevaSucursal').fadeOut(400);
                                        }
                                    });

                                }
                            }
                        );
                    }
                });
        }
    });
});
function ValidateFormSucural() {

    var result = false;
    var posicion = "NSucursal_Direccion";

    $('#select2-NSucursal_Estado-container').css("border", "1px solid red");
    $('#select2-NSucursal_Poblacion-container').css("border", "1px solid red");
    $('#select2-NSucursal_CP-container').css("border", "1px solid red");
    $('#select2-NSucursal_Colonia-container').css("border", "1px solid red");
    $('#select2-NSucursal_TipoSucursal-container').css("border", "1px solid red");
    $('#NSucursal_NombreSucursal').css("border", "1px solid red");
    $('#NSucursal_Direccion').css("border", "1px solid red");

    if ($("#NSucursal_Estado option:selected").val() > 0) {
        $('#select2-NSucursal_Estado-container').css("border", "0px solid #d9dee3");
    }

    if ($("#NSucursal_Poblacion option:selected").val() > 0) {
        $('#select2-NSucursal_Poblacion-container').css("border", "0px solid #d9dee3");
    }

    if ($("#NSucursal_CP option:selected").val() > 0) {
        $('#select2-NSucursal_CP-container').css("border", "0px solid #d9dee3");
    }

    if ($("#NSucursal_Colonia option:selected").val() > 0) {
        $('#select2-NSucursal_Colonia-container').css("border", "0px solid #d9dee3");
    }

    if ($("#NSucursal_TipoSucursal option:selected").val() > 0) {
        $('#select2-NSucursal_TipoSucursal-container').css("border", "0px solid #d9dee3");
    }

    if ($('#NSucursal_NombreSucursal').val().length > 0) {
        $('#NSucursal_NombreSucursal').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#NSucursal_Direccion').val().length > 0) {
        $('#NSucursal_Direccion').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($("#NSucursal_Estado option:selected").val() > 0) {
        if ($("#NSucursal_Poblacion option:selected").val() > 0) {
            if ($("#NSucursal_CP option:selected").val() > 0) {
                if ($("#NSucursal_Colonia option:selected").val() > 0) {
                    if ($("#NSucursal_TipoSucursal option:selected").val() > 0) {
                        if ($('#NSucursal_NombreSucursal').val().length > 0) {
                            if ($('#NSucursal_Direccion').val().length > 0) {
                                result = true;
                            } else {
                                posicion = $("#NSucursal_Direccion").offset().top;
                            }
                        } else {
                            posicion = $("#NSucursal_NombreSucursal").offset().top;
                        }
                    } else {
                        posicion = $("#NSucursal_TipoSucursal").offset().top;
                    }
                } else {
                    posicion = $("#NSucursal_Colonia").offset().top;
                }
            } else {
                posicion = $("#NSucursal_CP").offset().top;
            }
        } else {
            posicion = $("#NSucursal_Poblacion").offset().top;
        }
    } else {
        posicion = $("#NSucursal_Estado").offset().top;
    }

    $("html, body").animate({
        scrollTop: posicion - 100
    }, 500);

    return result;
}
function CargaTipoServicioTitulo() {
    var Id = $("#Ticket_TipoServicioCorrectivo option:selected").val();

    const _modelo = {
        Id: Id,
    };

    fetch("/Ticket/CatTipoServicio/Cat_TipoServicio_Seleccionar_Id", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
        .then(res => res.json())
        .then(res => {
            if (res === null) {
                $('#Ticket_TipoServicioCantidad_Label').text('* Unidad medida servicio correctivo');  
            } else {
                $('#Ticket_TipoServicioCantidad_Label').text("* " + res.tituloMedida);  
            }
        });
}
function CargaTipoServicio() {
    var IdFlujo = $("#Ticket_TipoServicio option:selected").val();

    const _modelo = {
        Id: IdFlujo,
    };

    fetch("/Ticket/CatTipoServicio/Cat_TipoServicio_Seleccionar_IdFlujo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        if (res === null) {
            $("#DivTicket_TipoServicioCorrectivo").fadeOut(400);
            $("#DivTicket_TipoServicioCantidad").fadeOut(400);
        } else {
            if (res.length > 0) {
                $('#DivTicket_TipoServicioCorrectivo').show();
                $('#DivTicket_TipoServicioCantidad').show();

                $("#Ticket_TipoServicioCorrectivo").empty();

                var option = $(document.createElement('option'));
                option.text("SELECCIONAR");
                option.val(0);

                $("#Ticket_TipoServicioCorrectivo").append(option);

                $(res).each(function () {
                    var option = $(document.createElement('option'));

                    option.text(this.nombre.toUpperCase());
                    option.val(this.id);

                    $("#Ticket_TipoServicioCorrectivo").append(option);
                });
            }
        }
    });
}
function CargaEquipos() {
    var IdSucursal = $("#Ticket_Sucursal option:selected").val();

    const _modelo = {
        Id: IdSucursal,
    };

    fetch("/Inventario/Equipo/Equipo_Seleccionar_IdSucursal", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {

        $("#Ticket_Equipo").empty();

        var option = $(document.createElement('option'));
        option.text("SELECCIONAR");
        option.val(0);

        $("#Ticket_Equipo").append(option);

        if (res.length > 0) {
            $(res).each(function () {
                var option = $(document.createElement('option'));

                option.text(this.serie + " - " + this.cat_Modelo.nombre.toUpperCase());
                option.val(this.id);

                $("#Ticket_Equipo").append(option);
            });
        }

    });
}
function MostrarFormularioNuevaSucursal() {
    $('#FormNuevaSucursal').show();
    $("#FormNuevoEquipo").fadeOut(400);  
    var posicion = $("#FormNuevaSucursal").offset().top;
    $("html, body").animate({
        scrollTop: posicion - 100
    }, 500);
}
function CancelarNuevaSucursal() {
    $("#FormNuevaSucursal").fadeOut(400);
}

//////////////////////////////////
///  CARGA DE DIRECCIONES 
//////////////////////////////////
function CargaPoblacion() {
    var IdEstdo = $("#NSucursal_Estado option:selected").val();

    const _modelo = {
        Id: IdEstdo,
    };

    fetch("/Empresa/CatPoblacion/Cat_Poblacion_Seleccionar_IdEstado", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        $("#NSucursal_Poblacion").empty();
        $("#NSucursal_CP").empty();
        $("#NSucursal_Colonia").empty();

        var option = $(document.createElement('option'));
        option.text("SELECCIONAR");
        option.val(0);

        var option2 = $(document.createElement('option'));
        option2.text("SELECCIONAR");
        option2.val(0);

        var option3 = $(document.createElement('option'));
        option3.text("SELECCIONAR");
        option3.val(0);

        $("#NSucursal_Poblacion").append(option);
        $("#NSucursal_CP").append(option2);
        $("#NSucursal_Colonia").append(option3);

        if (res === null) {

        }else if (res.length > 0) {
            
            $(res).each(function () {
                var option = $(document.createElement('option'));

                option.text(this.nombre.toUpperCase());
                option.val(this.id);

                $("#NSucursal_Poblacion").append(option);
            });
        }

    });
}
function CargaCP() {
    var IdPoblacion = $("#NSucursal_Poblacion option:selected").val();

    const _modelo = {
        Id: IdPoblacion,
    };

    fetch("/Empresa/CatCP/Cat_CP_Seleccionar_IdPoblacion", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        $("#NSucursal_CP").empty();
        $("#NSucursal_Colonia").empty();

        var option = $(document.createElement('option'));
        option.text("SELECCIONAR");
        option.val(0);

        var option2 = $(document.createElement('option'));
        option2.text("SELECCIONAR");
        option2.val(0);


        $("#NSucursal_CP").append(option);
        $("#NSucursal_Colonia").append(option2);

        if (res === null) {

        }else if (res.length > 0) {
            $(res).each(function () {
                var option = $(document.createElement('option'));

                option.text(this.nombre.toUpperCase());
                option.val(this.id);

                $("#NSucursal_CP").append(option);
            });
        }

    });
}
function CargaColonia() {
    var IdCP= $("#NSucursal_CP option:selected").val();

    const _modelo = {
        Id: IdCP,
    };

    fetch("/Empresa/CatColonia/Cat_Colonia_Seleccionar_IdCP", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {

        $("#NSucursal_Colonia").empty();

        var option = $(document.createElement('option'));
        option.text("SELECCIONAR");
        option.val(0);

        $("#NSucursal_Colonia").append(option);

        if (res === null) {

        } else  if (res.length > 0) {

            $(res).each(function () {
                var option = $(document.createElement('option'));

                option.text(this.nombre.toUpperCase());
                option.val(this.id);

                $("#NSucursal_Colonia").append(option);
            });
        }

    });
}

/////////////////////////////////
/// NUEVO ESTADO
/////////////////////////////////

$(document).ready(function () {
    $("#NuevoEstado").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateFormNuevoEstado()) {

            var Estado = $("#Modal_NEstado").val();
            var NewIdEstado = 0;
            const _model = {
                Nombre: Estado,
            };

            fetch("/Empresa/CatEstado/CreateCat_Estado", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
            .then(res => res.json())
            .then(res => {
                if (res.id == -1) {
                    swal({
                        title: "Estado no registrado",
                        text: "El estado que intentas agregar ya se encuentra registrado. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id == 0) {
                    swal({
                        title: "Estado no registrado",
                        text: "No es posible registrar el estado. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id > 0) {
                    NewIdEstado = res.id;
                    swal({
                        title: "Nuevo estado registrado.",
                        text: Estado,
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                        showCancelButton: false,
                        confirmButtonColor: "#3070A9",
                        confirmButtonText: "Aceptar",
                        closeOnConfirm: false,
                     },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();
                                $('#ModalEstado').modal('hide');


                                fetch("/Empresa/CatEstado/Cat_Estado_Seleccionar", {
                                    method: "POST",
                                    headers: { "Content-Type": "application/json; charset=utf-8" },
                                })
                                .then(res => res.json())
                                .then(res => {
                                    if (res.length > 0) {

                                        $("#NSucursal_Estado").empty();

                                        var option = $(document.createElement('option'));
                                        option.text("SELECCIONAR");
                                        option.val(0);

                                        $("#NSucursal_Estado").append(option);

                                        $(res).each(function () {
                                            var option = $(document.createElement('option'));

                                            option.text(this.nombre.toUpperCase());
                                            option.val(this.id);

                                            $("#NSucursal_Estado").append(option);
                                        });

                                        $('#NSucursal_Estado option[value="' + NewIdEstado + '"]').attr("selected", true);


                                        $("#NSucursal_Poblacion").empty();
                                        $("#NSucursal_CP").empty();
                                        $("#NSucursal_Colonia").empty();

                                        var option = $(document.createElement('option'));
                                        option.text("SELECCIONAR");
                                        option.val(0);

                                        var option2 = $(document.createElement('option'));
                                        option2.text("SELECCIONAR");
                                        option2.val(0);

                                        var option3 = $(document.createElement('option'));
                                        option3.text("SELECCIONAR");
                                        option3.val(0);

                                        $("#NSucursal_Poblacion").append(option);
                                        $("#NSucursal_CP").append(option2);
                                        $("#NSucursal_Colonia").append(option3);

                                        $('#Modal_NEstado').val("");
                                    }
                                });
                            }
                        }
                    );
                }
            });
        }
    });
});
function ValidateFormNuevoEstado() {
    var result = false;

    $('#Modal_NEstado').css("border", "1px solid red");

    if ($('#Modal_NEstado').val().length > 0) {
        $('#Modal_NEstado').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_NEstado').val().length > 0) {
        result = true;
    }

    return result;
}

/////////////////////////////////
/// NUEVA POBLACION
/////////////////////////////////

function MostrarModalPoblacion() {

    $('#select2-NSucursal_Estado-container').css("border", "1px solid red");

    if ($("#NSucursal_Estado option:selected").val() > 0) {
        $('#select2-NSucursal_Estado-container').css("border", "0px solid #d9dee3");

        $("#ModalPoblacion").modal("show");
    }

}

$(document).ready(function () {
    $("#NuevaPoblacion").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateFormNuevaPoblacion()) {
            
            var NewIdPoblacion = 0;
            var Poblacion = $("#Modal_NPoblacion").val();
            var IdEstado = $("#NSucursal_Estado option:selected").val();

            const Cat_Estado = {
                Id : IdEstado,
            };

            const _model = {
                Cat_Estado: Cat_Estado,
                Nombre: Poblacion,
            };

            fetch("/Empresa/CatPoblacion/CreateCat_Poblacion", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
            .then(res => res.json())
            .then(res => {
                if (res.id == -1) {
                    swal({
                        title: "Poblacion no registrada",
                        text: "La poblacion que intentas agregar ya se encuentra registrada. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id == 0) {
                    swal({
                        title: "Poblacion no registrada",
                        text: "No es posible registrar la poblacion. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id > 0) {
                    NewIdPoblacion = res.id;
                    swal({
                        title: "Nueva poblacion registrada.",
                        text: Poblacion,
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                        showCancelButton: false,
                        confirmButtonColor: "#3070A9",
                        confirmButtonText: "Aceptar",
                        closeOnConfirm: false,
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();
                                $('#ModalPoblacion').modal('hide');


                                var IdEstdo = $("#NSucursal_Estado option:selected").val();

                                const _modelo = {
                                    Id: IdEstdo,
                                };

                                fetch("/Empresa/CatPoblacion/Cat_Poblacion_Seleccionar_IdEstado", {
                                    method: "POST",
                                    headers: { "Content-Type": "application/json; charset=utf-8" },
                                    body: JSON.stringify(_modelo)
                                })
                                .then(res => res.json())
                                .then(res => {

                                    if (res.length > 0) {

                                        $("#NSucursal_Poblacion").empty();

                                        var option = $(document.createElement('option'));
                                        option.text("SELECCIONAR");
                                        option.val(0);

                                        $("#NSucursal_Poblacion").append(option);

                                        $(res).each(function () {
                                            var option = $(document.createElement('option'));

                                            option.text(this.nombre.toUpperCase());
                                            option.val(this.id);

                                            $("#NSucursal_Poblacion").append(option);
                                        });

                                        $('#NSucursal_Poblacion option[value="' + NewIdPoblacion + '"]').attr("selected", true);


                                        $("#NSucursal_CP").empty();
                                        $("#NSucursal_Colonia").empty();

                                        var option2 = $(document.createElement('option'));
                                        option2.text("SELECCIONAR");
                                        option2.val(0);

                                        var option3 = $(document.createElement('option'));
                                        option3.text("SELECCIONAR");
                                        option3.val(0);

                                        $("#NSucursal_CP").append(option2);
                                        $("#NSucursal_Colonia").append(option3);

                                        $('#Modal_NPoblacion').val("");
                                    }

                                });

                            }
                        }
                    );
                }
            });
        }
    });
});

function ValidateFormNuevaPoblacion() {
    var result = false;

    $('#Modal_NPoblacion').css("border", "1px solid red");

    if ($('#Modal_NPoblacion').val().length > 0) {
        $('#Modal_NPoblacion').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_NPoblacion').val().length > 0) {
        result = true;
    }

    return result;
}

/////////////////////////////////
/// NUEVO CODIGO POSTAL
/////////////////////////////////

$(document).ready(function () {
    $("#NuevoCP").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateFormNuevoCP()) {

            var NewIdCP = 0;
            var CP = $("#Modal_NCP").val();
            var IdPoblacion = $("#NSucursal_Poblacion option:selected").val();

            const Cat_Poblacion = {
                Id: IdPoblacion,
            };

            const _model = {
                Cat_Poblacion: Cat_Poblacion,
                Nombre: CP,
            };

            fetch("/Empresa/CatCP/CreateCat_CP", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
            .then(res => res.json())
            .then(res => {
                if (res.id == -1) {
                    swal({
                        title: "Codigo postal no registrado",
                        text: "El codigo postal que intentas agregar ya se encuentra registrado. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id == 0) {
                    swal({
                        title: "Codigo postal no registrado",
                        text: "No es posible registrar el codigo postal. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id > 0) {
                    NewIdCP = res.id;
                    swal({
                        title: "Nuevo codigo postal registrado.",
                        text: CP,
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                        showCancelButton: false,
                        confirmButtonColor: "#3070A9",
                        confirmButtonText: "Aceptar",
                        closeOnConfirm: false,
                    },
                        function (isConfirm) {
                            if (isConfirm) {

                                swal.close();
                                $('#ModalCP').modal('hide');

                                var IdPoblacion = $("#NSucursal_Poblacion option:selected").val();

                                const _modelo = {
                                    Id: IdPoblacion,
                                };

                                fetch("/Empresa/CatCP/Cat_CP_Seleccionar_IdPoblacion", {
                                    method: "POST",
                                    headers: { "Content-Type": "application/json; charset=utf-8" },
                                    body: JSON.stringify(_modelo)
                                })
                                    .then(res => res.json())
                                    .then(res => {

                                        if (res.length > 0) {

                                            $("#NSucursal_CP").empty();

                                            var option = $(document.createElement('option'));
                                            option.text("SELECCIONAR");
                                            option.val(0);

                                            $("#NSucursal_CP").append(option);

                                            $(res).each(function () {
                                                var option = $(document.createElement('option'));

                                                option.text(this.nombre.toUpperCase());
                                                option.val(this.id);

                                                $("#NSucursal_CP").append(option);
                                            });

                                            $('#NSucursal_CP option[value="' + NewIdCP + '"]').attr("selected", true);

                                            $("#NSucursal_Colonia").empty();

                                            var option3 = $(document.createElement('option'));
                                            option3.text("SELECCIONAR");
                                            option3.val(0);

                                            $("#NSucursal_Colonia").append(option3);

                                            $('#Modal_NCP').val("");
                                        }

                                    });
                                    

                            }
                        }
                    );
                }
            });
        }
    });
});

function MostrarModalCP() {

    $('#select2-NSucursal_Poblacion-container').css("border", "1px solid red");

    if ($("#NSucursal_Poblacion option:selected").val() > 0) {
        $('#select2-NSucursal_Poblacion-container').css("border", "0px solid #d9dee3");

        $("#ModalCP").modal("show");
    }

}

function ValidateFormNuevoCP() {
    var result = false;

    $('#Modal_NCP').css("border", "1px solid red");

    if ($('#Modal_NCP').val().length > 0) {
        $('#Modal_NCP').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_NCP').val().length > 0) {
        result = true;
    }

    return result;
}


/////////////////////////////////
/// NUEVA COLONIA
/////////////////////////////////

$(document).ready(function () {
    $("#NuevaColonia").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateFormNuevaColonia()) {
            var NewIdColonia = 0;
            var Colonia = $("#Modal_NColonia").val();
            var IdCP = $("#NSucursal_CP option:selected").val();

            const Cat_CP = {
                Id: IdCP,
            };

            const _model = {
                Cat_CP: Cat_CP,
                Nombre: Colonia,
            };

            fetch("/Empresa/CatColonia/CreateCat_Colonia", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
            .then(res => res.json())
            .then(res => {
                if (res.id == -1) {
                    swal({
                        title: "Colonia no registrada",
                        text: "La colonia que intentas agregar ya se encuentra registrada. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id == 0) {
                    swal({
                        title: "Colonia no registrada",
                        text: "No es posible registrar la colonia. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id > 0) {
                    NewIdColonia = res.id;
                    swal({
                        title: "Nueva colonia registrada.",
                        text: Colonia,
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                        showCancelButton: false,
                        confirmButtonColor: "#3070A9",
                        confirmButtonText: "Aceptar",
                        closeOnConfirm: false,
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();
                                $('#ModalColonia').modal('hide');

                                var IdCP = $("#NSucursal_CP option:selected").val();

                                const _modelo = {
                                    Id: IdCP,
                                };

                                fetch("/Empresa/CatColonia/Cat_Colonia_Seleccionar_IdCP", {
                                    method: "POST",
                                    headers: { "Content-Type": "application/json; charset=utf-8" },
                                    body: JSON.stringify(_modelo)
                                })
                                .then(res => res.json())
                                .then(res => {
                                    if (res.length > 0) {
                                        $("#NSucursal_Colonia").empty();

                                        var option = $(document.createElement('option'));
                                        option.text("SELECCIONAR");
                                        option.val(0);

                                        $("#NSucursal_Colonia").append(option);

                                        $(res).each(function () {
                                            var option = $(document.createElement('option'));

                                            option.text(this.nombre.toUpperCase());
                                            option.val(this.id);

                                            $("#NSucursal_Colonia").append(option);
                                        });

                                        $('#NSucursal_Colonia option[value="' + NewIdColonia + '"]').attr("selected", true);
                                        $('#Modal_NColonia').val("");
                                    }
                                });
                            }
                        }
                    );
                }
            });
        }
    });
});

function MostrarModalColonia() {

    $('#select2-NSucursal_CP-container').css("border", "1px solid red");

    if ($("#NSucursal_CP option:selected").val() > 0) {
        $('#select2-NSucursal_CP-container').css("border", "0px solid #d9dee3");

        $("#ModalColonia").modal("show");
    }
}

function ValidateFormNuevaColonia() {
    var result = false;

    $('#Modal_NColonia').css("border", "1px solid red");

    if ($('#Modal_NColonia').val().length > 0) {
        $('#Modal_NColonia').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_NColonia').val().length > 0) {
        result = true;
    }

    return result;
}

///////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////// NUEVO EQUIPO /////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////

$(document).ready(function () {
    $("#NuevoEquipo").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();


        if (ValidateFormNuevoEquipo()) {
            var NewIdEquipo = 0;

            const Sucursal = {
                Id: $("#Ticket_Sucursal option:selected").val(),
            }

            const Cat_Modelo = {
                Id: $("#NEquipo_Modelo option:selected").val(),
            }

            const Cat_EstatusEquipo = {
                Id: $("#NEquipo_EstatusEquipo option:selected").val(),
            }

            const _model = {
                Sucursal: Sucursal,
                Cat_Modelo: Cat_Modelo,
                Cat_EstatusEquipo: Cat_EstatusEquipo,
                Serie: $('#NEquipo_NumeroSerie').val(),
                FechaApertura: $('#NEquipo_FechaApartura').val(),
                FechaAdquisicion: $('#NEquipo_FechaAdquisicion').val(),
                FechaCaducidad: $('#NEquipo_FechaCaducidad').val(),
                FechaGarantia: $('#NEquipo_FechaGarantia').val(),
                Observaciones: $('#NEquipo_Observaciones').val(),
            };

            fetch("/Inventario/Equipo/CreateEquipo", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
            .then(res => res.json())
            .then(res => {
                if (res.id == -3) {
                    swal({
                        title: "Equipo no registrado",
                        text: " Debe de agregar un regla de rutina  !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else  if (res.id == -2) {
                    swal({
                        title: "Equipo no registrado",
                        text: "Debes de agregar un nombre de imagen antes vs despues  !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id == -1) {
                    swal({
                        title: "Equipo no registrado",
                        text: "El equipo que intentas agregar ya se encuentra registrado. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id == 0) {
                    swal({
                        title: "Equipo no registrado",
                        text: "No es posible registrar el equipo. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id > 0) {
                    NewIdEquipo = res.id;
                    swal({
                        title: "Nuevo equipo registrado.",
                        text: $('#NEquipo_NumeroSerie').val(),
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                        showCancelButton: false,
                        confirmButtonColor: "#3070A9",
                        confirmButtonText: "Aceptar",
                        closeOnConfirm: false,
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();

                                fetch("/Inventario/Equipo/Equipo_Seleccionar_IdSucursal", {
                                    method: "POST",
                                    headers: { "Content-Type": "application/json; charset=utf-8" },
                                    body: JSON.stringify(Sucursal)
                                })
                                .then(res => res.json())
                                .then(res => {
                                    if (res.length > 0) {

                                        $("#Ticket_Equipo").empty();

                                        var option = $(document.createElement('option'));
                                        option.text("SELECCIONAR");
                                        option.val(0);

                                        $("#Ticket_Equipo").append(option);

                                        $(res).each(function () {
                                            var option = $(document.createElement('option'));

                                            option.text(this.serie + " - " + this.cat_Modelo.nombre.toUpperCase());
                                            option.val(this.id);

                                            $("#Ticket_Equipo").append(option);
                                        });
                                        $('#Ticket_Equipo option[value="' + NewIdEquipo + '"]').attr("selected", true);

                                        // Linpiar tipo de equipo
                                        $("#NEquipo_TipoEquipo").empty();
                                        fetch("/Inventario/CatTipoEquipo/GetCat_TipoEquipo_Seleccionar", {
                                            method: "POST",
                                            headers: { "Content-Type": "application/json; charset=utf-8" },
                                        })
                                            .then(res => res.json())
                                            .then(res => {
                                                $("#NEquipo_TipoEquipo").empty();
                                                $("#NEquipo_Categoria").empty();
                                                $("#NEquipo_Modelo").empty();

                                                var option = $(document.createElement('option'));
                                                option.text("SELECCIONAR");
                                                option.val(0);

                                                var option2 = $(document.createElement('option'));
                                                option2.text("SELECCIONAR");
                                                option2.val(0);

                                                var option3 = $(document.createElement('option'));
                                                option3.text("SELECCIONAR");
                                                option3.val(0);

                                                $("#NEquipo_TipoEquipo").append(option);
                                                $("#NEquipo_Categoria").append(option2);
                                                $("#NEquipo_Modelo").append(option3);


                                                $('#NEquipo_NumeroSerie').val("");
                                                $('#NEquipo_FechaApartura').val("");
                                                $('#NEquipo_FechaAdquisicion').val("");
                                                $('#NEquipo_FechaCaducidad').val("");
                                                $('#NEquipo_FechaGarantia').val("");
                                                $('#NEquipo_Observaciones').val("");

                                                $("#NEquipo_Categoria").empty();
                                                $("#NEquipo_Modelo").empty();

                                                $('#FormNuevoEquipo').fadeOut(100);
                                                $("#NumRutina").html("0");
                                                $("#NumImagenes").html("0");

                                                $("#TabImagenEquipo").html("");
                                                $("#TabRutinaEquipo").html("");



                                                if (res === null) {

                                                } else if (res.length > 0) {

                                                    $(res).each(function () {
                                                        var option = $(document.createElement('option'));

                                                        option.text(this.nombre.toUpperCase());
                                                        option.val(this.id);

                                                        $("#NEquipo_TipoEquipo").append(option);
                                                    });

                                                    $('#NEquipo_TipoEquipo option[value="0"]').attr("selected", true);
                                               
                                                }

                                            });

                                        $("html, body").animate({
                                            scrollTop: $("#Ticket_Equipo").offset().top - 100
                                        }, 500);

                                    }
                                });

                            }
                        }
                    );
                }
            });
        }
    });
});
function MostrarFormularioNuevoEquipo() {
    $('#select2-Ticket_Sucursal-container').css("border", "1px solid red");

    if ($("#Ticket_Sucursal option:selected").val() > 0) {
        $('#select2-Ticket_Sucursal-container').css("border", "0px solid #d9dee3");

        $('#FormNuevoEquipo').show();
        $("#FormNuevaSucursal").fadeOut(400);
        var posicion = $("#FormNuevoEquipo").offset().top;
        $("html, body").animate({
            scrollTop: posicion - 100
        }, 500);
    }
}
function CancelarNuevoEquipo() {
    $('#FormNuevoEquipo').fadeOut(400);
}
function CargaCategorias() {
   
    var IdTipoEquipo = $("#NEquipo_TipoEquipo option:selected").val();

    const _modelo = {
        Id: IdTipoEquipo,
    };

    fetch("/Inventario/CatCategoria/GetCat_Categoria_Seleccionar_IdTipoEquipo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        $("#NEquipo_Categoria").empty();
        $("#NEquipo_Modelo").empty();

        var option = $(document.createElement('option'));
        option.text("SELECCIONAR");
        option.val(0);

        var option2 = $(document.createElement('option'));
        option2.text("SELECCIONAR");
        option2.val(0);


        $("#NEquipo_Categoria").append(option);
        $("#NEquipo_Modelo").append(option2);

        if (res === null) {

        } else if (res.length > 0) {

            $(res).each(function () {
                var option = $(document.createElement('option'));

                option.text(this.nombre.toUpperCase());
                option.val(this.id);

                $("#NEquipo_Categoria").append(option);
            });
        }

    });


    ConsultarRutinaEquipo();

    ConsultarImagenEquipo();
}
function CargaModelos() {
    var IdCategoria = $("#NEquipo_Categoria option:selected").val();

    const _modelo = {
        Id: IdCategoria,
    };

    fetch("/Inventario/CatModelo/GetCat_Modelo_Seleccionar_IdCategoria", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        $("#NEquipo_Modelo").empty();

        var option = $(document.createElement('option'));
        option.text("SELECCIONAR");
        option.val(0);

        $("#NEquipo_Modelo").append(option);

        if (res === null) {

        } else if (res.length > 0) {

            $(res).each(function () {
                var option = $(document.createElement('option'));

                option.text(this.nombre.toUpperCase());
                option.val(this.id);

                $("#NEquipo_Modelo").append(option);
            });
        }

    });
}
function ValidateFormNuevoEquipo() {

    var result = false;
    var posicion = "NEquipo_TipoEquipo";

    $('#select2-NEquipo_TipoEquipo-container').css("border", "1px solid red");
    $('#select2-NEquipo_Categoria-container').css("border", "1px solid red");
    $('#select2-NEquipo_Modelo-container').css("border", "1px solid red");
    $('#select2-NEquipo_EstatusEquipo-container').css("border", "1px solid red");

    $('#NEquipo_NumeroSerie').css("border", "1px solid red");
    $('#NEquipo_FechaApartura').css("border", "1px solid red");
    $('#NEquipo_FechaAdquisicion').css("border", "1px solid red");
    $('#NEquipo_FechaCaducidad').css("border", "1px solid red");
    $('#NEquipo_FechaGarantia').css("border", "1px solid red");

    if ($("#NEquipo_TipoEquipo option:selected").val() > 0) {
        $('#select2-NEquipo_TipoEquipo-container').css("border", "0px solid #d9dee3");
    }
    if ($("#NEquipo_Categoria option:selected").val() > 0) {
        $('#select2-NEquipo_Categoria-container').css("border", "0px solid #d9dee3");
    }
    if ($("#NEquipo_Modelo option:selected").val() > 0) {
        $('#select2-NEquipo_Modelo-container').css("border", "0px solid #d9dee3");
    }
    if ($("#NEquipo_EstatusEquipo option:selected").val() > 0) {
        $('#select2-NEquipo_EstatusEquipo-container').css("border", "0px solid #d9dee3");
    }

    if ($('#NEquipo_NumeroSerie').val().length > 0) {
        $('#NEquipo_NumeroSerie').css("border", "1px solid rgba(0, 0, 0, .15)");
    }
    if ($('#NEquipo_FechaApartura').val().length > 0) {
        $('#NEquipo_FechaApartura').css("border", "1px solid rgba(0, 0, 0, .15)");
    }
    if ($('#NEquipo_FechaAdquisicion').val().length > 0) {
        $('#NEquipo_FechaAdquisicion').css("border", "1px solid rgba(0, 0, 0, .15)");
    }
    if ($('#NEquipo_FechaCaducidad').val().length > 0) {
        $('#NEquipo_FechaCaducidad').css("border", "1px solid rgba(0, 0, 0, .15)");
    }
    if ($('#NEquipo_FechaGarantia').val().length > 0) {
        $('#NEquipo_FechaGarantia').css("border", "1px solid rgba(0, 0, 0, .15)");
    }


    if ($("#NEquipo_TipoEquipo option:selected").val() > 0) {
        if ($("#NEquipo_Categoria option:selected").val() > 0) {
            if ($("#NEquipo_Modelo option:selected").val() > 0) {
                if ($("#NEquipo_EstatusEquipo option:selected").val() > 0) {
                    if ($('#NEquipo_NumeroSerie').val().length > 0) {
                        if ($('#NEquipo_FechaApartura').val().length > 0) {
                            if ($('#NEquipo_FechaAdquisicion').val().length > 0) {
                                if ($('#NEquipo_FechaCaducidad').val().length > 0) {
                                    if ($('#NEquipo_FechaGarantia').val().length > 0) {
                                        result = true;
                                    } else {
                                        posicion = $("#NEquipo_FechaGarantia").offset().top;
                                    }
                                } else {
                                    posicion = $("#NEquipo_FechaCaducidad").offset().top;
                                }
                            } else {
                                posicion = $("#NEquipo_FechaAdquisicion").offset().top;
                            }
                        } else {
                            posicion = $("#NEquipo_FechaApartura").offset().top;
                        }
                    } else {
                        posicion = $("#NEquipo_NumeroSerie").offset().top;
                    }
                } else {
                    posicion = $("#NEquipo_EstatusEquipo").offset().top;
                }
            } else {
                posicion = $("#NEquipo_Modelo").offset().top;
            }
        } else {
            posicion = $("#NEquipo_Categoria").offset().top;
        }
    } else {
        posicion = $("#NEquipo_TipoEquipo").offset().top;
    }

    if (!result) {
        $("html, body").animate({
            scrollTop: posicion - 100
        }, 500);
    }
    
    return result;
}


////////////////////////////////
////// NUEVO TIPO EQUIPO ///////
////////////////////////////////
// Eliminaor imagenes y rutina de lsita
$(document).ready(function () {
    $("#NuevoTipoEquipo").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateFormNuevoTipoEquipo()) {
            var NewIdTipoEquipo = 0;
            const _model = {
                Nombre: $("#Modal_TipoEquipo_Nombre").val(),
            };

            fetch("/Inventario/CatTipoEquipo/CreateCat_TipoEquipo", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
            .then(res => res.json())
            .then(res => {
                if (res.id == -1) {
                    swal({
                        title: "Tipo equipo no registrado",
                        text: "El tipo de equipo que intentas agregar ya se encuentra registrado. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id == 0) {
                    swal({
                        title: "Tipo equipo no registrado",
                        text: "No es posible registrar el tipo de quipo. !",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                } else if (res.id > 0) {
                    NewIdTipoEquipo = res.id;
                    swal({
                        title: "Nuevo tipo de quipo registrado.",
                        text: $("#Modal_TipoEquipo_Nombre").val(),
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                        showCancelButton: false,
                        confirmButtonColor: "#3070A9",
                        confirmButtonText: "Aceptar",
                        closeOnConfirm: false,
                    },
                    function (isConfirm) {
                        swal.close();
                        $('#ModalTipoEquipo').modal('hide');


                        fetch("/Inventario/CatTipoEquipo/GetCat_TipoEquipo_Seleccionar", {
                            method: "POST",
                            headers: { "Content-Type": "application/json; charset=utf-8" },
                        })
                        .then(res => res.json())
                        .then(res => {
                            $("#NEquipo_TipoEquipo").empty();
                            $("#NEquipo_Categoria").empty();
                            $("#NEquipo_Modelo").empty();

                            var option = $(document.createElement('option'));
                            option.text("SELECCIONAR");
                            option.val(0);

                            var option2 = $(document.createElement('option'));
                            option2.text("SELECCIONAR");
                            option2.val(0);

                            var option3 = $(document.createElement('option'));
                            option3.text("SELECCIONAR");
                            option3.val(0);

                            $("#NEquipo_TipoEquipo").append(option);
                            $("#NEquipo_Categoria").append(option2);
                            $("#NEquipo_Modelo").append(option3);

                            if (res === null) {

                            } else if (res.length > 0) {

                                $(res).each(function () {
                                    var option = $(document.createElement('option'));

                                    option.text(this.nombre.toUpperCase());
                                    option.val(this.id);

                                    $("#NEquipo_TipoEquipo").append(option);
                                });

                                $('#NEquipo_TipoEquipo option[value="' + NewIdTipoEquipo + '"]').attr("selected", true);
                                $('#Modal_TipoEquipo_Nombre').val("");
                            }

                        });
                    }
                    );
                }
            });
        }
    });
});
function ValidateFormNuevoTipoEquipo() {
    var result = false;

    $('#Modal_TipoEquipo_Nombre').css("border", "1px solid red");

    if ($('#Modal_TipoEquipo_Nombre').val().length > 0) {
        $('#Modal_TipoEquipo_Nombre').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_TipoEquipo_Nombre').val().length > 0) {
        result = true;
    }

    return result;
}

////////////////////////////////
//////// NUEVA CATEGORIA ///////
////////////////////////////////

$(document).ready(function () {
    $("#NuevaCategoria").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateFormNuevaCategoria()) {
            var NewIdCategoria = 0;

            const Cat_TipoEquipo = {
                Id: $("#NEquipo_TipoEquipo option:selected").val(),
            }

            const _model = {
                Cat_TipoEquipo: Cat_TipoEquipo,
                Nombre: $("#Modal_Categoria_Nombre").val(),
            };

            fetch("/Inventario/CatCategoria/CreateCat_TipoEquipo", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
                .then(res => res.json())
                .then(res => {
                    if (res.id == -1) {
                        swal({
                            title: "Categoria no registrada",
                            text: "La categoria que intentas agregar ya se encuentra registrada. !",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id == 0) {
                        swal({
                            title: "Categoria no registrada",
                            text: "No es posible registrar la categoria. !",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id > 0) {
                        NewIdCategoria = res.id;
                        swal({
                            title: "Nueva categoria registrada.",
                            text: $("#Modal_Categoria_Nombre").val(),
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                            showCancelButton: false,
                            confirmButtonColor: "#3070A9",
                            confirmButtonText: "Aceptar",
                            closeOnConfirm: false,
                        },
                            function (isConfirm) {
                                swal.close();
                                $('#ModalCategoria').modal('hide');

                                fetch("/Inventario/CatCategoria/GetCat_Categoria_Seleccionar_IdTipoEquipo", {
                                    method: "POST",
                                    headers: { "Content-Type": "application/json; charset=utf-8" },
                                    body: JSON.stringify(Cat_TipoEquipo)
                                })
                                .then(res => res.json())
                                .then(res => {
                                    $("#NEquipo_Categoria").empty();
                                    $("#NEquipo_Modelo").empty();

                                    var option = $(document.createElement('option'));
                                    option.text("SELECCIONAR");
                                    option.val(0);

                                    var option2 = $(document.createElement('option'));
                                    option2.text("SELECCIONAR");
                                    option2.val(0);

                                    $("#NEquipo_Categoria").append(option);
                                    $("#NEquipo_Modelo").append(option2);

                                    if (res === null) {

                                    } else if (res.length > 0) {

                                        $(res).each(function () {
                                            var option = $(document.createElement('option'));

                                            option.text(this.nombre.toUpperCase());
                                            option.val(this.id);

                                            $("#NEquipo_Categoria").append(option);
                                        });

                                        $('#NEquipo_Categoria option[value="' + NewIdCategoria + '"]').attr("selected", true);
                                        $('#Modal_Categoria_Nombre').val("");
                                    }

                                });
                            }
                        );
                    }
                });
        }
    });
});

function ValidateFormNuevaCategoria() {
    var result = false;

    $('#Modal_Categoria_Nombre').css("border", "1px solid red");

    if ($('#Modal_Categoria_Nombre').val().length > 0) {
        $('#Modal_Categoria_Nombre').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_Categoria_Nombre').val().length > 0) {
        result = true;
    }

    return result;
}

function MostrarModalCategoria() {

    $('#select2-NEquipo_TipoEquipo-container').css("border", "1px solid red");

    if ($("#NEquipo_TipoEquipo option:selected").val() > 0) {
        $('#select2-NEquipo_TipoEquipo-container').css("border", "0px solid #d9dee3");

        $("#ModalCategoria").modal("show");
    }
}

////////////////////////////////
//// NUEVO MODELO DE EQUIPO ////
////////////////////////////////

$(document).ready(function () {
    $("#NuevoModelo").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateFormNuevoModelo()) {
            var NewIdModelo = 0;

            const Cat_Categoria = {
                Id: $("#NEquipo_Categoria option:selected").val(),
            }

            const _model = {
                Cat_Categoria: Cat_Categoria,
                Nombre: $("#Modal_Modelo_Nombre").val(),
            };

            fetch("/Inventario/CatModelo/CreateCat_Modelo", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
                .then(res => res.json())
                .then(res => {
                    if (res.id == -1) {
                        swal({
                            title: "Modelo no registrado",
                            text: "El modelo que intentas agregar ya se encuentra registrado. !",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id == 0) {
                        swal({
                            title: "Modelo no registrado",
                            text: "No es posible registrar el modelo. !",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id > 0) {
                        NewIdModelo = res.id;
                        swal({
                            title: "Nuevo modelo registrado.",
                            text: $("#Modal_Modelo_Nombre").val(),
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                            showCancelButton: false,
                            confirmButtonColor: "#3070A9",
                            confirmButtonText: "Aceptar",
                            closeOnConfirm: false,
                        },
                            function (isConfirm) {
                                swal.close();
                                $('#ModalModelo').modal('hide');

                                fetch("/Inventario/CatModelo/GetCat_Modelo_Seleccionar_IdCategoria", {
                                    method: "POST",
                                    headers: { "Content-Type": "application/json; charset=utf-8" },
                                    body: JSON.stringify(Cat_Categoria)
                                })
                                    .then(res => res.json())
                                    .then(res => {
                                        $("#NEquipo_Modelo").empty();

                                        var option = $(document.createElement('option'));
                                        option.text("SELECCIONAR");
                                        option.val(0);

                                        $("#NEquipo_Modelo").append(option);

                                        if (res === null) {

                                        } else if (res.length > 0) {

                                            $(res).each(function () {
                                                var option = $(document.createElement('option'));

                                                option.text(this.nombre.toUpperCase());
                                                option.val(this.id);

                                                $("#NEquipo_Modelo").append(option);
                                            });

                                            $('#NEquipo_Modelo option[value="' + NewIdModelo + '"]').attr("selected", true);
                                            $('#Modal_Modelo_Nombre').val("");
                                        }

                                    });
                            }
                        );
                    }
                });
        }
    });
});

function ValidateFormNuevoModelo() {
    var result = false;

    $('#Modal_Modelo_Nombre').css("border", "1px solid red");

    if ($('#Modal_Modelo_Nombre').val().length > 0) {
        $('#Modal_Modelo_Nombre').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_Modelo_Nombre').val().length > 0) {
        result = true;
    }
    return result;
}

function MostrarModalModelo() {

    $('#select2-NEquipo_Categoria-container').css("border", "1px solid red");

    if ($("#NEquipo_Categoria option:selected").val() > 0) {
        $('#select2-NEquipo_Categoria-container').css("border", "0px solid #d9dee3");

        $("#ModalModelo").modal("show");
    }
}

///////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////// NUEVA REGLA  /////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////

$(document).ready(function () {
    $("#NuevaRutina").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateFormNuevaRutina()) {
            
            const _modelo = {
                Nombre: $('#Modal_NRutina_Nombre').val(),
                Observaciones: $('#Modal_NRutina_Observaciones').val(),
            };

            fetch("/Inventario/CatEquipoRutina/AddListEquipoRutina", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_modelo)
            })
            .then(res => res.json())
            .then(res => {
                
                var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                                    "<table class='table text-center'>" +
                                        "<thead>" +
                                            "<tr>" +
                                                "<tr>" +
                                                    "<th class='text-center'>Rutina de la solucion</th>" +
                                                    "<th class='text-center'></th>" +
                                                "</tr>" +
                                        "</thead>" +
                                        "<tbody>";

                
                var TableFooter =       "</tbody>" +
                                    "</table>" +
                                  "</div>";

                var Tabla = "";
                if (res === null) {
                    $("#NumRutina").html("0");
                    Tabla =  "<tr>" +
                                "<td>" +
                                "</td>" +
                                "<td>" +
                                "</td>" +
                            "</tr>";
                } else if (res.length > 0) {
                    $("#NumRutina").html(res.length);
                    $(res).each(function () {
                        Tabla += "<tr>" +
                                    "<td>" +
                                        this.nombre.toUpperCase() +
                                    "</td>" +
                                    "<td>" +
                                        "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarRutinaEquipol(" + this.id + ")'>" +
                                            "<i class='icofont icofont-trash'></i>" +
                                        "</button>" +
                                    "</td>" +
                                "</tr>";
                    });
                }
                
                $("#TabRutinaEquipo").html(TableHeader + Tabla + TableFooter);

                $("#Modal_NRutina_Nombre").val("");
                $("#Modal_NRutina_Observaciones").val("");

            });
        }
    });
});

function EliminarRutinaEquipol(Id) {

    const _modelo = {
        Id: Id,
    };

    fetch("/Inventario/CatEquipoRutina/DeleteListEquipoRutina", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        $("#NumRutina").html("0");
        var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                            "<table class='table text-center'>" +
                                "<thead>" +
                                    "<tr>" +
                                        "<tr>" +
                                            "<th class='text-center'>Rutina de la solucion</th>" +
                                            "<th class='text-center'></th>" +
                                        "</tr>" +
                                "</thead>" +
                                "<tbody>";

                
        var TableFooter =       "</tbody>" +
                            "</table>" +
                            "</div>";

        var Tabla = "";
        if (res === null) {
            $("#NumRutina").html("0");
            Tabla =  "<tr>" +
                        "<td>" +
                        "</td>" +
                        "<td>" +
                        "</td>" +
                    "</tr>";
        } else if (res.length > 0) {
            $(res).each(function () {
                $("#NumRutina").html(res.length);
                Tabla += "<tr>" +
                            "<td>" +
                                this.nombre.toUpperCase() +
                            "</td>" +
                            "<td>" +
                                "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarRutinaEquipol(" + this.id + ")'>" +
                                    "<i class='icofont icofont-trash'></i>" +
                                "</button>" +
                            "</td>" +
                        "</tr>";
            });
        }

        $("#TabRutinaEquipo").html(TableHeader + Tabla + TableFooter);
    });
}

function ValidateFormNuevaRutina() {
    var result = false;

    $('#Modal_NRutina_Nombre').css("border", "1px solid red");

    if ($('#Modal_NRutina_Nombre').val().length > 0) {
        $('#Modal_NRutina_Nombre').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_NRutina_Nombre').val().length > 0) {
        result = true;
    }

    return result;
}

function MostrarModalRutina() {
    $("#ModalRutinaSolucion").modal("show");
}

function ConsultarRutinaEquipo() {
    const _Cat_TipoEquipo = {
        Id: $("#NEquipo_TipoEquipo option:selected").val(),
    }

    fetch("/Inventario/CatTipoEquipoRutina/GetCat_TipoEquipoRutina_Seleccionar_IdTipoEquipo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_Cat_TipoEquipo)
    })
        .then(res => res.json())
        .then(res => {
            $("#NumRutina").html("0");
            var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                "<table class='table text-center'>" +
                "<thead>" +
                "<tr>" +
                "<tr>" +
                "<th class='text-center'>Rutina de la solucion</th>" +
                "<th class='text-center'></th>" +
                "</tr>" +
                "</thead>" +
                "<tbody>";


            var TableFooter = "</tbody>" +
                "</table>" +
                "</div>";

            var Tabla = "";
            if (res === null) {
                $("#NumRutina").html("0");
                Tabla = "<tr>" +
                    "<td>" +
                    "</td>" +
                    "<td>" +
                    "</td>" +
                    "</tr>";
            } else if (res.length > 0) {
                $(res).each(function () {
                    $("#NumRutina").html(res.length);
                    Tabla += "<tr>" +
                        "<td>" +
                        this.nombre.toUpperCase() +
                        "</td>" +
                        "<td>" +
                        "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarRutinaEquipol(" + this.id + ")'>" +
                        "<i class='icofont icofont-trash'></i>" +
                        "</button>" +
                        "</td>" +
                        "</tr>";
                });
            }

            $("#TabRutinaEquipo").html(TableHeader + Tabla + TableFooter);
        });
}

///////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////// NUEVA IMAGEN /////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////

$(document).ready(function () {
    $("#NuevaImagen").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateFormNuevaImagen()) {

            const _modelo = {
                Nombre: $('#Modal_NImagen_Nombre').val(),
                Observaciones: $('#Modal_NImagen_Observaciones').val(),
            };

            fetch("/Inventario/CatEquipoImagen/AddListEquipoImagen", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_modelo)
            })
            .then(res => res.json())
            .then(res => {

                var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                    "<table class='table text-center'>" +
                    "<thead>" +
                    "<tr>" +
                    "<tr>" +
                    "<th class='text-center'>Imágenes solicitadas para el equipo</th>" +
                    "<th class='text-center'></th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody>";


                var TableFooter = "</tbody>" +
                    "</table>" +
                    "</div>";

                var Tabla = "";
                if (res === null) {
                    $("#NumImagenes").html("0");
                    Tabla = "<tr>" +
                        "<td>" +
                        "</td>" +
                        "<td>" +
                        "</td>" +
                        "</tr>";
                } else if (res.length > 0) {
                    $("#NumImagenes").html(res.length);
                    $(res).each(function () {
                        Tabla += "<tr>" +
                            "<td>" +
                            this.nombre.toUpperCase() +
                            "</td>" +
                            "<td>" +
                            "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarImagenEquipo(" + this.id + ")'>" +
                            "<i class='icofont icofont-trash'></i>" +
                            "</button>" +
                            "</td>" +
                            "</tr>";
                    });
                }

                $("#TabImagenEquipo").html(TableHeader + Tabla + TableFooter);

                $("#Modal_NImagen_Nombre").val("");
                $("#Modal_NImagen_Observaciones").val("");

            });
        }
    });
});
function EliminarImagenEquipo(Id) {
    const _modelo = {
        Id: Id,
    };

    fetch("/Inventario/CatEquipoImagen/DeleteListEquipoImagen", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        $("#NumImagenes").html("0");
        var TableHeader = "<div class='col-sm-12 table-responsive'>" +
            "<table class='table text-center'>" +
            "<thead>" +
            "<tr>" +
            "<tr>" +
            "<th class='text-center'>Rutina de la solucion</th>" +
            "<th class='text-center'></th>" +
            "</tr>" +
            "</thead>" +
            "<tbody>";


        var TableFooter = "</tbody>" +
            "</table>" +
            "</div>";

        var Tabla = "";
        if (res === null) {
            $("#NumImagenes").html("0");
            Tabla = "<tr>" +
                "<td>" +
                "</td>" +
                "<td>" +
                "</td>" +
                "</tr>";
        } else if (res.length > 0) {
            $(res).each(function () {
                $("#NumImagenes").html(res.length);
                Tabla += "<tr>" +
                    "<td>" +
                    this.nombre.toUpperCase() +
                    "</td>" +
                    "<td>" +
                    "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarImagenEquipo(" + this.id + ")'>" +
                    "<i class='icofont icofont-trash'></i>" +
                    "</button>" +
                    "</td>" +
                    "</tr>";
            });
        }

        $("#TabImagenEquipo").html(TableHeader + Tabla + TableFooter);
    });
}
function MostrarModalImagen() {
    $("#ModalImagenEquipo").modal("show");
}
function ValidateFormNuevaImagen() {
    var result = false;

    $('#Modal_NImagen_Nombre').css("border", "1px solid red");

    if ($('#Modal_NImagen_Nombre').val().length > 0) {
        $('#Modal_NImagen_Nombre').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_NImagen_Nombre').val().length > 0) {
        result = true;
    }

    return result;
}
function ConsultarImagenEquipo() {

    const _Cat_TipoEquipo = {
        Id: $("#NEquipo_TipoEquipo option:selected").val(),
    }

    fetch("/Inventario/CatTipoEquipoImagen/GetCat_TipoEquipoImagen_Seleccionar_IdTipoEquipo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_Cat_TipoEquipo)
    })
        .then(res => res.json())
        .then(res => {
            $("#NumImagenes").html("0");
            var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                "<table class='table text-center'>" +
                "<thead>" +
                "<tr>" +
                "<tr>" +
                "<th class='text-center'>Rutina de la solucion</th>" +
                "<th class='text-center'></th>" +
                "</tr>" +
                "</thead>" +
                "<tbody>";


            var TableFooter = "</tbody>" +
                "</table>" +
                "</div>";

            var Tabla = "";
            if (res === null) {
                $("#NumImagenes").html("0");
                Tabla = "<tr>" +
                    "<td>" +
                    "</td>" +
                    "<td>" +
                    "</td>" +
                    "</tr>";
            } else if (res.length > 0) {
                $(res).each(function () {
                    $("#NumImagenes").html(res.length);
                    Tabla += "<tr>" +
                        "<td>" +
                        this.nombre.toUpperCase() +
                        "</td>" +
                        "<td>" +
                        "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarImagenEquipo(" + this.id + ")'>" +
                        "<i class='icofont icofont-trash'></i>" +
                        "</button>" +
                        "</td>" +
                        "</tr>";
                });
            }

            $("#TabImagenEquipo").html(TableHeader + Tabla + TableFooter);
        });
}