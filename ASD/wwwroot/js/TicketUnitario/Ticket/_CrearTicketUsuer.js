$('#MenuTicket2').addClass("active");
$('#MenuTicketNuevoTicket').addClass("active");

$(document).ready(function () {
    // Single Search Select
    $(".js-example-basic-single").select2();

    $("#NuevoTicket").on("submit", function (event) {

        // Prevenir el envío del formulario por defecto
        event.preventDefault();

        if (ValidateForm()) {
            var EquipoCheck = 0;
            var ArchivosCheck = 0;
            const Flujo = {
                Id: $("#Ticket_TipoServicio option:selected").val(),
            }


            if (chkAddEquipo.checked) {
                EquipoCheck = 1;
            }

            if (chkArchivos.checked) {
                ArchivosCheck = 1;
            }

            const Equipo = {
                Id: $("#Ticket_Equipo option:selected").val(),
            }

            const TicketEquipo = {
                Equipo: Equipo
            }

            const Ticket = {
                Flujo: Flujo,
                Titulo: $('#Ticket_Titulo').val(),
                Detalle: $('#Ticket_Detalle').val(),
            }

            const _model = {
                Ticket: Ticket,
                TicketEquipo: TicketEquipo,
                Archivo: ArchivosCheck,
                Equipo: EquipoCheck,
            };

            console.log(_model);

            fetch("/TicketUnitario/Ticket/CreateTicket_Crear_Usuario", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
            .then(res => res.json())
            .then(res => {
                if (res.ticket.id > 0) {


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
    var Formulario = false;
    var posicion = "";

    $('#select2-Ticket_TipoServicio-container').css("border", "1px solid red");
    $('#Ticket_Titulo').css("border", "1px solid red");


    if ($("#Ticket_TipoServicio option:selected").val() > 0) {
        $('#select2-Ticket_TipoServicio-container').css("border", "0px solid #d9dee3");
    }

    if ($('#Ticket_Titulo').val().length > 0) {
        $('#Ticket_Titulo').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($("#Ticket_TipoServicio option:selected").val() > 0) {
        if ($('#Ticket_Titulo').val().length > 0) {
            Formulario = true;
        } else {
            posicion = $("#Ticket_Titulo").offset().top;
        }
    } else {
        posicion = $("#Ticket_TipoServicio").offset().top;
    }

    if (Formulario) {
        result = true;
    }

    if (!result) {
        $("html, body").animate({
            scrollTop: posicion - 100
        }, 500);
    }


    return result;
}

/******************************************************************************************** */
/*****************************  SUBIR ARCHIVOS ADICIONALES  ********************************* */
/******************************************************************************************** */
function SubirArchivoAdicional() {
    $("#FileArchivo").click();
}
function ValidaUploadFileAdicional() {
    var fileInput = document.getElementById('FileArchivo');

    if (fileInput.files.length > 0) {

        for (i = 0; i < fileInput.files.length; i++) {
            var Formato = "";
            var Carga = false;
            var ext = fileInput.files[i].name.split('.').pop();
            Extencion = fileInput.files[i].name.split('.').pop();

            // Convertimos en min�scula porque
            // la extensi�n del archivo puede estar en may�scula
            ext = ext.toLowerCase();

            if (ext == 'png' || ext == 'jpg' || ext == 'jpeg' || ext == 'docx' || ext == 'xlsx' || ext == 'rar' || ext == 'pdf') {
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

                $("#DivCargaArchivo").html(Div);
                $("#ModalCargaArchivo").modal("show");

                UploadFileAdicional();

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

                toastr.error("Intentas cargar un formato no permitido: <strong>" + Formato + "</strong> Formato de archivo permitido: <strong>.png, .jpg, .jpeg, .docx, .xlsx, .rar, .pdf  </strong>", "Notificacion");
            }
        }
    }
}
function UploadFileAdicional() {

    var progressBar = document.getElementById('progressBar');
    var formData = new FormData();
    var fileInput = document.getElementById('FileArchivo');

    formData.append('file', fileInput.files[0]);

    fetch('/Ticket/Archivo/AddListArchivo', {
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

                    $("#BtnCargaArchivo").click();

                    // Carga completada
                    /* console.log('Carga completada');*/
                    $('#ListArchivos').html("");
                    // id de la nueva imagen
                    CargaArchivos();

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
function CargaArchivos() {

    fetch("/Ticket/Archivo/GetListArchivo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
    })
        .then(res => res.json())
        .then(res => {
            var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                "<table class='table text-center'>" +
                "<thead>" +
                "<tr>" +
                "<th class='text-center'>Archivo</th>" +
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
                    "<td>" +
                    "</td>" +
                    "<td>" +
                    "</td>" +
                    "</tr>";
            } else if (res.length > 0) {
                $(res).each(function () {
                    Tabla += "<tr>" +
                        "<td>" +
                        this.nmOriginal.toUpperCase() +
                        "</td>" +
                        "<td>" +
                        "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarArchivo(" + this.id + ")'>" +
                        "<i class='icofont icofont-trash'></i>" +
                        "</button>" +
                        "</td>" +
                        "</tr>";
                });
            }

            $("#ListArchivos").html(TableHeader + Tabla + TableFooter);

        });
}
function EliminarArchivo(Id) {
    const _modelo = {
        Id: Id,
    };

    fetch("/Ticket/Archivo/DeleteListArchivo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
        .then(res => res.json())
        .then(res => {
            CargaArchivos();
            //if (res.id > 0) {
            //    CargaArchivos();
            //} else {
            //    toastr.options = {
            //        "closeButton": true,
            //        "progressBar": true,
            //        "positionClass": "toast-top-right",
            //        "showDuration": "500",
            //        "hideDuration": "1000",
            //        "timeOut": "5000",
            //        "extendedTimeOut": "1000",
            //        "iconClass": "toast-error"
            //    };

            //    toastr.error("Archivo no eliminado ", "Notificacion");
            //}

        });
}

/***************************************************************************************** */
/***************************************************************************************** */
/***************************************************************************************** */
function showEquipoForm() {
    
    // Get a reference to the element where you want to display the form
    const equipoForm = document.getElementById("equipoForm");

    if (chkAddEquipo.checked) {
        equipoForm.style.display = "block"; // Show the form when checked
    } else {
        equipoForm.style.display = "none"; // Hide the form when unchecked
    }
}
function showArchivosForm() {

    const archivosForm = document.getElementById("ArchivosForm");
    const archivosForm2 = document.getElementById("ArchivosForm2");

    if (chkArchivos.checked) {
        archivosForm.style.display = "block"; // Show the form when checked
        archivosForm2.style.display = "block"; // Show the form when checked
    } else {
        archivosForm.style.display = "none"; // Hide the form when unchecked
        archivosForm2.style.display = "none"; // Hide the form when unchecked
    }
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
            var FechaApertura = null;
            var FechaAdquisicion = null;
            var FechaCaducidad = null;
            var FechaGarantia = null;
            const Cat_Modelo = {
                Id: $("#NEquipo_Modelo option:selected").val(),
            }

            const Cat_EstatusEquipo = {
                Id: $("#NEquipo_EstatusEquipo option:selected").val(),
            }


            if ($('#NEquipo_FechaApartura').val().length > 0) {
                FechaApertura = $('#NEquipo_FechaApartura').val();
            } 

            if ($('#NEquipo_FechaAdquisicion').val().length > 0) {
                FechaAdquisicion = $('#NEquipo_FechaAdquisicion').val();
            } 

            if ($('#NEquipo_FechaCaducidad').val().length > 0) {
                FechaCaducidad = $('#NEquipo_FechaCaducidad').val();
            } 

            if ($('#NEquipo_FechaGarantia').val().length > 0) {
                FechaGarantia = $('#NEquipo_FechaGarantia').val();
            } 

            const _model = {
                Cat_Modelo: Cat_Modelo,
                Cat_EstatusEquipo: Cat_EstatusEquipo,
                Serie: $('#NEquipo_NumeroSerie').val(),
                FechaApertura: FechaApertura,
                FechaAdquisicion: FechaAdquisicion,
                FechaCaducidad: FechaCaducidad,
                FechaGarantia: FechaGarantia,
                Observaciones: $('#NEquipo_Observaciones').val(),
            };

            fetch("/Inventario/EquipoUsuario/CreateEquipo", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
                .then(res => res.json())
                .then(res => {
                    if (res.id == -1) {
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

                                    fetch("/Inventario/EquipoUsuario/GetEquipoUsuario_Seleccionar_IdUsuario", {
                                        method: "POST",
                                        headers: { "Content-Type": "application/json; charset=utf-8" },
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

                                                $('#NEquipo_NumeroSerie').val("");
                                                $('#NEquipo_FechaApartura').val("");
                                                $('#NEquipo_FechaAdquisicion').val("");
                                                $('#NEquipo_FechaCaducidad').val("");
                                                $('#NEquipo_FechaGarantia').val("");
                                                $('#NEquipo_Observaciones').val("");

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

                                                $('#FormNuevoEquipo').fadeOut(100);
                                                $('#FormInstrucciones').show();
                                                $("#NumRutina").html("0");
                                                $("#NumImagenes").html("0");

                                                $("#TabImagenEquipo").html("");
                                                $("#TabRutinaEquipo").html("");


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
    $('#FormInstrucciones').fadeOut(0);
    $('#FormNuevoEquipo').show();
    
    var posicion = $("#FormNuevoEquipo").offset().top;
    $("html, body").animate({
        scrollTop: posicion - 100
    }, 500);
}
function CancelarNuevoEquipo() {
    $('#FormNuevoEquipo').fadeOut(400);
    $('#FormInstrucciones').show();
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

    if ($("#NEquipo_TipoEquipo option:selected").val() > 0) {
        if ($("#NEquipo_Categoria option:selected").val() > 0) {
            if ($("#NEquipo_Modelo option:selected").val() > 0) {
                if ($("#NEquipo_EstatusEquipo option:selected").val() > 0) {
                    if ($('#NEquipo_NumeroSerie').val().length > 0) {
                        result = true;
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