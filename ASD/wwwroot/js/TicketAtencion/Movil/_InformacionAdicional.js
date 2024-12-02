$('#MenuOperacion').addClass("active");




function CargaVideoInicial(_model) {

    fetch("/Ticket/SucursalVideo/GetSucursalVideo_Seleccionar_IdTikcet", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_model)
    })
    .then(res => res.json())
    .then(res => {

        if (res != null) {
            const videoData = res.filter(item => item.cat_SucursalVideo);
            const videoMap = {
                "2": $("#VideoRecorridoDespues"),
            };

            for (const item of videoData) {
                const videoContainer = videoMap[item.cat_SucursalVideo.id];
                if (videoContainer) {
                    videoContainer.html(`
                <div class="card">
                    <div class="card-block text-center">
                        <div class="row">
                            <div class="col-sm-12 text-center">
                                <video width="100%" controls >
                                    <source src="${item.nmArchivo}" type="video/mp4">
                                </video>
                            </div>
                        </div>
                    </div>
                </div>
            `);
                }
            }
        }
    });
}

function CargaImagenesInicial(_model) {

    fetch("/Ticket/SucursalImg/GetSucursalImg_Seleccionar_IdTicket", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_model)
    })
        .then(res => res.json())
        .then(res => {
            if (res != null) {
                const imageData = res.filter(item => item.cat_SucursalImagen);
                const imageMap = {
                    "Orden de trabajo": $("#imagenOrdenTrabajo")
                };

                for (const item of imageData) {
                    const imageContainer = imageMap[item.cat_SucursalImagen.nombre];
                    if (imageContainer) {
                        imageContainer.html(`
                        <div class="card">
                            <div class="card-block text-center">
                                <div class="row">
                                    <div class="col-sm-12 text-center">
                                        <a href="${item.nmArchivo}" data-lightbox="roadtrip" style="text-align:center">
                                            <img src="${item.nmArchivo}" class="img-fluid" alt="" width="40%" />
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `);
                    }
                }
            }
        });
}



/********************************************************************************************************************** */
/********************************************************************************************************************** */


function SubirArchivo() {
    $("#FileArchivoOrdenTrabajo").click();
}

function ValidaUploadFile(Id) {
    var fileInput = document.getElementById('FileArchivoOrdenTrabajo');

    if (fileInput.files.length > 0) {

        for (i = 0; i < fileInput.files.length; i++) {
            var Formato = "";
            var Carga = false;
            var ext = fileInput.files[i].name.split('.').pop();
            Extencion = fileInput.files[i].name.split('.').pop();

            // Convertimos en min�scula porque
            // la extensi�n del archivo puede estar en may�scula
            ext = ext.toLowerCase();

            if (ext == 'png' || ext == 'jpg' || ext == 'jpeg') {
                Carga = true;
            } else {
                Carga = false;
                Formato = ext;
                /*break;*/
            }

            if (Carga) {
                var Div = "<div class='col-md-12 col-lg-12'>" +
                            "<progress id='progressBar' style='width:100%; height: 30px;' value='0' max='100'></progress>" +
                          "</div>";

                $("#DivCargaArchivo").html(Div);
                $("#ModalCargaArchivo").modal("show");

                UploadFile(Id);

            } else {
                toastr.options = {
                    "closeButton": true,
                    "progressBar": true,
                    "positionClass": "toast-top-right",
                    "showDuration": "500",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "iconClass": "toast-error"
                };

                toastr.error("Intentas cargar un formato no permitido: <strong>" + Formato + "</strong> Formato de archivo permitido: <strong>.png, .jpg, jpeg </strong>", "Notificacion");
            }
        }
    }
}

function UploadFile(Id) {

    var progressBar = document.getElementById('progressBar');
    var formData = new FormData();
    var fileInput = document.getElementById('FileArchivoOrdenTrabajo');

    formData.append('file', fileInput.files[0]);
    formData.append('IdTicket', Id);
    // Valor definido en la base de datos
    formData.append('Idtipo', 3);

    fetch('/Ticket/SucursalImg/CreateSucursalImg', {
        method: 'POST',
        body: formData,
        headers: {
            // Puedes necesitar configurar los encabezados seg�n tu API
        },
        // Importante: Desactiva el cach� para evitar problemas de lectura de FormData
        cache: 'no-cache',
    })
    .then(response => {

        var total = response.headers.get('Content-Length');
        var reader = response.body.getReader();
        var loaded = 0;

        return reader.read().then(function process(result) {
            if (result.done) {

                $("#BtnCargaArchivo").click();

                // Carga completada
                console.log('Carga completada');
                var Id = response.headers.get('Id');

                $('#imagenOrdenTrabajo').html("");
                // id de la nueva imagen
                CargaImagen(Id);
                //console.log(Id);
                //console.log("id del ticket: " + Id)

                return;
            }

            loaded += result.value.length;

            progressBar.value = (loaded / total) * 100;

            // Continuar leyendo el siguiente fragmento del cuerpo de la respuesta
            return reader.read().then(process);
        });
    })
    .then(data => {
        /*console.log('�xito:', data);*/
    })
    .catch(error => {
        /* console.error('Error:', error);*/
    });
}

function CargaImagen(Id) {

    var protocol = window.location.protocol;
    var host = window.location.host;

    var image = new Image();
    var src = protocol + "//" + host + "/Ticket/SucursalImg/ImageView?cont=" + Id;
    image.src = src;


    var html = "<div class='card'>" +
        "<div class='card-block'>" +
        "<div class='row'>" +
        "<div class='col-xl-2 col-lg-3 col-sm-3 col-xs-12'>" +
        "<a href='" + src + "' data-lightbox='roadtrip'>" +
        "<img src='" + src + "' class='img-fluid' alt='' />" +
        "</a>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>";

    $('#imagenOrdenTrabajo').append(html);
}


/*********************************************************** */
/*********************************************************** */

function SubirArchivoAdicional() {
    $("#FileArchivo").click();
}

function ValidaUploadFileAdicional(Id) {
    var fileInput = document.getElementById('FileArchivo');

    if (fileInput.files.length > 0) {

        for (i = 0; i < fileInput.files.length; i++) {
            var Formato = "";
            var Carga = false;
            var ext = fileInput.files[i].name.split('.').pop();
            Extencion = fileInput.files[i].name.split('.').pop();

            // Convertimos en min�scula porque
            // la extensi�n del archivo puede estar en may�scula
            ext = ext.toLowerCase();

            if (ext == 'png' || ext == 'jpg' || ext == 'jpeg' || ext == 'docx' || ext == 'xlsx' || ext == 'rar' || ext == 'pdf') {
                Carga = true;
            } else {
                Carga = false;
                Formato = ext;
                /*break;*/
            }

            if (Carga) {
                var Div = "<div class='col-md-12 col-lg-12'>" +
                    "<progress id='progressBar' style='width:100%; height: 30px;' value='0' max='100'></progress>" +
                    "</div>";

                $("#DivCargaArchivo").html(Div);
                $("#ModalCargaArchivo").modal("show");

                UploadFileAdicional(Id);

            } else {
                toastr.options = {
                    "closeButton": true,
                    "progressBar": true,
                    "positionClass": "toast-top-right",
                    "showDuration": "500",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "iconClass": "toast-error"
                };

                toastr.error("Intentas cargar un formato no permitido: <strong>" + Formato + "</strong> Formato de archivo permitido: <strong>.png, .jpg, .jpeg, .docx, .xlsx, .rar, .pdf  </strong>", "Notificacion");
            }
        }
    }
}

function UploadFileAdicional(Id) {

    var progressBar = document.getElementById('progressBar');
    var formData = new FormData();
    var fileInput = document.getElementById('FileArchivo');

    formData.append('file', fileInput.files[0]);
    formData.append('IdTicket', Id);

    fetch('/Ticket/Archivo/CreateArchivo', {
        method: 'POST',
        body: formData,
        headers: {
            // Puedes necesitar configurar los encabezados seg�n tu API
        },
        // Importante: Desactiva el cach� para evitar problemas de lectura de FormData
        cache: 'no-cache',
    })
    .then(response => {

        var total = response.headers.get('Content-Length');
        var reader = response.body.getReader();
        var loaded = 0;

        return reader.read().then(function process(result) {
            if (result.done) {

                $("#BtnCargaArchivo").click();

                // Carga completada
                /* console.log('Carga completada');*/
                $('#ListArchivos').html("");
                // id de la nueva imagen
                CargaArchivos(Id);

                return;
            }

            loaded += result.value.length;

            progressBar.value = (loaded / total) * 100;

            // Continuar leyendo el siguiente fragmento del cuerpo de la respuesta
            return reader.read().then(process);
        });
    })
    .then(data => {
        /*console.log('�xito:', data);*/
    })
    .catch(error => {
        /* console.error('Error:', error);*/
    });
}

function CargaArchivos(Id) {

    const _modelo = {
        Id: Id,
    };

    fetch("/Ticket/Archivo/GetArchivo_Seleccionar_IdTicket", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        var TableHeader = "<div class='col-sm-12 table-responsive'>" +
                            "<table class='table text-center'>" +
                                "<thead>" +
                                    "<tr>" +
                                        "<th class='text-center'>Archivo</th>" +
                                        "<th class='text-center'></th>" +
                                    "</tr>" +
                                "</thead>" +
                                "<tbody>";


        var TableFooter =       "</tbody>" +
                            "</table>" +
                         "</div>";

        var Tabla = "";
        if (res === null) {
            Tabla = "<tr>" +
                        "<td>" +
                        "</td>" +
                        "<td>" +
                        "</td>" +
                    "</tr>";
        } else if (res.length > 0) {
            $(res).each(function () {
                Tabla += "<tr>" +
                    "<td>" +
                    this.nmOriginal.toUpperCase() +
                    "</td>" +
                    "<td>" +
                    "<button type='button' class='btn btn-primary waves-effect waves-light' data-original-title='.icofont-home' onclick='EliminarArchivo(" + this.id + "," + Id +")'>" +
                    "<i class='icofont icofont-trash'></i>" +
                    "</button>" +
                    "</td>" +
                    "</tr>";
            });
        }

        $("#ListArchivos").html(TableHeader + Tabla + TableFooter);

    });
}

function EliminarArchivo(Id, IdTicket) {
    const _modelo = {
        Id: Id,
    };

    fetch("/Ticket/Archivo/DeleteArchivo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {

        if (res.id > 0) {
            CargaArchivos(IdTicket);

        } else {
            toastr.options = {
                "closeButton": true,
                "progressBar": true,
                "positionClass": "toast-top-right",
                "showDuration": "500",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "iconClass": "toast-error"
            };

            toastr.error("Archivo no eliminado ", "Notificacion");
        }

    });
}

/*********************************************************** */
/*********************************************************** */

function SubirArchivoVideo() {
    $("#FileArchivoVideo").click();
}
function ValidaUploadFile3(Id) {
    var fileInput = document.getElementById('FileArchivoVideo');

    if (fileInput.files.length > 0) {

        for (i = 0; i < fileInput.files.length; i++) {
            var Formato = "";
            var Carga = false;
            var ext = fileInput.files[i].name.split('.').pop();
            Extencion = fileInput.files[i].name.split('.').pop();

            // Convertimos en min�scula porque
            // la extensi�n del archivo puede estar en may�scula
            ext = ext.toLowerCase();

            if (ext == 'mp4' || ext == 'mov' || ext == 'wmv' || ext == 'avi') {
                Carga = true;
            } else {
                Carga = false;
                Formato = ext;
                /*break;*/
            }

            if (Carga) {
                var Div = "<div class='col-md-12 col-lg-12'>" +
                    "<progress id='progressBar' style='width:100%; height: 30px;' value='0' max='100'></progress>" +
                    "</div>";

                $("#DivCargaArchivo").html(Div);
                $("#ModalCargaArchivo").modal("show");

                UploadFile3(Id);

            } else {
                toastr.options = {
                    "closeButton": true,
                    "progressBar": true,
                    "positionClass": "toast-top-right",
                    "showDuration": "500",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "iconClass": "toast-error"
                };

                toastr.error("Intentas cargar un formato no permitido: <strong>" + Formato + "</strong> Formato de archivo permitido: <strong>.mp4, .mov, .wmv, .avi </strong>", "Notificacion");
            }
        }
    }
}

function UploadFile3(Id) {

    var progressBar = document.getElementById('progressBar');
    var formData = new FormData();
    var fileInput = document.getElementById('FileArchivoVideo');

    formData.append('file', fileInput.files[0]);
    formData.append('IdTicket', Id);
    // Valor definido en la base de datos
    formData.append('Idtipo', 2);

    fetch('/Ticket/SucursalVideo/CreateSucursalVideo', {
        method: 'POST',
        body: formData,
        headers: {
            // Puedes necesitar configurar los encabezados seg�n tu API
        },
        // Importante: Desactiva el cach� para evitar problemas de lectura de FormData
        cache: 'no-cache',
    })
        .then(response => {

            var total = response.headers.get('Content-Length');
            var reader = response.body.getReader();
            var loaded = 0;

            return reader.read().then(function process(result) {
                if (result.done) {


                    $("#BtnCargaArchivo").click();

                    // Carga completada
                    console.log('Carga completada');
                    var Id = response.headers.get('Id');

                    $('#VideoRecorridoDespues').html("");
                    // id de la nueva imagen
                    CargaVideo(Id);
                    //console.log(Id);
                    //console.log("id del ticket: " + Id)

                    return;
                }

                loaded += result.value.length;

                progressBar.value = (loaded / total) * 100;

                // Continuar leyendo el siguiente fragmento del cuerpo de la respuesta
                return reader.read().then(process);
            });
        })
        .then(data => {
            /*console.log('�xito:', data);*/
        })
        .catch(error => {
            /* console.error('Error:', error);*/
        });
}

function CargaVideo(Id) {

    var protocol = window.location.protocol;
    var host = window.location.host;

    var image = new Image();
    var src = protocol + "//" + host + "/Ticket/SucursalVideo/VideoView?cont=" + Id;
    image.src = src;

    var html = "<div class='card'>" +
        "<div class='card-block'>" +
        "<div class='row'>" +
        "<div class='col-sm-12 col-xs-12'>" +
        "<video width='100%' controls >" +
        "<source src='" + src + "' type='video/mp4'>" +
        "</video>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>";

    $('#VideoRecorridoDespues').append(html);
}


/*********************************************************** */
/*********************************************************** */

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
            window.location.href = '/TicketAtencionMovil/CierreCliente/Index?cont=' + res.url;
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
            window.location.href = '/TicketAtencionMovil/FotoDespues/Index?cont=' + res.url;
        });
}
/*********************************************************** */
/**********************PROCESOS FIRMA************************************* */
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
function saveSignature(Id) {
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

                toastr.error("Por favor, proporcione una firma primero.", "Notificación");
            }
            else {
                const InformacionAdicional = {
                    //IdTicket: new URLSearchParams(window.location.search).get('cont'),
                    IdTicket: Id,
                    Firma: signaturePad.toDataURL()
                };
                fetch("/Administracion/FirmaElectronica/Create_Signature", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(InformacionAdicional)
                })
                    .then(response => response.json())
                    .then(response => {
                        if (response.id === 1) {
                            Siguiente(Id);
                        }
                        else {
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
        } catch {
            Siguiente(Id);
        }
}
function EliminarFirma(Id) {
    const CierreCliente = {
        IdTicket: Id
    };
    fetch("/Administracion/FirmaElectronica/Delete_Firma", {
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