
$(document).ready(function () {

    $('#Telefono').on('input', function () {
        var numericValue = $(this).val().replace(/\D/g, '');
        numericValue = numericValue.substring(0, 10);
        $(this).val(numericValue);
    });


    $('.letras').on('input', function () {
        var inputValue = $(this).val();
        var lettersAndSpacesOnly = inputValue.replace(/[^A-Za-záéíóúüñ\s]/g, '');
        $(this).val(lettersAndSpacesOnly);
    });

    let Data = $("#selectRol").val();

    if (Data == 1){$("#Cuadrilla").css("display", "block");
    }
});

function Validar() {
    var result = false;

    $('#Nombre, #ApellidoP, #ApellidoM, #Email, #Telefono, #selectRol, #SelectCorreo, #SelectTelefono, #selectCuadrilla').css("border", "1px solid #ff0049");

    //if ($('#Contraseña').val().length > 0) {$('#Contraseña').css("border", "1px solid #cccccc"); }
    //if ($('#Contraseña2').val().length > 0) {$('#Contraseña2').css("border", "1px solid #cccccc"); }
    if ($('#Nombre').val().length > 0) { $('#Nombre').css("border", "1px solid #cccccc"); }
    if ($('#ApellidoP').val().length > 0) { $('#ApellidoP').css("border", "1px solid #cccccc"); }
    if ($('#ApellidoM').val().length > 0) { $('#ApellidoM').css("border", "1px solid #cccccc"); }
    if ($('#Email').val().length > 0) { $('#Email').css("border", "1px solid #cccccc"); }
    if ($('#Telefono').val().length > 0) { $('#Telefono').css("border", "1px solid #cccccc"); }
    if ($("#selectRol option:selected").val() > 0) { $('#selectRol').css("border", "1px solid #cccccc"); }
    if ($("#SelectCorreo option:selected").val() > 0) { $('#SelectCorreo').css("border", "1px solid #cccccc"); }
    if ($("#SelectTelefono option:selected").val() > 0) { $('#SelectTelefono').css("border", "1px solid #cccccc"); }


    var selectRolValue = $("#selectRol option:selected").val();
    var selectCuadrillaValue = $("#selectCuadrilla option:selected").val();

    if (selectRolValue == 1) {
        if (selectCuadrillaValue > 0) { $('#selectCuadrilla').css("border", "1px solid #cccccc"); }
    } else {
        $('#selectCuadrilla').css("border", "1px solid #cccccc");
    }
    result = (
        //$('#Contraseña').val().length > 0 &&
        //$('#Contraseña2').val().length > 0 &&
        $('#Nombre').val().length > 0 &&
        $('#ApellidoP').val().length > 0 &&
        $('#ApellidoM').val().length > 0 &&
        $('#Email').val().length > 0 &&
        $('#Telefono').val().length > 0 &&
        selectRolValue > 0 &&
        $("#SelectCorreo option:selected").val() > 0 &&
        $("#SelectTelefono option:selected").val() > 0 &&
        (selectRolValue != 1 || (selectRolValue == 1 && selectCuadrillaValue > 0))
    );


    return result;

}
function ValidaEmail() {
    var email = $('#Email').val();
    var emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailRegex.test(email);
}

function Guardar() {
    if (Validar()) {

        if (ValidaEmail()) {

            let Cat_Rol = {
                Id: $("#selectRol option:selected").val()
            }
            let Cuadrilla = {
                Id: $("#selectCuadrilla option:selected").val()
            }
            let Usuario = {
                Cat_Rol: Cat_Rol,
                Cuadrilla: Cuadrilla,
                Id: new URLSearchParams(window.location.search).get('Id'),
                Usuario: $('#Email').val()
            }
            let Persona = {
                Nombre: $('#Nombre').val(),
                ApellidoPaterno: $('#ApellidoP').val(),
                ApellidoMaterno: $('#ApellidoM').val()
            }

            let Cat_TipoEmail = {
                Id: $("#SelectCorreo option:selected").val(),
            }
            let Cat_TipoTelefono = {
                Id: $("#SelectTelefono option:selected").val(),
            }

            let Email = {
                Cat_TipoEmail: Cat_TipoEmail,
                Email: $('#Email').val()
            }
            let Telefono = {
                Cat_TipoTelefono: Cat_TipoTelefono,
                Telefono: $('#Telefono').val()
            }

            let _model = {
                Usuario: Usuario,
                Persona: Persona,
                Email: Email,
                Telefono: Telefono
            };

            fetch("/Administracion/Usuario/UpdateUser", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_model)
            })
                .then(res => res.json())
                .then(res => {
                    if (res.id === 1) {
                        swal({
                            title: "Exito",
                            text: "usuario actualizado.",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/update.png",

                            showCancelButton: false,
                            confirmButtonColor: "#3070A9",
                            confirmButtonText: "Aceptar",
                            closeOnConfirm: false,
                        },
                            function (isConfirm) {
                                if (isConfirm) {
                                    location.reload();
                                }
                            });
                    } else if (res.id === 2) {
                        swal({
                            title: "Correo en uso",
                            text: "El correo electrónico ya está en uso. Por favor, elige otro.",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {
                        });
                    }
                    else if (res.id === -1) {
                        swal({
                            title: "Usuario no actualizado",
                            text: "No es posible actualizar el Usuario. !",
                            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                            buttons: true,
                            dangerMode: true,
                        }).then((willDelete) => {
                        });
                    }
                });

        } else {

            swal({
                title: "Correo no valido",
                text: "El correo proporcionado no tiene un formato valido",
                imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                buttons: true,
                dangerMode: true,
            })
        }


    } else {
        swal({
            title: "Datos incompletos",
            text: "Completa los datos solicitados",
            imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
            buttons: true,
            dangerMode: true,
        })
    }
}

function Cuadrilla() {
    var Id = $("#selectRol").val();
    if (Id == 1) {
        $("#Cuadrilla").css("display", "block");
        $("#selectCuadrilla").val('0');

    } else {
        $("#Cuadrilla").css("display", "none");
        $("#selectCuadrilla").val('0');
    }
}