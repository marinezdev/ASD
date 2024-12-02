$('#MenuOperacion').addClass("active");


/***************************************************************************************************/
/***************************************************************************************************/

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
                "1": $("#VideoRecorridoAntes"),
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
                "Tienda/Sucursal": $("#image1"),
                "Mostrador": $("#image2"),
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

/***************************************************************************************************/
/***************************************************************************************************/

function SubirArchivo() {
    $("#FileArchivoFachada").click();
}

function ValidaUploadFile(Id) {
    var fileInput = document.getElementById('FileArchivoFachada');

    if (fileInput.files.length > 0) {
      
        for (i = 0; i < fileInput.files.length; i++) {
            var Formato = "";
            var Carga = false;
            var ext = fileInput.files[i].name.split('.').pop();
            Extencion = fileInput.files[i].name.split('.').pop();

            // Convertimos en minúscula porque
            // la extensión del archivo puede estar en mayúscula
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
    var fileInput = document.getElementById('FileArchivoFachada');

    formData.append('file', fileInput.files[0]);
    formData.append('IdTicket', Id);
    // Valor definido en la base de datos
    formData.append('Idtipo', 1);

    fetch('/Ticket/SucursalImg/CreateSucursalImg', {
        method: 'POST',
        body: formData,
        headers: {
            // Puedes necesitar configurar los encabezados según tu API
        },
        // Importante: Desactiva el caché para evitar problemas de lectura de FormData
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

                $('#image1').html("");
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
        /*console.log('Éxito:', data);*/
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
                                "<a href='"+ src +"' data-lightbox='roadtrip'>" +
                                    "<img src='"+ src +"' class='img-fluid' alt='' />" +
                                "</a>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                "</div>";

    $('#image1').append(html);
}

function SubirArchivoMostrador() {
    $("#FileArchivoMostrador").click();
}

function ValidaUploadFile2(Id) {
    var fileInput = document.getElementById('FileArchivoMostrador');

    if (fileInput.files.length > 0) {

        for (i = 0; i < fileInput.files.length; i++) {
            var Formato = "";
            var Carga = false;
            var ext = fileInput.files[i].name.split('.').pop();
            Extencion = fileInput.files[i].name.split('.').pop();

            // Convertimos en minúscula porque
            // la extensión del archivo puede estar en mayúscula
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

                UploadFile2(Id);

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

function UploadFile2(Id) {
    var progressBar = document.getElementById('progressBar');
    var formData = new FormData();
    var fileInput = document.getElementById('FileArchivoMostrador');

    formData.append('file', fileInput.files[0]);
    formData.append('IdTicket', Id);
    // Valor definido en la base de datos
    formData.append('Idtipo', 2);

    fetch('/Ticket/SucursalImg/CreateSucursalImg', {
        method: 'POST',
        body: formData,
        headers: {
            // Puedes necesitar configurar los encabezados según tu API
        },
        // Importante: Desactiva el caché para evitar problemas de lectura de FormData
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

                    $('#image2').html("");
                    // id de la nueva imagen
                    CargaImagen2(Id);
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
            /*console.log('Éxito:', data);*/
        })
        .catch(error => {
            /* console.error('Error:', error);*/
        });
}

function CargaImagen2(Id) {

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

    $('#image2').append(html);
}


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

            // Convertimos en minúscula porque
            // la extensión del archivo puede estar en mayúscula
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
    formData.append('Idtipo', 1);

    fetch('/Ticket/SucursalVideo/CreateSucursalVideo', {
        method: 'POST',
        body: formData,
        headers: {
            // Puedes necesitar configurar los encabezados según tu API
        },
        // Importante: Desactiva el caché para evitar problemas de lectura de FormData
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

                    $('#VideoRecorridoAntes').html("");
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
            /*console.log('Éxito:', data);*/
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

    $('#VideoRecorridoAntes').append(html);
}

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
            window.location.href = '/TicketAtencionMovil/FotoAntes/Index?cont=' + res.url;
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
            window.location.href = '/TicketAtencionMovil/Informacion/Index?cont=' + res.url;
        });
}


$(document).on('click', '[data-toggle="lightbox"]', function(event) {
    event.preventDefault();
    $(this).ekkoLightbox();
});


