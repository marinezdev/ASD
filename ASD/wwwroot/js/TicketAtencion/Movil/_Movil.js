$('#MenuOperacion').addClass("active");


function Siguiente(Id) {

    const Ticket = {
        Id: Id,
    }

    const _modelo = {
        Ticket: Ticket,
    };

    fetch("/Ticket/TicketCuadrilla/CreateTicketCuadrilla_AtenderTicket", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(_modelo)
    })
    .then(res => res.json())
    .then(res => {
        if (res.id > 0) {


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
        } else {
            alert("Ticket no procesable");
        }
    });

    
}





