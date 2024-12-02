
$('#MenuTipoEmail').addClass("active");
$('#MenuPermisos').addClass("active");


$('#TURL').DataTable({
    pageLength: 10,
    lengthMenu: [[10, 20, -1], [10, 20, 'Todas']],
    language: {
        url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
    }
}).on('init.dt', function () {

    $('.dataTables_filter input').attr('placeholder', 'Urls');
});



function CargarMenuAcceso() {
    const MenuRol = {
        Id: $("#selectRol").val(),
    };

    fetch("/Administracion/Menu/GetMenu_IdRol", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(MenuRol)
    })
        .then(res => res.json())
        .then(res => {
            tabla(res);
        });
}
function tabla(datos) {
    var table = $('#TURL').DataTable();
    table.destroy();
    Contenido.innerHTML = "";
    for (let valor of datos) {

        Contenido.innerHTML +=
            `
            <tr>
                <td>${valor.url}</td>
               

                <td style="padding-right: 10px; position: relative; text-align:center;">
                    <i onclick="EliminarAcceso(${valor.id})" class="icofont icofont-ui-delete btn btn-inverse-danger waves-effect waves-light"></i>
                </td>
            </tr>
        `
    }
    $('#TURL').DataTable({
        pageLength: 10,
        lengthMenu: [[10, 20, -1], [10, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {

        $('.dataTables_filter input').attr('placeholder', 'URLS');
    });

}
function EliminarAcceso(Id) {
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

                const MenuRol = {
                    Id: Id
                };

                fetch("/Administracion/Menu/Delete_Menu", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(MenuRol)
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
                                        CargarMenuAcceso();
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


function CargaPermisoFaltante() {

    const UsuarioFlujo = {
        Id: $("#selectRol").val(),
    };

    fetch("/Administracion/Menu/GetCat_Menu_Listar_Faltante", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(UsuarioFlujo)
    })
        .then(res => res.json())
        .then(res => {
            $("#selectMenus").empty();

            var option = $(document.createElement('option'));
            option.text("SELECCIONAR");
            option.val(0);

            $("#selectMenus").append(option);

            if (res.length > 0) {
                $(res).each(function () {
                    var option = $(document.createElement('option'));

                    option.text(this.url);
                    option.val(this.id);

                    $("#selectMenus").append(option);
                });
            }
        });
}


function ValidarSelec() {
    var result = false;
    $('#selectRol').css("border", "1px solid #ff0049");
    if ($("#selectRol option:selected").val() > 0) { $('#selectRol').css("border", "1px solid #cccccc"); }
    result = (
        $("#selectRol option:selected").val() > 0
    );
    return result;
};
function AgregarPermiso() {
    if (ValidarSelec()) {
        CargaPermisoFaltante();
        $("#ModalPermiso").modal("show");
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

        toastr.error("Por favor, selecciona un rol.", "Notificación");

    }
}
function ValidarSelecMenus() {
    var result = false;
    $('#selectMenus').css("border", "1px solid #ff0049");
    if ($("#selectMenus option:selected").val() > 0) { $('#selectMenus').css("border", "1px solid #cccccc"); }
    result = (
        $("#selectMenus option:selected").val() > 0
    );
    return result;
};


function AsignarMenu() {
    if (ValidarSelecMenus()) {
        const Rol = {
            Id: $("#selectRol").val()
        }
        const MenuRol = {
            Id: $("#selectMenus").val(),
            Rol: Rol
        };

        fetch("/Administracion/Menu/Insert_MenuPermiso", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(MenuRol)
        })
            .then(res => res.json())
            .then(res => {
                if (res.id > 0) {
                    swal({
                        title: "Asignado!",
                        text: "Menú asignado al Rol.",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                $('#ModalPermiso').modal('hide');
                                CargarMenuAcceso();
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

        toastr.error("Por favor, selecciona un menú valido.", "Notificación");

    }
}