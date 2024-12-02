$('#MenuOperacion').addClass("active");


const jsConfetti = new JSConfetti();
document.querySelector("#congrats")
    .addEventListener("click", (e) => {
        jsConfetti.addConfetti();
    });

function Anterior(Id) {
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
            window.location.href = '/TicketAtencionMovil/CierreCliente/Index?cont=' + res.url;
        });
}

function Finalizar(Id) {
    const Ticket = {
        Id: Id
    }

    const _modelo = {
        Ticket: Ticket,
    };

    fetch("/Ticket/TicketCuadrilla/CreateTicketCuadrilla_FinalizarTicket", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {

        if (res.id > 0) {

            swal({
                title: "Ticket atendido.",
                imageUrl: "https://asddemo.asae.com.mx/icons/Finalizar.png",
                showCancelButton: false,
                confirmButtonColor: "#3070A9",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false,
            },
                function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = '/Ticket/Ticket/OperacionUsuario';
                    }
                }
            );

            jsConfetti.addConfetti();
            jsConfetti.addConfetti();

        } else {
            toastr.options = {
                "closeButton": true,
                "progressBar": true,
                "positionClass": "toast-top-right",
                "showDuration": "500",
                "hideDuration": "1000",
                "timeOut": "6000",
                "extendedTimeOut": "1000",
                "iconClass": "toast-error"
            };

            toastr.error("Por el momento no es posible finalizar el ticket", "Notificacion");
        }
    });
}