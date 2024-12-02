
$(document).ready(function () {
    var tabValue = obtenerValorParametro('tab');
    if (tabValue !== null) {
        $('#' + tabValue).trigger('click');
    }
    $("#MenuTicket").addClass("active");

    $('#TEmpresaAtencion').DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {
        $('.dataTables_filter input').attr('placeholder', 'Empresa Atención');
    });

    $('#TEstatusFechaAtencion').DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {
        $('.dataTables_filter input').attr('placeholder', 'Fecha Atención');
    });

    $('#TEstatusOrdenTrabajo').DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {
        $('.dataTables_filter input').attr('placeholder', 'Orden de Trabajo');
    });

    $('#TPrioridad').DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {
        $('.dataTables_filter input').attr('placeholder', 'Prioridad');
    });
});

//PROCESOS EMPRESA ATENCION
function NuevaEmpresaAtencion() {
    if (ValidarNuevaEmpresaAtencion()) {
        const nombreEmpresa = $("#Modal_EmpresaAtencion_Nombre").val();
        const catAsignacionEmpresa = {
            Nombre: nombreEmpresa
        };
        fetch("/Ticket/Cat_AsignacionEmpresa/InsertAsignacionEmpresa", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(catAsignacionEmpresa)
        })
            .then(response => response.json())
            .then(response => {
                if (response.id === 1) {
                    swal({
                        title: "Registrado!",
                        text: "Operación Exitosa",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();
                                $('#ModalEmpresaA').modal('hide');
                                CargaEmpresaAtencion();
                            }
                        });
                } else {
                    swal({
                        title: "Error",
                        text: "No es posible realizar la operación seleccionada!",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    })
                        .then(willDelete => {
                            // Puedes agregar más lógica aquí si es necesario
                        });
                }
            });
    }

}
function eliminarEmpresaAtencion(Ids) {
    swal({
        title: "¿Estás seguro?",
        text: "Esta acción eliminará el estatus. ¿Estás seguro de continuar?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, eliminar!",
        cancelButtonText: "No, cancelar!",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                const cat_AsignacionEmpresa = {
                    Id: Ids
                };

                fetch("/Ticket/Cat_AsignacionEmpresa/DeleteAsignacionEmpresa", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(cat_AsignacionEmpresa)
                })
                    .then(response => response.json())
                    .then(response => {
                        if (response.id > 0) {
                            swal({
                                title: "Eliminado!",
                                text: "El registro ha sido eliminado.",
                                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/delete.png",
                            },
                                function (isConfirm) {
                                    if (isConfirm) {
                                        CargaEmpresaAtencion();
                                    }
                                });
                        } else {
                            swal({
                                title: "Registro no eliminado",
                                text: "No es posible realizar la operación seleccionada!",
                                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                                buttons: true,
                                dangerMode: true,
                            }).then((willDelete) => {
                                // Puedes realizar acciones adicionales aquí si es necesario
                            });
                        }
                    });
            } else {
                // El usuario ha cancelado la operación
                swal("Cancelado", "La operación ha sido cancelada", "error");
            }
        });
}
function InfoEmpresaAtencion(Id, nombre) {
    $("#Modal_EmpresaAtención_NombreEditar").val(nombre);
    $("#EditarEmpresaAtencion").off("click").on("click", function () {
        EditarEmpresaAtencion(Id);
    });
}
function EditarEmpresaAtencion(Id) {
    if (ValidarEditarEmpresaAtencion()) {
        const cat_AsignacionEmpresa = {
            Id: Id,
            Nombre: $("#Modal_EmpresaAtención_NombreEditar").val(),
        };
        fetch("/Ticket/Cat_AsignacionEmpresa/UpdateAsignacionEmpresa", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(cat_AsignacionEmpresa)
        })
            .then(res => res.json())
            .then(res => {
                if (res.id == 1) {
                    swal({
                        title: "Actualizado!",
                        text: "El registro ha sido Actualizado.",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/update.png",
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();
                                $('#ModalEditarEmpresaAt').modal('hide');
                                CargaEmpresaAtencion();
                            }
                        });
                } else {
                    swal({
                        title: "Registro no Actualizado",
                        text: "No es posible realizar la operación seleccionada!",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {
                    });
                }
            });
    }


}
function CargaEmpresaAtencion() {
    fetch("/Ticket/Cat_AsignacionEmpresa/GetAsignacionEmpresa", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
    })
        .then(res => res.json())
        .then(res => {
            TablaEmpresaAtencion(res);
        });
}
function TablaEmpresaAtencion(datos) {
    var table = $('#TEmpresaAtencion').DataTable();
    table.destroy();
    BEmpresaAtencion.innerHTML = "";


    for (let valor of datos) {

        BEmpresaAtencion.innerHTML += `
              <tr>
                <td>${valor.nombre}</td>
                    <td style="border-right: 2px solid #eceeef; padding-right: 10px; position: relative;text-align:center;">
                        <i class="ti-pencil-alt btn btn-inverse-primary waves-effect waves-light" style="margin-right: 10px;" onclick="InfoEmpresaAtencion('${valor.id}', '${valor.nombre}')" data-toggle="modal" data-target="#ModalEditarEmpresaAt"></i>
                        <i class="icofont icofont-ui-delete btn btn-inverse-danger waves-effect waves-light" onclick="eliminarEmpresaAtencion(${valor.id})"></i>
                </td>
            </tr>
            `
             //<td style="display: flex; justify-content: center; align-items: center; position: relative;">
                //    33
                //    <span onclick="Consultar(${valor.id})" style="position: absolute; right: 5%;">
                //        <i class="icofont icofont-eye-alt btn btn-inverse-success waves-effect waves-light"></i>
                //    </span>
                //</td>
    }

    $('#TEmpresaAtencion').DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {
        $('.dataTables_filter input').attr('placeholder', 'Empresa Atención');
    });
}
//PROCESOS FechaAtencion
function eliminarFechaAtencion(Ids) {
    swal({
        title: "¿Estás seguro?",
        text: "Esta acción eliminará el estatus. ¿Estás seguro de continuar?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, eliminar!",
        cancelButtonText: "No, cancelar!",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                const cat_EstatusFechaAtencion = {
                    Id: Ids
                };

                fetch("/Ticket/Cat_EstatusFechaAtencion/DeleteEstatusFechaAtencion", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(cat_EstatusFechaAtencion)
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
                                        CargaFechaAtencion();
                                }
                                });
                        } else {
                            swal({
                                title: "Registro no eliminado",
                                text: "No es posible realizar la operación seleccionada!",
                                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                                buttons: true,
                                dangerMode: true,
                            }).then((willDelete) => {
                                // Puedes realizar acciones adicionales aquí si es necesario
                            });
                        }
                    });
            } else {
                // Manejar el caso en el que se cancela la eliminación
                swal("Cancelado", "La eliminación ha sido cancelada", "error");
            }
        });
}
function NuevoEstatusFecha() {
    if (ValidarNuevoEstatusFecha()) {
        const cat_EstatusFechaAtencion = {
            Nombre: $("#Modal_EstatusFecha_Nombre").val(),
            Color: $("#colorNEstatusFecha").val()
        };

        fetch("/Ticket/Cat_EstatusFechaAtencion/InsertEstatusFechaAtencion", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(cat_EstatusFechaAtencion)
        })
            .then(response => response.json())
            .then(response => {
                if (response.id === 1) {
                    swal({
                        title: "Registrado!",
                        text: "Operación Exitosa",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();
                                $('#ModalEstatusFecha').modal('hide');
                                CargaFechaAtencion();
                            }
                        });
                } else {
                    swal({
                        title: "Error",
                        text: "No es posible realizar la operación seleccionada!",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    })
                        .then(willDelete => {
                            // Puedes agregar más lógica aquí si es necesario
                        });
                }
            });
    }

}
function InfoFechaAtencion(Id, nombre, color) {
    $("#Modal_EstatusFecha_NombreEditar").val(nombre);
    $("#colorFechaAtencion").val(color);
    $("#EditarFechaAtencion").off("click").on("click", function () {
        EditarFechaAtencion(Id);
    });
}
function EditarFechaAtencion(Id) {
    if (ValidarEditarFechaAtencion()) {
        const Cat_EstatusTicket = {
            Id: Id,
            Nombre: $("#Modal_EstatusFecha_NombreEditar").val(),
            Color: $("#colorFechaAtencion").val()
        };
        fetch("/Ticket/Cat_EstatusFechaAtencion/UpdateEstatusFechaAtencion", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(Cat_EstatusTicket)
        })
            .then(res => res.json())
            .then(res => {
                if (res.id == 1) {
                    swal({
                        title: "Actualizado!",
                        text: "El registro ha sido Actualizado.",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/update.png",
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();
                                $('#ModalEditarEFechaAtencion').modal('hide');
                                CargaFechaAtencion();
                            }
                        });
                } else {
                    swal({
                        title: "Registro no Actualizado",
                        text: "No es posible realizar la operación seleccionada!",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {
                    });
                }
            });
    }


}
function CargaFechaAtencion() {
    fetch("/Ticket/Cat_EstatusFechaAtencion/GetCat_EstatusFechaAtencion", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
    })
        .then(res => res.json())
        .then(res => {
            TablaFechaAtencion(res);
        });
}
function TablaFechaAtencion(datos) {
    var table = $('#TEstatusFechaAtencion').DataTable();
    table.destroy();
    BEstatusFechaAtencion.innerHTML = "";


    for (let valor of datos) {

        BEstatusFechaAtencion.innerHTML += `
              <tr>
                <td>
                    ${valor.nombre}
                </td>
                  <td style="border-right: 2px solid #eceeef; padding-right: 10px; position: relative;">
                    <i class="icofont icofont-square" style="color:${valor.color};font-size: 176%;"></i>
                  </td>
                      <td  style="text-align:center">
                    <i class="ti-pencil-alt btn btn-inverse-primary waves-effect waves-light" style="margin-right: 10px;" onclick="InfoFechaAtencion('${valor.id}', '${valor.nombre}', '${valor.color}')" data-toggle="modal" data-target="#ModalEditarEFechaAtencion"></i>
                    <i class="icofont icofont-ui-delete btn btn-inverse-danger waves-effect waves-light" onclick="eliminarFechaAtencion(${valor.id})"></i>
                  </td>
                </tr>
                `
            //< td style = "display: flex; justify-content: center; align-items: center; position: relative;" >
            //    15
            //    < span onclick = "Consultar(${valor.id})" style = "position: absolute; right: 5%;" >
            //        <i class="icofont icofont-eye-alt btn btn-inverse-success waves-effect waves-light"></i>
            //            </span >
            //      </td >
    }

    $('#TEstatusFechaAtencion').DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {
        $('.dataTables_filter input').attr('placeholder', 'Empresa Atención');
    });
}
//PROCESOS OrdenTrabajo
function eliminarOrdenTrabajo(Ids) {
    swal({
        title: "¿Estás seguro?",
        text: "Esta acción eliminará el estatus. ¿Estás seguro de continuar?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, eliminar!",
        cancelButtonText: "No, cancelar!",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                const Cat_EstatusOrdenTrabajo = {
                    Id: Ids
                };

                fetch("/Ticket/Cat_EstatusOrdenTrabajo/DeleteEstatusOrdenTrabajo", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(Cat_EstatusOrdenTrabajo)
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
                                        CargaOrdenTrabajo();
                                    }
                                });
                        } else {
                            swal({
                                title: "Registro no eliminado",
                                text: "No es posible realizar la operación seleccionada!",
                                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                                buttons: true,
                                dangerMode: true,
                            }).then((willDelete) => {
                                // Puedes realizar acciones adicionales aquí si es necesario
                            });
                        }
                    });
            } else {
                // Manejar el caso en el que se cancela la eliminación
                swal("Cancelado", "La eliminación ha sido cancelada", "error");
            }
        });
}
function NuevoEstatusOrdenTrabajo() {
    if (ValidarNuevoEstatusOrdenTrabajo()) {
        const cat_EstatusOrdenTrabajo = {
            Nombre: $("#Modal_EstatusOrdenTrabajo_Nombre").val(),
            Color: $("#colorNOrdenTrabajo").val()
        };

        fetch("/Ticket/Cat_EstatusOrdenTrabajo/InsertEstatusOrdenTrabajo", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(cat_EstatusOrdenTrabajo)
        })
            .then(response => response.json())
            .then(response => {
                if (response.id === 1) {
                    swal({
                        title: "Registrado!",
                        text: "Operación Exitosa",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();
                                $('#ModalEstatusOrdenTrabajo').modal('hide');
                                CargaOrdenTrabajo();
                            }
                        });
                } else {
                    swal({
                        title: "Error",
                        text: "No es posible realizar la operación seleccionada!",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    })
                        .then(willDelete => {
                            // Puedes agregar más lógica aquí si es necesario
                        });
                }
            });
    }

}
function InfoOrdenTrabajo(Id, nombre, color) {
    $("#Modal_EstatusOrdenTrabajo_NombreEditar").val(nombre);
    $("#colorOrdenTrabajo").val(color);
    $("#EditarOrdenTrabajo").off("click").on("click", function () {
        EditarOrdenTrabajo(Id);
    });
}
function EditarOrdenTrabajo(Id) {
    if (ValidarEditarOrdenTrabajo()) {
        const cat_EstatusOrdenTrabajo = {
            Id: Id,
            Nombre: $("#Modal_EstatusOrdenTrabajo_NombreEditar").val(),
            Color: $("#colorOrdenTrabajo").val()
        };
        fetch("/Ticket/Cat_EstatusOrdenTrabajo/UpdateEstatusOrdenTrabajo", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(cat_EstatusOrdenTrabajo)
        })
            .then(res => res.json())
            .then(res => {
                if (res.id == 1) {
                    swal({
                        title: "Actualizado!",
                        text: "El registro ha sido Actualizado.",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/update.png",
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();
                                $('#ModalEditarEOrdenTrabajo').modal('hide');
                                CargaOrdenTrabajo();
                            }
                        });
                } else {
                    swal({
                        title: "Registro no Actualizado",
                        text: "No es posible realizar la operación seleccionada!",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {
                    });
                }
            });
    }


}
function CargaOrdenTrabajo() {
    fetch("/Ticket/Cat_EstatusOrdenTrabajo/GetCat_EstatusOrdenTrabajo_Seleccionar", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
    })
        .then(res => res.json())
        .then(res => {
            TablaOrdenTrabajo(res);
        });
}
function TablaOrdenTrabajo(datos) {
    var table = $('#TEstatusOrdenTrabajo').DataTable();
    table.destroy();
    BEstatusOrdenTrabajo.innerHTML = "";


    for (let valor of datos) {

        BEstatusOrdenTrabajo.innerHTML += `
              <tr>
                <td>
                    ${valor.nombre}
                </td>
               
                    <td style="border-right: 2px solid #eceeef; padding-right: 10px; position: relative;">
                        <i class="icofont icofont-square" style="color:${valor.color};font-size: 176%;"></i>
                    </td>
                        <td style="text-align:center">
                       <i class="ti-pencil-alt btn btn-inverse-primary waves-effect waves-light" style="margin-right: 10px;" onclick="InfoOrdenTrabajo('${valor.id}', '${valor.nombre}', '${valor.color}')" data-toggle="modal" data-target="#ModalEditarEOrdenTrabajo"></i>
                       <i class="icofont icofont-ui-delete btn btn-inverse-danger waves-effect waves-light" onclick="eliminarOrdenTrabajo(${valor.id})"></i>
                    </td>
                        
                       
                </tr>
                `
            //< td style = "display: flex; justify-content: center; align-items: center; position: relative;" >
            //    15
            //    < span onclick = "Consultar(${valor.id})" style = "position: absolute; right: 5%;" >
            //        <i class="icofont icofont-eye-alt btn btn-inverse-success waves-effect waves-light"></i>
            //                </span >
            //            </td >
    }

    $('#TEstatusOrdenTrabajo').DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {
        $('.dataTables_filter input').attr('placeholder', 'Empresa Atención');
    });
}
//PROCESOS Prioridad
function eliminarPrioridad(Ids) {
    swal({
        title: "¿Estás seguro?",
        text: "Esta acción eliminará el estatus. ¿Estás seguro de continuar?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, eliminar!",
        cancelButtonText: "No, cancelar!",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                const cat_Prioridad = {
                    Id: Ids
                };

                fetch("/Ticket/Cat_Prioridad/DeletePrioridad", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(cat_Prioridad)
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
                                        CargaPrioridad();
                                    }
                                });
                        } else {
                            swal({
                                title: "Registro no eliminado",
                                text: "No es posible realizar la operación seleccionada!",
                                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                                buttons: true,
                                dangerMode: true,
                            }).then((willDelete) => {
                                // Puedes realizar acciones adicionales aquí si es necesario
                            });
                        }
                    });
            } else {
                // Manejar el caso en el que se cancela la eliminación
                swal("Cancelado", "La eliminación ha sido cancelada", "error");
            }
        });
}
function NuevaPrioridad() {
    if (ValidarNuevaPrioridad()) {
        const cat_Prioridad = {
            Nombre: $("#Modal_Prioridad_Nombre").val(),
            Color: $("#colorNPrioridad").val()
        };

        fetch("/Ticket/Cat_Prioridad/InsertPrioridad", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(cat_Prioridad)
        })
            .then(response => response.json())
            .then(response => {
                if (response.id === 1) {
                    swal({
                        title: "Registrado!",
                        text: "Operación Exitosa",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();
                                $('#ModalPrioridad').modal('hide');
                                CargaPrioridad();
                            }
                        });
                } else {
                    swal({
                        title: "Error",
                        text: "No es posible realizar la operación seleccionada!",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    })
                        .then(willDelete => {
                            // Puedes agregar más lógica aquí si es necesario
                        });
                }
            });
    }

}
function InfoPrioridad(Id, nombre, color) {
    $("#Modal_Prioridad_NombreEditar").val(nombre);
    $("#colorPrioridad").val(color);
    $("#EditarPrioridad").off("click").on("click", function () {
        EditarPrioridad(Id);
    });
}
function EditarPrioridad(Id) {
    if (ValidarEditarPrioridad()) {
        const cat_Prioridad = {
            Id: Id,
            Nombre: $("#Modal_Prioridad_NombreEditar").val(),
            Color: $("#colorPrioridad").val()
        };
        fetch("/Ticket/Cat_Prioridad/UpdatePrioridad", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(cat_Prioridad)
        })
            .then(res => res.json())
            .then(res => {
                if (res.id == 1) {
                    swal({
                        title: "Actualizado!",
                        text: "El registro ha sido Actualizado.",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/update.png",
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                swal.close();
                                $('#ModalEditarPrioridad').modal('hide');
                                CargaPrioridad();
                            }
                        });
                } else {
                    swal({
                        title: "Registro no Actualizado",
                        text: "No es posible realizar la operación seleccionada!",
                        imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {
                    });
                }
            });
    }


}
function CargaPrioridad() {
    fetch("/Ticket/Cat_Prioridad/GetCat_Prioridad_Seleccionar", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
    })
        .then(res => res.json())
        .then(res => {
            TablaPrioridad(res);
        });
}
function TablaPrioridad(datos) {
    var table = $('#TPrioridad').DataTable();
    table.destroy();
    BPrioridad.innerHTML = "";


    for (let valor of datos) {

        BPrioridad.innerHTML += `
              <tr>
                <td>
                    ${valor.nombre}
                </td>
                <td style="border-right: 2px solid #eceeef; padding-right: 10px; position: relative;">
                    <i class="icofont icofont-square" style="color:${valor.color};font-size: 176%;"></i>
                </td>
                    <td style="text-align:center">
                    <i class="ti-pencil-alt btn btn-inverse-primary waves-effect waves-light" style="margin-right: 10px;" onclick="InfoPrioridad('${valor.id}', '${valor.nombre}', '${valor.color}')" data-toggle="modal" data-target="#ModalEditarPrioridad"></i>
                    <i class="icofont icofont-ui-delete btn btn-inverse-danger waves-effect waves-light" onclick="eliminarPrioridad(${valor.id})"></i>
                </td>
               
                </tr>
                `
            //< td style = "display: flex; justify-content: center; align-items: center; position: relative;" >
            //    15
            //    < span onclick = "Consultar(${valor.id})" style = "position: absolute; right: 5%;" >
            //        <i class="icofont icofont-eye-alt btn btn-inverse-success waves-effect waves-light"></i>
            //        </span >
            //    </td >
    }

    $('#TPrioridad').DataTable({
        pageLength: 5,
        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, 'Todas']],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
        }
    }).on('init.dt', function () {
        $('.dataTables_filter input').attr('placeholder', 'Empresa Atención');
    });
}


//PROCESOS SOLO PARA DISEÑO BONITO
function obtenerValorParametro(nombreParametro) {
    var urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(nombreParametro);
}
function EmpresaAtencion() {
    $("#EmpresaAtencion").addClass("active");
    $("#EstatusF").removeClass("active");
    $("#EstatusO").removeClass("active");
    $("#Prioridad").removeClass("active");
}
function EstatusFecha() {
    $("#EstatusF").addClass("active");
    $("#EmpresaAtencion").removeClass("active");
    $("#EstatusO").removeClass("active");
    $("#Prioridad").removeClass("active");
}
function EstatusOrden() {
    $("#EstatusO").addClass("active");
    $("#EstatusF").removeClass("active");
    $("#EmpresaAtencion").removeClass("active");
    $("#Prioridad").removeClass("active");
}
function Prioridad() {
    $("#Prioridad").addClass("active");
    $("#EstatusF").removeClass("active");
    $("#EstatusO").removeClass("active");
    $("#EmpresaAtencion").removeClass("active");
}


//VALIDACIONES PARA MODALES 
function ValidarNuevaEmpresaAtencion() {
    var result = false;

    $('#Modal_EmpresaAtencion_Nombre').css("border", "1px solid red");

    if ($('#Modal_EmpresaAtencion_Nombre').val().length > 0) {
        $('#Modal_EmpresaAtencion_Nombre').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_EmpresaAtencion_Nombre').val().length > 0) {
        result = true;
    }

    return result;
}
function ValidarEditarEmpresaAtencion() {
    var result = false;

    $('#Modal_EmpresaAtención_NombreEditar').css("border", "1px solid red");

    if ($('#Modal_EmpresaAtención_NombreEditar').val().length > 0) {
        $('#Modal_EmpresaAtención_NombreEditar').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_EmpresaAtención_NombreEditar').val().length > 0) {
        result = true;
    }

    return result;
}

function ValidarNuevoEstatusFecha() {
    var result = false;

    $('#Modal_EstatusFecha_Nombre').css("border", "1px solid red");

    if ($('#Modal_EstatusFecha_Nombre').val().length > 0) {
        $('#Modal_EstatusFecha_Nombre').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_EstatusFecha_Nombre').val().length > 0) {
        result = true;
    }

    return result;
}
function ValidarEditarFechaAtencion() {
    var result = false;

    $('#Modal_EstatusFecha_NombreEditar').css("border", "1px solid red");

    if ($('#Modal_EstatusFecha_NombreEditar').val().length > 0) {
        $('#Modal_EstatusFecha_NombreEditar').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_EstatusFecha_NombreEditar').val().length > 0) {
        result = true;
    }

    return result;
}

function ValidarNuevoEstatusOrdenTrabajo() {
    var result = false;

    $('#Modal_EstatusOrdenTrabajo_Nombre').css("border", "1px solid red");

    if ($('#Modal_EstatusOrdenTrabajo_Nombre').val().length > 0) {
        $('#Modal_EstatusOrdenTrabajo_Nombre').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_EstatusOrdenTrabajo_Nombre').val().length > 0) {
        result = true;
    }

    return result;
}
function ValidarEditarOrdenTrabajo() {
    var result = false;

    $('#Modal_EstatusOrdenTrabajo_NombreEditar').css("border", "1px solid red");

    if ($('#Modal_EstatusOrdenTrabajo_NombreEditar').val().length > 0) {
        $('#Modal_EstatusOrdenTrabajo_NombreEditar').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_EstatusOrdenTrabajo_NombreEditar').val().length > 0) {
        result = true;
    }

    return result;
}

function ValidarNuevaPrioridad() {
    var result = false;

    $('#Modal_Prioridad_Nombre').css("border", "1px solid red");

    if ($('#Modal_Prioridad_Nombre').val().length > 0) {
        $('#Modal_Prioridad_Nombre').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_Prioridad_Nombre').val().length > 0) {
        result = true;
    }

    return result;
}
function ValidarEditarPrioridad() {
    var result = false;

    $('#Modal_Prioridad_NombreEditar').css("border", "1px solid red");

    if ($('#Modal_Prioridad_NombreEditar').val().length > 0) {
        $('#Modal_Prioridad_NombreEditar').css("border", "1px solid rgba(0, 0, 0, .15)");
    }

    if ($('#Modal_Prioridad_NombreEditar').val().length > 0) {
        result = true;
    }

    return result;
}





