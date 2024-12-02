$('#MenuOperacion').addClass("active");


function CargaRutinaInical(_model) {
    fetch("/Ticket/TicketEquipoImagen/GetTicketEquipoImagen_Seleccionar_IdTicketEquipo", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_model)
    })
        .then(res => res.json())
        .then(res => {
            
            if (res != null) {
                var html = "";
                var labelEquipoImg = "";

                for (let i = 0; i < res.length; i++) {
                    console.log(res);

                    if (res[i].ticketEquipoImagen != null) {

                        for (let a = 0; a < res[i].ticketEquipoImagen.length; a++) {
                            var imagen = "";
                            if (res[i].ticketEquipoImagen[a] != null) {

                                imagen += `
                                    <div class="col-sm-12 text-right">
                                        <button type='button' class='btn btn-primary waves-effect waves-light' onclick="SubirArchivo(${res[i].ticketEquipoImagen[a].id})"> Capturar imagen</button>
                                        <input id="FileArchivo${res[i].ticketEquipoImagen[a].id}" type="file" onchange="ValidaUploadFile(${res[i].ticketEquipoImagen[a].id}, ${res[i].id})" style="visibility:hidden">
                                    </div>
                                    <div class="col-md-12">
                                        <div id="image${res[i].ticketEquipoImagen[a].id}">
                                    `;

                                if (res[i].ticketEquipoImagen[a].imagen != null) {
                                    imagen += `
                                <div class='card'>
                                    <div class='card-block'>
                                        <div class='row'>
                                            <div class='col-md-12 col-xs-12 text-center'>
                                                <p>Foto antes</p>
                                                <a href='${res[i].ticketEquipoImagen[a].imagen}' data-lightbox='roadtrip'>
                                                    <img src='${res[i].ticketEquipoImagen[a].imagen}' class='img-fluid' alt='' />
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                `;
                                }

                                imagen += `</div>
                                    </div>
                                `;
                            }

                            labelEquipoImg += `
                        <div class="row">
                            <div class="col-md-12">
                                <label class="m-b-10">Imagen: <strong> ${res[i].ticketEquipoImagen[a].cat_EquipoImagen.nombre} </strong></label>
                            </div>
                        </div>
                        <div class="row">
                            ${imagen}
                        </div>
                        `;
                        }
                    }

                    html += `
                <div class="col-sm-12">
                    <div class="panel panel-primary" style="border-color: #2196F3;">
                        <div class="panel-heading bg-primary">
                             ${res[i].equipo.serie}
                        </div>
                        <div class="panel-body" style="padding: 15px;">
                            ${labelEquipoImg}
                        </div>
                        <div class="panel-footer txt-primary">
                             ${res[i].equipo.serie}
                        </div>
                    </div>
                </div>
                `;
                }
                $("#ImagenesEquipoImg").html(html);
            }

        });
}






function SubirArchivo(Id) {
    $("#FileArchivo" + Id).click();
}

function ValidaUploadFile(Id, IdTicket) {

    var fileInput = document.getElementById('FileArchivo' + Id);

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

                UploadFile(Id, IdTicket);

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

function UploadFile(Id, IdTicket) {
    var progressBar = document.getElementById('progressBar');
    var formData = new FormData();
    var fileInput = document.getElementById('FileArchivo' + Id);

    formData.append('file', fileInput.files[0]);
    formData.append('IdTicket', IdTicket);
    formData.append('IdTicketEquipoImagen', Id);

    fetch('/Ticket/TicketEquipoImagen/UpdateTicketEquipoImagen_Actualizar', {
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
                //var Id = response.headers.get('Id');

                $('#image' + Id).html("");
                //// id de la nueva imagen
                CargaImagen(Id);

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
    var src = protocol + "//" + host + "/Ticket/TicketEquipoImagen/ImageView?cont=" + Id;
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

    $('#image'+ Id).append(html);
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
            window.location.href = '/TicketAtencionMovil/CheckList/Index?cont=' + res.url;
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
            window.location.href = '/TicketAtencionMovil/UnidadNegocio/Index?cont=' + res.url;
        });
}





