
let passwor = document.getElementById('Contraseña');
let passwordStrenght = document.getElementById('password-strenght');
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


    $('.eye-icon').on('click', function () {
        var passwordInput = $('#Contraseña');
        var currentType = passwordInput.attr('type');
        if (currentType === 'password') {
            passwordInput.attr('type', 'text');
            $(this).html('<i class="icofont icofont-eye"></i>');
        } else {
            passwordInput.attr('type', 'password');
            $(this).html('<i class="icofont icofont-eye-blocked"></i>');
        }
    });

    $('.eye-icon2').on('click', function () {
        var passwordInput = $('#Contraseña2');
        var currentType = passwordInput.attr('type');
        if (currentType === 'password') {
            passwordInput.attr('type', 'text');
            $(this).html('<i class="icofont icofont-eye"></i>');
        } else {
            passwordInput.attr('type', 'password');
            $(this).html('<i class="icofont icofont-eye-blocked"></i>');
        }
    });


    passwor.addEventListener('keyup', function () {
        let pass = passwor.value;
        checkStrength(pass);
    })

    function checkStrength(password) {
        let stregth = 0;

        if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) {
            stregth += 1;
        }
        if (password.match(/([0-9])/)) {
            stregth += 1;
        }
        if (password.match(/[`!@#$%^&*()_+\-=\[\]{ };':"\\|,.<>\/?~]/)) {
            stregth += 1;
        }
        if (password.length > 11) {
            stregth += 1;
        }
        if (stregth == 0) {
            $("#estado1").css("display", "block");
            $("#estado2").css("display", "none");
            $("#estado3").css("display", "none");
            $("#estado4").css("display", "none");
        } else if (stregth == 2) {
            $("#estado1").css("display", "none");
            $("#estado2").css("display", "block");
            $("#estado3").css("display", "none");
            $("#estado4").css("display", "none");
        } else if (stregth == 3) {
            $("#estado1").css("display", "none");
            $("#estado2").css("display", "none");
            $("#estado3").css("display", "block");
            $("#estado4").css("display", "none");
        }
        else if (stregth == 4) {
            $("#estado1").css("display", "none");
            $("#estado2").css("display", "none");
            $("#estado3").css("display", "none");
            $("#estado4").css("display", "block");
        }

    }

});

function Guardar() {
    if (Validar()) {
        if (validatepass()) {
            if (passecure()) {
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
                        Password: $('#Contraseña').val(),
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


                    fetch("/Administracion/Usuario/CreateUser", {
                        method: "POST",
                        headers: { "Content-Type": "application/json; charset=utf-8" },
                        body: JSON.stringify(_model)
                    })
                        .then(res => res.json())
                        .then(res => {
                            if (res.id === 1) {
                                swal({
                                    title: "Exito",
                                    text: "Nuevo usuario registrado.",
                                    imageUrl: "https://asddemo.asae.com.mx/icons/operacion/create.png",

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
                                    title: "Usuario no registrado",
                                    text: "No es posible registrar el Usuario. !",
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
                    title: "Contraseña no segura",
                    text: "La contraseña no cumple con los requisitos:\n* Minúsculas y Mayúsculas\n* Números (0-9)\n* Caracteres especiales (!@@#$^&*)\n* Mayor a 11 caracteres",
                    imageUrl: "https://asddemo.asae.com.mx/icons/operacion/alert.png",
                    buttons: true,
                    dangerMode: true,
                })
            }

        } else {
            swal({
                title: "Error Contraseña",
                text: "Verifica que las contraseñas sean las mismas",
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

function Validar() {
    var result = false;

    $('#Contraseña,#Contraseña2, #Nombre, #ApellidoP, #ApellidoM, #Email, #Telefono, #selectRol, #SelectCorreo, #SelectTelefono, #selectCuadrilla').css("border", "1px solid #ff0049");

    if ($('#Contraseña').val().length > 0) { $('#Contraseña').css("border", "1px solid #cccccc"); }
    if ($('#Contraseña2').val().length > 0) { $('#Contraseña2').css("border", "1px solid #cccccc"); }
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
        $('#Contraseña').val().length > 0 &&
        $('#Contraseña2').val().length > 0 &&
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

function validatepass() {
    pass1 = document.getElementById('Contraseña');
    pass2 = document.getElementById('Contraseña2');

    if (pass1.value != pass2.value) {
        return false;
    } else {
        return true;
    }
}
function passecure() {
    var result = false;

    let password = passwor.value;
    let stregth = 0;

    if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) {
        stregth += 1;
    }


    if (password.match(/([0-9])/)) {
        stregth += 1;
    }

    if (password.match(/[`!@@#$%^&*()_+\-=\[\]{ };':"\\|,.<>\/?~]/)) {
        stregth += 1;
    }

    if (password.length > 11) {
        stregth += 1;
    }

    if (stregth == 4) {
        result = true;
    }

    return result;

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
$("#estado1").css("display", "block");
$("#estado2").css("display", "none");
$("#estado3").css("display", "none");
$("#estado4").css("display", "none");

$("#MenuUsuario").addClass("active");
$("#NuevoUsuario").addClass("active");

