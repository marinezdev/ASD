$('#MenuUsuario').addClass("active");
$('#MenuUsuarioOperacion').addClass("active");

$('#TablaEtapas').DataTable({
    pageLength: 10,
    lengthMenu: [[10, 20, -1], [10, 20, 'Todas']],
    language: {
        url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
    }
}).on('init.dt', function () {

    $('.dataTables_filter input').attr('placeholder', 'Etapas');
});

$("#AgregarE").on("click", function () {
    if (ValidarSelect()) {
        $("#ModalAgregarE").modal("show");
    } else {
        toastr.options = {
            "closeButton": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "iconClass": "toast-error" // Puedes usar diferentes clases de iconos de FontAwesome
        };

        toastr.error("Por favor, elige el campo o campos que están resaltados en rojo.", "Notificación");

    }

});

$("#AsignarFlujo").on("click", function () {
    if (ValidarSelec()) {
        $("#ModalFlujo").modal("show");
    } else {
        toastr.options = {
            "closeButton": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "iconClass": "toast-error" // Puedes usar diferentes clases de iconos de FontAwesome
        };

        toastr.error("Por favor, selecciona el campo o los campos resaltados en rojo.", "Notificación");

    }

});

function ValidarSelec() {
    var result = false;
    $('#selectUsuario, #selectRol').css("border", "1px solid #ff0049");
    if ($("#selectUsuario option:selected").val() > 0) { $('#selectUsuario').css("border", "1px solid #cccccc"); }
    if ($("#selectRol option:selected").val() > 0) { $('#selectRol').css("border", "1px solid #cccccc"); }
    result = (
        $("#selectUsuario option:selected").val() > 0 &&
        $("#selectRol option:selected").val() > 0
    );
    return result;
};
function ValidarSelect() {
    var result = false;
    $('#selectUsuario, #selectRol, #selectFlujo').css("border", "1px solid #ff0049");
    if ($("#selectUsuario option:selected").val() > 0) { $('#selectUsuario').css("border", "1px solid #cccccc"); }
    if ($("#selectRol option:selected").val() > 0) { $('#selectRol').css("border", "1px solid #cccccc"); }
    if ($("#selectFlujo option:selected").val() > 0) { $('#selectFlujo').css("border", "1px solid #cccccc"); }
    result = (
        $("#selectUsuario option:selected").val() > 0 &&
        $("#selectRol option:selected").val() > 0 &&
        $("#selectFlujo option:selected").val() > 0
    );
    return result;
};
function CargaRol() {

    const Cat_Rol = {
        Id: $("#selectUsuario").val()
    };

    fetch("/Administracion/Cat_Rol/GetCat_Cat_Rol_Listar_IdUsuario", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(Cat_Rol)
    })
        .then(res => res.json())
        .then(res => {
            $("#selectRol").empty();

            var option = $(document.createElement('option'));
            option.text("SELECCIONAR");
            option.val(0);

            $("#selectRol").append(option);

            if (res.length > 0) {
                $(res).each(function () {
                    var option = $(document.createElement('option'));

                    option.text(this.nombre);
                    option.val(this.id);

                    $("#selectRol").append(option);
                });
            }
        });
}
function CargaFlujo() {

    const Flujo = {
        Id: $("#selectUsuario").val()
    };

    fetch("/Operacion/Flujo/GetCat_Flujo_Listar_IdUsuario", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(Flujo)
    })
        .then(res => res.json())
        .then(res => {
            $("#selectFlujo").empty();

            var option = $(document.createElement('option'));
            option.text("SELECCIONAR");
            option.val(0);

            $("#selectFlujo").append(option);

            if (res.length > 0) {
                $(res).each(function () {
                    var option = $(document.createElement('option'));

                    option.text(this.nombre);
                    option.val(this.id);

                    $("#selectFlujo").append(option);
                });
            }
        });
}
function CargaEtapas() {

    const Usuario = {
        Id: $("#selectUsuario").val(),
    }
    const Etapa = {
        Id: $("#selectFlujo").val(),
        Usuario: Usuario
    };

    fetch("/Operacion/UsuarioEtapa/GetCat_Etapa_Listar_IdUsuario", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(Etapa)
    })
        .then(res => res.json())
        .then(res => {
            tabla(res);
        });
}
function tabla(datos) {
    var table = $('#TablaEtapas').DataTable();
    table.destroy();
    Contenido.innerHTML = "";
    for (let valor of datos) {

        Contenido.innerHTML +=
        `
            <tr>
                <td>${valor.etapa.nombre}</td>
                <td>
                  <i class="icofont icofont-ui-press" style="color:${valor.etapa.color};font-size: 176%;"></i>
                </td>

                <td style="padding-right: 10px; position: relative; text-align:center;">
                    <i onclick="EliminarEtapa(${valor.id})" class="icofont icofont-ui-delete btn btn-inverse-danger waves-effect waves-light"></i>
                </td>
            </tr>
        `
    }
    $('#TablaEtapas').DataTable({
        pageLength: 10,
        lengthMenu: [[10, 20, -1], [10, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {

        $('.dataTables_filter input').attr('placeholder', 'Etapas');
    });

}
function EliminarEtapa(Ids) {
    swal({
        title: "Eliminar",
        text: "Esta seguro de eliminar el registro?",
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

                const Etapa = {
                    Id: Ids
                };

                fetch("/Operacion/UsuarioEtapa/Delete_Etapa", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(Etapa)
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
                                        CargaEtapas();
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
function CargaFlujoFaltante() {

    const Flujo = {
        Id: $("#selectUsuario").val()
    };

    fetch("/Operacion/Flujo/GetCat_Flujo_Listar_Faltante", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(Flujo)
    })
        .then(res => res.json())
        .then(res => {
            $("#selectNFlujo").empty();

            var option = $(document.createElement('option'));
            option.text("SELECCIONAR");
            option.val(0);

            $("#selectNFlujo").append(option);

            if (res.length > 0) {
                $(res).each(function () {
                    var option = $(document.createElement('option'));

                    option.text(this.nombre);
                    option.val(this.id);

                    $("#selectNFlujo").append(option);
                });
            }
        });
}
function CargaEtapaFaltante() {

    const Flujo = {
        Id: $("#selectFlujo").val()
    };

    const UsuarioFlujo = {
        Id: $("#selectUsuario").val(),
        Flujo: Flujo
    };

    fetch("/Operacion/UsuarioEtapa/GetCat_Etapa_Listar_Faltante", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(UsuarioFlujo)
    })
        .then(res => res.json())
        .then(res => {
            $("#selectNEtapa").empty();

            var option = $(document.createElement('option'));
            option.text("SELECCIONAR");
            option.val(0);

            $("#selectNEtapa").append(option);

            if (res.length > 0) {
                $(res).each(function () {
                    var option = $(document.createElement('option'));

                    option.text(this.etapa.nombre);
                    option.val(this.id);

                    $("#selectNEtapa").append(option);
                });
            }
        });
}

function AsignarEtapa() {

    const Etapa = {
        Id: $("#selectNEtapa").val()
    };
    const UsuarioEtapa = {
        Id: $("#selectUsuario").val(),
        Etapa: Etapa
    };

    fetch("/Operacion/UsuarioEtapa/Add_UsuarioEtapa", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(UsuarioEtapa)
    })
        .then(res => res.json())
        .then(res => {
            if (res.id > 0) {
                swal({
                    title: "Etapa Asignada!",
                    text: "La etapa a sido asignada.",
                    imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            $('#ModalAgregarE').modal('hide');
                            CargaEtapas();
                        }
                    });

            } else {
                swal({
                    title: "Función no disponible",
                    text: "No es posible realizar la operacion selecionada!",
                    imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                    buttons: true,
                    dangerMode: true,
                }).then((willDelete) => {

                });
            }
        });
}

function AsignarFlujo() {
    const Usuario = {
        Id: $("#selectUsuario").val()
    };
    const Flujo = {
        Id: $("#selectNFlujo").val()
    };
    const UsuarioFlujo = {
        Usuario: Usuario,
        Flujo: Flujo
    };

    fetch("/Operacion/Flujo/Add_UsuarioFlujo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(UsuarioFlujo)
    })
        .then(res => res.json())
        .then(res => {
            if (res.id > 0) {
                NewId = res.id;
                swal({
                    title: "Nuevo flujo asignado.",
                    imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

                    showCancelButton: false,
                    confirmButtonColor: "#3070A9",
                    confirmButtonText: "Aceptar",
                    closeOnConfirm: false,
                },
                    function (isConfirm) {
                        if (isConfirm) {

                            const Flujo = {
                                Id: $("#selectUsuario").val()
                            };

                            fetch("/Operacion/Flujo/GetCat_Flujo_Listar_IdUsuario", {
                                method: "POST",
                                headers: { "Content-Type": "application/json; charset=utf-8" },
                                body: JSON.stringify(Flujo)
                            })
                                .then(res => res.json())
                                .then(res => {
                                    $("#selectFlujo").empty();

                                    var option = $(document.createElement('option'));
                                    option.text("SELECCIONAR");
                                    option.val(0);

                                    $("#selectFlujo").append(option);

                                    if (res.length > 0) {
                                        $(res).each(function () {
                                            var option = $(document.createElement('option'));

                                            option.text(this.nombre);
                                            option.val(this.id);

                                            $("#selectFlujo").append(option);
                                        });
                                        $('#selectFlujo option[value="' + NewId + '"]').attr("selected", true);

                                    }
                                });
                                swal.close();
                               $('#ModalFlujo').modal('hide');
                            
                            const Usuario = {
                                Id: $("#selectUsuario").val(),
                            }
                            const Etapa = {
                                Id: NewId,
                                Usuario: Usuario
                            };

                            fetch("/Operacion/UsuarioEtapa/GetCat_Etapa_Listar_IdUsuario", {
                                method: "POST",
                                headers: { "Content-Type": "application/json; charset=utf-8" },
                                body: JSON.stringify(Etapa)
                            })
                                .then(res => res.json())
                                .then(res => {
                                    tabla(res);
                                });
                        }
                    }
                );
            } else {
                swal({
                    title: "Función no disponible",
                    text: "No es posible realizar la operacion selecionada!",
                    imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                    buttons: true,
                    dangerMode: true,
                }).then((willDelete) => {

                });
            }
        });

}