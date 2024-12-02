$('#MenuOperacion').addClass("active");
function Siguiente(Id) {
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
            window.location.href = '/TicketAtencionMovil/Cierre/Index?cont=' + res.url;
        });
}
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
            window.location.href = '/TicketAtencionMovil/InformacionAdicional/Index?cont=' + res.url;
        });
}


let signaturePad;
$(document).ready(function () {
    const canvas = $("#FirmaAtencion");
    signaturePad = new SignaturePad(canvas[0]);
    $(window).resize(obtenerDimensionesModal);
    $("#colorPicker").on("input", function () {
        const selectedColor = $(this).val();
        signaturePad.penColor = selectedColor;
    });
    obtenerDimensionesModal()
});
function obtenerDimensionesModal() {
    var canvas = $("#FirmaAtencion");
    var Contenedor = $("#ContenedorFirma");

    var Contenedor = Contenedor.width();
    canvas.attr("width", Contenedor);
    signaturePad.fromData(signaturePad.toData());
}
function LimpiarFirma() {
    signaturePad.clear();
}
function atras() {
    const data = signaturePad.toData();
    if (data) {
        data.pop();
        signaturePad.fromData(data);
    }
}
function Validar() {
    var result = false;
    $('#Pregunta1, #Pregunta2,#Pregunta3,#Nombre,Observaciones').css("border", "1px solid #ff0049");
    if ($("#Pregunta1 option:selected").val() > 0) { $('#Pregunta1').css("border", "1px solid #cccccc"); }
    if ($("#Pregunta2 option:selected").val() > 0) { $('#Pregunta2').css("border", "1px solid #cccccc"); }
    if ($("#Pregunta3 option:selected").val() > 0) { $('#Pregunta3').css("border", "1px solid #cccccc"); }
    if ($('#Nombre').val().length > 0) { $('#Nombre').css("border", "1px solid #cccccc"); }

    result = (
        $("#Pregunta1 option:selected").val() > 0 &&
        $("#Pregunta2 option:selected").val() > 0 &&
        $("#Pregunta3 option:selected").val() > 0 &&
        $("#Nombre").val().length >0
    );
    return result;
}
function saveSignature(Id) {
    if (Validar()) {
        try {
            if (signaturePad.isEmpty()) {
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

                toastr.error("Por favor, proporcione una firma primero.", "Notificacion");
            }
            else {
                const CierreCliente = {
                    IdTicket: Id,
                    Pregunta1: $("#Pregunta1 option:selected").val(),
                    Pregunta2: $("#Pregunta2 option:selected").val(),
                    Pregunta3: $("#Pregunta3 option:selected").val(),
                    Nombre: $("#Nombre").val(),
                    Firma: signaturePad.toDataURL(),
                    Observaciones: $("#Observaciones").val()
                };
                fetch("/TicketAtencionMovil/CierreCliente/Create_CierreCliente", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(CierreCliente)
                })
                    .then(response => response.json())
                    .then(response => {
                        if (response.id === 1) {
                            Siguiente(Id) 
                        }
                        else {
                            swal({
                                title: "Error",
                                text: "No es posible realizar la operaci�n seleccionada!",
                                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                                buttons: true,
                                dangerMode: true,
                            })
                                .then(willDelete => {
                                });
                        }
                    });
            }
        } catch {
            const CierreCliente = {
                IdTicket: Id,
                Pregunta1: $("#Pregunta1 option:selected").val(),
                Pregunta2: $("#Pregunta2 option:selected").val(),
                Pregunta3: $("#Pregunta3 option:selected").val(),
                Nombre: $("#Nombre").val(),
                Observaciones: $("#Observaciones").val()
            };
            fetch("/TicketAtencionMovil/CierreCliente/Create_CierreCliente", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(CierreCliente)
            })
                .then(response => response.json())
                .then(response => {
                    if (response.id === 1) {
                        Siguiente(Id) 
                    }
                    else {
                        swal({
                            title: "Error",
                            text: "No es posible realizar la operaci�n seleccionada!",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        })
                            .then(willDelete => {
                            });
                    }
                });
        }
       
    }
    else {
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

        toastr.error("Por favor, proporcione la información solicitada.", "Notificación");
    }
}
function EliminarFirma() {
    const CierreCliente = {
        IdTicket: new URLSearchParams(window.location.search).get('cont')
    };
    fetch("/TicketAtencionMovil/CierreCliente/Delete_CierreClienteFirma", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(CierreCliente)
    })
        .then(response => response.json())
        .then(response => {
            if (response.id === 1) {
                location.reload();
            }
            else {
                swal({
                    title: "Error",
                    text: "No es posible realizar la operaci�n seleccionada!",
                    imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                    buttons: true,
                    dangerMode: true,
                })
                    .then(willDelete => {
                    });
            }
        });
}
