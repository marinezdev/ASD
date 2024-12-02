
$('#TListadoUsuarios').DataTable({
    pageLength: 10,
    lengthMenu: [[10, 20, -1], [10, 20, 'Todas']],
    language: {
        url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/es-MX.json'
    }
}).on('init.dt', function () {

    $('.dataTables_filter input').attr('placeholder', 'Usuarios');
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

                    const persona = {
                        Id: Ids
                    };

                    fetch("/Persona/Persona/DeletePerson", {
                        method: "POST",
                        headers: { "Content-Type": "application/json; charset=utf-8" },
                        body: JSON.stringify(persona)
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

    function NewUser() {
        window.location.href = '/Persona/Persona/Nueva';
    }
    function EditUser(Id) {
        window.location.href = '/Persona/Persona/Editar?Id=' + Id;
}


    $("#MenuUsuario").addClass("active");
    $("#ListaUsuario").addClass("active");


