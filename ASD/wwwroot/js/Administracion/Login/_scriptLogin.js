const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

signUpButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});


$(document).ready(function () {
   
    $("#IniciarSesion").on("submit", function (event) {
        event.preventDefault();
        $("#ErrorMensaje").html("");
        if (ValidateForm()) {
            const _modelo = {
                Password: $("#txPassword").val(),
                Usuario: $("#txUsuario").val(),
            };

            fetch("/Administracion/Login/IniciarSesion", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(_modelo)
            })
                .then(res => res.json())
                .then(res => {

                    if (res && typeof res === 'object' && res.usuario && typeof res.usuario.id !== 'undefined') {
                        if (res.usuario.id !== 0) {
                            redirigirDashboard(res);
                        } else {
                            $("#ErrorMensaje").html("Password incorrecto");
                        }
                    } else if (res && typeof res === 'string') {
                        window.location.href = res;
                    } else {
                        $("#ErrorMensaje").html("Respuesta inesperada del servidor");
                    }

                });
        } else {
            $("#ErrorMensaje").html("Completa los campos marcados");
        }
    });

   

});

function ValidateForm() {
    var result = false;
    $('#txPassword').css("border", "1px solid red");
    $('#txUsuario').css("border", "1px solid red");

    if ($('#txPassword').val().length > 0) {
        $('#txPassword').css("border", "0px");

        if ($('#txUsuario').val().length > 0) {
            $('#txUsuario').css("border", "0px");
            result = true;
        }
    } 

    return result;
}

function ValidaInput(Id) {
    if ($('#' + Id).val().length > 0) {
        $('#' + Id).css("border", "0px");
    } else {
        $('#' + Id).css("border", "1px solid red");
    }
}


function redirigirDashboard(res) {
   /* console.log(res);*/
    if (res.usuario.id == -1) {
        //alert("Usuario no encontrado");
        $("#ErrorMensaje").html("Usuario no encontrado");
    } else if (res.usuario.id == 0) {
        $("#ErrorMensaje").html("Password incorrecto");
    } else {
        window.location.href = res.usuario.cat_Rol.ruta;

        //if (res.usuario.cat_Rol.id == 1) {
        //    window.location.href = '/Dhasbord/Operador';
        //} else if (res.usuario.cat_Rol.id == 2) {
        //    window.location.href = '/Dhasbord/SuperProveedor';
        //} else if (res.usuario.cat_Rol.id == 3) {
        //    window.location.href = '/Dhasbord/Home';
        //} else if (res.usuario.cat_Rol.id == 5) {
        //    window.location.href = '/Dhasbord/Administracion';
        //} else if (res.usuario.cat_Rol.id == 7) {
        //    window.location.href = '/Dhasbord/Cliente';
        //} else if (res.usuario.cat_Rol.id == 1007) {
        //    window.location.href = '/Dhasbord/Supervisor';
        //} else if (res.usuario.cat_Rol.id == 2007) {
        //    window.location.href = '/Dhasbord/Supervisor';
        //}
    }
}