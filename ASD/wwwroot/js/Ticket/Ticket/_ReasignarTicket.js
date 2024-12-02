$('#MenuOperacion').addClass("active");
$('#MenuOperacionReasignar').addClass("active");
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

function CambioCuadrillaTicket(Id) {

    const _modelo = {
        Id: $("#Ticket_Cuadrilla" + Id + " option:selected").val(),
    };

    fetch("/Empresa/CuadrillaUsuario/GetCuadrillaUsuario_Seleccionar_IdCuadrilla", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        $("#Ticket_UsuarioAtencion" + Id).empty();

        var option = $(document.createElement('option'));
        option.text("Seleccionar");
        option.val(0);

        $("#Ticket_UsuarioAtencion" + Id).append(option);

        if (res === null) {

        } else if (res.length > 0) {

            $(res).each(function () {
                var option = $(document.createElement('option'));

                option.text(this.persona.nombre.toUpperCase() + " " + this.persona.apellidoPaterno.toUpperCase() + " " + this.persona.apellidoMaterno.toUpperCase());
                option.val(this.usuario.id);

                $("#Ticket_UsuarioAtencion" + Id).append(option);
            });
        }

    });
}

function CambioUsuarioAtencion(Id) {

    $("#TituloCuadrilla").text($("#Ticket_Cuadrilla" + Id + " option:selected").text());
    $("#TituloCuadrillaUsaurio").text($("#Ticket_UsuarioAtencion" + Id + " option:selected").text());

    var Div = "" +
        "<div class='row'>" +
            "<div class='col-xl-6'>" +
                "<button type='button' class='btn btn-default waves-effect waves-light col-xl-12' data-dismiss='modal' onclick='CancelarCuadrilla(" + Id + ")'><i class='icofont icofont-close-squared-alt'></i><span class='m-l-12'> Cancelar  </span></button>" +
            "</div>" +
            "<div class='col-xl-6'>" +
                "<button type='button' class='btn btn-primary waves-effect waves-light col-xl-12' data-original-title='.icofont-home' onclick='AsignarCuadrilla(" + Id + ")'><i class='icofont icofont-envelope'></i><span class='m-l-12'> Asignar  </span></button>" +
            "</div>" +
        "</div>";

    $("#DivAsignacionTicket").html(Div);
    $("#ModalAsignarTicket").modal({
        backdrop: "static"
    });
}

function AsignarCuadrilla(Id) {

    var Notificar = 0;
    if ($('#Modal_NAsignacion_Notificar').is(':checked')) {
        Notificar = 1;
    }

    const Ticket = {
        Id: Id
    }

    const Cuadrilla = {
        Id: $("#Ticket_Cuadrilla" + Id + " option:selected").val()
    }

    const Usuario = {
        Id: $("#Ticket_UsuarioAtencion" + Id + " option:selected").val()
    }

    const _modelo = {
        Ticket: Ticket,
        Cuadrilla: Cuadrilla,
        Usuario: Usuario,
        Observaciones: $("#Modal_NAsignacion_Observaciones").val(),
        Notificar: Notificar
    };
    

    fetch("/Ticket/TicketCuadrilla/UpdateTicketCuadrilla", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {

        console.log(res);

        if (res.ticket.id > 0) {

            CargaNuevoEstatusTicket(Id);

            $("#ModalAsignarTicket").modal("hide");

            swal({
                title: "Ticket reasignado.",
                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",
                showCancelButton: false,
                confirmButtonColor: "#3070A9",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
            },
                function (isConfirm) {
                    if (isConfirm) {
                        swal.close();

                        $('#Modal_NAsignacion_Observaciones').val("");

                    }
                }
            );

        } else {

            CancelarCuadrilla(Id);
        }

    });
}

function CargaNuevoEstatusTicket(Id) {

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

        console.log(res);

        const select = document.querySelector("#Ticket_EstatusTicket" + Id);
        select.value = res.ticket.cat_EstatusTicket.id;

     
        if (res.ticket.cat_EstatusTicket.nombre == "Programado") {
            $("#Ticket_Cuadrilla" + Id + " ").prop('disabled', true);
            $("#Ticket_UsuarioAtencion" + Id + " ").prop('disabled', true);
        } else {
            $("#Ticket_Cuadrilla" + Id + " ").prop('disabled', false);
            $("#Ticket_UsuarioAtencion" + Id + " ").prop('disabled', false);
        }

    });
}

function CancelarCuadrilla(Id) {

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
          const select = document.querySelector("#Ticket_Cuadrilla" + Id);
          const selectU = document.querySelector("#Ticket_UsuarioAtencion" + Id);
         
        if (res.cuadrilla == null) {
            select.options[0].selected = true;
            selectU.options[0].selected = true;

        } else {
            select.value = res.cuadrilla.id; 

            const _modelo = {
                Id: res.cuadrilla.id,
            };

            fetch("/Empresa/CuadrillaUsuario/GetCuadrillaUsuario_Seleccionar_IdCuadrilla", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_modelo)
            })
            .then(res2 => res2.json())
            .then(res2 => {
                $("#Ticket_UsuarioAtencion" + Id).empty();

                var option = $(document.createElement('option'));
                option.text("Seleccionar");
                option.val(0);

                $("#Ticket_UsuarioAtencion" + Id).append(option);

                if (res2 === null) {

                } else if (res2.length > 0) {

                    $(res2).each(function () {
                        var option = $(document.createElement('option'));

                        option.text(this.persona.nombre.toUpperCase() + " " + this.persona.apellidoPaterno.toUpperCase() + " " + this.persona.apellidoMaterno.toUpperCase());
                        option.val(this.usuario.id);

                        $("#Ticket_UsuarioAtencion" + Id).append(option);
                    });

                    selectU.value = res.cuadrillaUsuario.usuario.id; 
                }

            });
        }
    });
}
