
$(document).ready(function () {
    $("#MenuEmpresa").addClass("active");
    $("#ListaSucursal").addClass("active");

    $('#TablaSucursales').DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 20, -1], [5, 10, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {
        $('.dataTables_filter input').attr('placeholder', 'Buscar Sucursales');
    });
});

function Consultar() {
    $("#loading").css("display", "block");

    let Id = $("#selectEm").val();

        const Sucursal = {
            Id: Id
        };

        fetch("/Empresa/Sucursal/GetSucursalIdEmpresa", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(Sucursal)
        })
        .then(res => res.json())
        .then(res => {
            tabla(res);
        });
}

function tabla(datos) {
    var table = $('#TablaSucursales').DataTable();
    table.destroy();
    Contenido.innerHTML = "";

    for (let valor of datos) {

        Contenido.innerHTML += `

                     <tr>
                        <td>${valor.nombre}</td>
                        <td class="text-center">${valor.direccion}</td>
                        <td class="text-center">${valor.cat_Colonia.nombre}</td>
                        <td class="text-center">${valor.cat_Colonia.cat_CP.nombre}</td>
                        <td class="text-center">${valor.cat_Colonia.cat_CP.cat_Poblacion.nombre}</td>
                        <td class="text-center">${valor.cat_Colonia.cat_CP.cat_Poblacion.cat_Estado.nombre}</td>
                        <td class="text-center" onclick="Mapa(${valor.latitud}, ${valor.longitud});">
                         <a href="#mapa">
                            <i class="icofont icofont-map" aria-hidden="true" style="font-size: 170%"></i>
                          <a/>
                        </td>
                    </tr>
                `
    }

    $("#loading").css("display", "none");

    $('#TablaSucursales').DataTable({
        pageLength: 10,
        lengthMenu: [[5,10, 20, -1], [5, 10, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {
        $('.dataTables_filter input').attr('placeholder', 'Buscar Sucursales');
    });
}

function NuevaSucursal() {
    const IdEMpresa = $("#selectEm option:selected").val();

    if (IdEMpresa > 0) {
        $('#NuevaS').css('display', 'block');
        $('#selectEm').css("border", "1px solid black");
    } else {
        $('#selectEm').css("border", "1px solid red");
    }
}

function Mapa(la, lo) {
    if (la === null || lo === null) {
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

        toastr.error("Ubicación no registrada", "Notificación");
    } else {
        var anchoVentana = 600;
        var altoVentana = 400;
        var left = (screen.width / 2) - (anchoVentana / 2);
        var top = (screen.height / 2) - (altoVentana / 2);
        var enlaceMapa = 'https://www.google.com/maps?q=' + la + ',' + lo;
        var opcionesVentana = 'width=' + anchoVentana + ',height=' + altoVentana + ',location=no,menubar=no,status=no,toolbar=no,left=' + left + ',top=' + top;
        window.open(enlaceMapa, '_blank', opcionesVentana);
    }
}

function ValidateFormSucural() {

    var result = false;
    var posicion = "NSucursal_Direccion";

    $('#select2-NSucursal_Estado-container,#selectEm, #NSucursal_Poblacion, #NSucursal_CP, #NSucursal_Colonia, #NSucursal_TipoSucursal, #NSucursal_NombreSucursal, #NSucursal_Direccion').css("border", "1px solid red");


    if ($("#select2-NSucursal_Estado-container option:selected").val() > 0) {
        $('#select2-NSucursal_Estado-container').css("border", "1px solid #d9dee3");
    }
    if ($("#selectEm option:selected").val() > 0) {
        $('#selectEm').css("border", "1px solid #d9dee3");
    }

    if ($("#NSucursal_Poblacion option:selected").val() > 0) {
        $('#NSucursal_Poblacion').css("border", "1px solid #d9dee3");
    }

    if ($("#NSucursal_CP option:selected").val() > 0) {
        $('#NSucursal_CP').css("border", "1px solid #d9dee3");
    }

    if ($("#NSucursal_Colonia option:selected").val() > 0) {
        $('#NSucursal_Colonia').css("border", "1px solid #d9dee3");
    }

    if ($("#NSucursal_TipoSucursal option:selected").val() > 0) {
        $('#NSucursal_TipoSucursal').css("border", "1px solid #d9dee3");
    }

    if ($('#NSucursal_NombreSucursal').val().length > 0) {
        $('#NSucursal_NombreSucursal').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#NSucursal_Direccion').val().length > 0) {
        $('#NSucursal_Direccion').css("border", "1px solid rgba(0, 0, 0, .15)");
    }



    if ($("#select2-NSucursal_Estado-container option:selected").val() > 0) {
        if ($("#NSucursal_Poblacion option:selected").val() > 0) {
            if ($("#NSucursal_CP option:selected").val() > 0) {
                if ($("#NSucursal_Colonia option:selected").val() > 0) {
                    if ($("#NSucursal_TipoSucursal option:selected").val() > 0) {
                        if ($('#NSucursal_NombreSucursal').val().length > 0) {
                            if ($('#NSucursal_Direccion').val().length > 0) {
                                if ($('#selectEm').val().length > 0) {

                                    result = true;

                                } else {
                                    posicion = $("#NSucursal_Direccion").offset().top;
                                }
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
        posicion = $("#select2-NSucursal_Estado-container").offset().top;
    }


    $("html, body").animate({
        scrollTop: posicion - 100
    }, 500);

    return result;
}

function Guardar() {
    if (ValidateFormSucural()) {
        var NewIdSucursal = 0;
        const Cat_Colonia = {
            Id: $("#NSucursal_Colonia option:selected").val(),
        }

        const Cat_TipoSucursal = {
            Id: $("#NSucursal_TipoSucursal option:selected").val(),
        }
        const Empresa = {
            Id: $("#selectEm option:selected").val(),
        }


        const _model = {
            Cat_Colonia: Cat_Colonia,
            Cat_TipoSucursal: Cat_TipoSucursal,
            Nombre: $('#NSucursal_NombreSucursal').val(),
            Direccion: $('#NSucursal_Direccion').val(),
            Latitud: $('#Latitud').val(),
            Longitud: $('#Longitud').val(),
            Empresa: Empresa
        };

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


                                            $('#select2-NSucursal_Estado-container option[value="0"]').attr("selected", true);
                                            $('#NSucursal_TipoSucursal option[value="0"]').attr("selected", true);
                                            $('#NSucursal_NombreSucursal').val("");
                                            $('#NSucursal_Direccion').val("");

                                            $("#NSucursal_Poblacion").empty();
                                            $("#NSucursal_CP").empty();
                                            $("#NSucursal_Colonia").empty();

                                            $("#Latitud").val("");
                                            $("#Longitud").val("");


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

                                            Consultar();

                                        }
                                    });

                            }
                        }
                    );
                }
            });
    } else {
    }
}

//////////////////////////////////
///  CARGA DE DIRECCIONES 
//////////////////////////////////
function CargaPoblacion() {
    var IdEstdo = $("#select2-NSucursal_Estado-container option:selected").val();

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

            } else if (res.length > 0) {

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

            } else if (res.length > 0) {
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

            $("#NSucursal_Colonia").empty();

            var option = $(document.createElement('option'));
            option.text("SELECCIONAR");
            option.val(0);

            $("#NSucursal_Colonia").append(option);

            if (res === null) {

            } else if (res.length > 0) {

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

        // Prevenir el env�o del formulario por defecto
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
                            imageUrl: "http://localhost:5269/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id == 0) {
                        swal({
                            title: "Estado no registrado",
                            text: "No es posible registrar el estado. !",
                            imageUrl: "http://localhost:5269/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id > 0) {
                        NewIdEstado = res.id;
                        swal({
                            title: "Nuevo estado registrado.",
                            text: Estado,
                            imageUrl: "http://localhost:5269/icons/operacion/create.png",

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

                                                $("#select2-NSucursal_Estado-container").empty();

                                                var option = $(document.createElement('option'));
                                                option.text("SELECCIONAR");
                                                option.val(0);

                                                $("#select2-NSucursal_Estado-container").append(option);

                                                $(res).each(function () {
                                                    var option = $(document.createElement('option'));

                                                    option.text(this.nombre.toUpperCase());
                                                    option.val(this.id);

                                                    $("#select2-NSucursal_Estado-container").append(option);
                                                });

                                                $('#select2-NSucursal_Estado-container option[value="' + NewIdEstado + '"]').attr("selected", true);


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
    $('#select2-select2-NSucursal_Estado-container-container').css("border", "1px solid red");

    if ($("#select2-NSucursal_Estado-container option:selected").val() > 0) {
        $('#select2-select2-NSucursal_Estado-container-container').css("border", "0px solid #d9dee3");

        $("#ModalPoblacion").modal("show");
    }

}

$(document).ready(function () {
    $("#NuevaPoblacion").on("submit", function (event) {

        // Prevenir el env�o del formulario por defecto
        event.preventDefault();

        if (ValidateFormNuevaPoblacion()) {

            var NewIdPoblacion = 0;
            var Poblacion = $("#Modal_NPoblacion").val();
            var IdEstado = $("#select2-NSucursal_Estado-container option:selected").val();

            const Cat_Estado = {
                Id: IdEstado,
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
                            imageUrl: "http://localhost:5269/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id == 0) {
                        swal({
                            title: "Poblacion no registrada",
                            text: "No es posible registrar la poblacion. !",
                            imageUrl: "http://localhost:5269/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id > 0) {
                        NewIdPoblacion = res.id;
                        swal({
                            title: "Nueva poblacion registrada.",
                            text: Poblacion,
                            imageUrl: "http://localhost:5269/icons/operacion/create.png",

                            showCancelButton: false,
                            confirmButtonColor: "#3070A9",
                            confirmButtonText: "Aceptar",
                            closeOnConfirm: false,
                        },
                            function (isConfirm) {
                                if (isConfirm) {
                                    swal.close();
                                    $('#ModalPoblacion').modal('hide');


                                    var IdEstdo = $("#select2-NSucursal_Estado-container option:selected").val();

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

        // Prevenir el env�o del formulario por defecto
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
                            imageUrl: "http://localhost:5269/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id == 0) {
                        swal({
                            title: "Codigo postal no registrado",
                            text: "No es posible registrar el codigo postal. !",
                            imageUrl: "http://localhost:5269/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id > 0) {
                        NewIdCP = res.id;
                        swal({
                            title: "Nuevo codigo postal registrado.",
                            text: CP,
                            imageUrl: "http://localhost:5269/icons/operacion/create.png",

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

        // Prevenir el env�o del formulario por defecto
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
                            imageUrl: "http://localhost:5269/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id == 0) {
                        swal({
                            title: "Colonia no registrada",
                            text: "No es posible registrar la colonia. !",
                            imageUrl: "http://localhost:5269/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {

                        });
                    } else if (res.id > 0) {
                        NewIdColonia = res.id;
                        swal({
                            title: "Nueva colonia registrada.",
                            text: Colonia,
                            imageUrl: "http://localhost:5269/icons/operacion/create.png",

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

