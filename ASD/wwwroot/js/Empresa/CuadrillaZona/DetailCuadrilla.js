
$(document).ready(function () {
    $('#TZona').DataTable({
        pageLength: 10,
        lengthMenu: [[5, 10, 20, -1], [5, 10, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {
        $('.dataTables_filter input').attr('placeholder', 'Zonas de atención');
    });
});

function eliminar(Ids) {
    swal({
        title: "Eliminar",
        text: "Eliminar registro?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, Eliminar!",
        cancelButtonText: "No, cancelar!",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (isConfirm) {
            if (isConfirm) {

                const CuadrillaZona = {
                    Id: Ids
                };

                fetch("/Empresa/CuadrillaZona/Delete_CuadrillaZona", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(CuadrillaZona)
                })
                    .then(res => res.json())
                    .then(res => {
                        if (res.id > 0) {
                            swal({
                                title: "Eliminado!",
                                text: "El registro ha sido eliminado.",
                                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/delete.png",
                            },
                                function (isConfirm) {
                                    if (isConfirm) {
                                        //CargaEquipos();
                                        location.reload();
                                    }
                                });

                        } else {
                            swal({
                                title: "Registro no eliminado",
                                text: "No es posible realizar la operacion selecionada!",
                                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                                buttons: true,
                                dangerMode: true,
                            }).then((willDelete) => {

                            });
                        }
                    });

            }
        });
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

    $('#NSucursal_Estado').css("border", "1px solid red");

    if ($("#NSucursal_Estado option:selected").val() > 0) {
        $('#NSucursal_Estado').css("border", "1px solid #d9dee3");

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

    $('#NSucursal_Poblacion').css("border", "1px solid red");

    if ($("#NSucursal_Poblacion option:selected").val() > 0) {
        $('#NSucursal_Poblacion').css("border", "1px solid #d9dee3");

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

    $('#NSucursal_CP').css("border", "1px solid red");

    if ($("#NSucursal_CP option:selected").val() > 0) {
        $('#NSucursal_CP').css("border", "1px solid #d9dee3");

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

function RegistrarZona() {
    if (Validar()) {

        const Colonia = {
            Id: $("#NSucursal_Colonia option:selected").val()
        };

        const Cuadrilla = {
            Id: xona
        };

        const CuadrillaZona = {
            Cuadrilla: Cuadrilla,
            Colonia: Colonia
        };

        fetch("/Empresa/CuadrillaZona/Add_CuadrillaZona", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(CuadrillaZona)
        })
            .then(res => res.json())
            .then(res => {
                if (res.id > 0) {
                    swal({
                        title: "Registrada!",
                        text: "Nueva Zona Registrada.",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                //CargaModelos();
                                location.reload();
                            }
                        });

                } else {
                    swal({
                        title: "Operación no disponible",
                        text: "No es posible realizar la operacion selecionada!",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {

                    });
                }
            });
    } else {
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

        toastr.error("Por favor, completa el formulario.", "Notificación");
    }
}

function Validar() {
    var result = false;
    $('#NSucursal_Colonia').css("border", "1px solid #ff0049");
    if ($("#NSucursal_Colonia option:selected").val() > 0) { $('#NSucursal_Colonia').css("border", "1px solid #cccccc"); }
    result = (
        $("#NSucursal_Colonia option:selected").val() > 0
    );
    return result;
}

function Mostrarzona() {
    $('#FormNuevaSucursal').css('display', 'block');
}
function Ocultarzona() {
    $('#FormNuevaSucursal').css('display', 'none');
}