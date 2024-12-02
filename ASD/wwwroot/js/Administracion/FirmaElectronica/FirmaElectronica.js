
let signaturePad;

$(document).ready(function () {
    const canvas = $("#signature-pad");
    canvas.attr("width", 569);
    signaturePad = new SignaturePad(canvas[0]);

    $(window).resize(obtenerDimensionesModal);

    $("#colorPicker").on("input", function () {
        const selectedColor = $(this).val();
        signaturePad.penColor = selectedColor;
    });
});

function NuevaFirma() {
    if (obtenerTipoDispositivo()) {

        $('#ModalFirma').modal('show');
    }
}
function LimpiarFirma() {
    signaturePad.clear();
}
function saveSignature() {
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

        toastr.error("Por favor, proporcione una firma primero.", "Notificación");
    } else {
        var signatureData = signaturePad.toDataURL();

        const firmaElectronica = {
            Nombre: signatureData
        };
        fetch("/Administracion/FirmaElectronica/CrearFirma", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(firmaElectronica)
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
                                $('#ModalFirma').modal('hide');

                                var imgElement = document.getElementById("imagenFirma");
                                imgElement.src = signatureData;
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
                        });
                }
            });
    }

}
function atras() {
    const data = signaturePad.toData();
    if (data) {
        data.pop();
        signaturePad.fromData(data);
    }
}

function obtenerDimensionesModal() {
    var canvas = $("#signature-pad");
    var modal = $("#ModalFirmaSize");

    var anchoModal = modal.width();
    canvas.attr("width", anchoModal - 29);

    signaturePad.fromData(signaturePad.toData());
}
function obtenerTipoDispositivo() {
    const userAgent = navigator.userAgent;
    let tipoDispositivo;

    if (/iPad/i.test(userAgent)) {
        tipoDispositivo = 'tablet';
    } else if (/Android|webOS|iPhone|iPod|BlackBerry|IEMobile|Opera Mini/i.test(userAgent)) {
        tipoDispositivo = 'movil';
        alert('Este sitio web se ve mejor en modo horizontal. Por favor, gire su dispositivo.');
    } else {
        tipoDispositivo = 'pc';
    }
    return tipoDispositivo;
}


    //OPCION #2
    //$(document).ready(function () {
    //    const canvas = $("#signature-pad");
    //    //canvas.attr("width", 569);
    //    signaturePad = new SignaturePad(canvas[0]);

    //    window.onresize = resizeCanvas;
    //    resizeCanvas();
    //});

    //function resizeCanvas() {
    //    const ratio = Math.max(window.devicePixelRatio || 1, 1);
    //    canvas.width = canvas.offsetWidth * ratio;
    //    canvas.height = canvas.offsetHeight * ratio;
    //    canvas.getContext("2d").scale(ratio, ratio);
    //    signaturePad.fromData(signaturePad.toData());

    //    var canvas = $("#signature-pad");
    //    var modal = $("#ModalFirmaSize");

    //    var anchoModal = modal.width();
    //    canvas.attr("width", anchoModal - 29);
    //}

