$('#MenuOperacion').addClass("active");

function CheckCambio(Id) {
    var Estatus = 0;
    if ($("#chkremeberme" + Id + ":checked").length > 0) {
        // PROCESO PARA ACTIVARLO
        Estatus = 1;
    } else {
        Estatus = 0;
    }

    const _modelo = {
        Id: Id,
        Estatus: Estatus
    };

    fetch("/Ticket/TicketEquipoRutina/UpdateTicketEquipoRutina", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        if (res.id = -1) {
           
        }
    });

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
            window.location.href = '/TicketAtencionMovil/FotoDespues/Index?cont=' + res.url;
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
            window.location.href = '/TicketAtencionMovil/FotoAntes/Index?cont=' + res.url;
        });
}






