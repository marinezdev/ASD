using ASD.Areas.Empresa.Models;
using ASD.Areas.TicketUnitario.Models;

namespace ASD.Areas.Ticket.Models.Consulta
{
    public class CTicketViewModel
    {
        public TicketViewModel? Ticket { get; set; }
       
        public FolioViewModel? Folio { get; set; }
        public FechaAtencionViewModel? FechaAtencion { get; set; }
        public Cat_EstatusOrdenTrabajoViewModel? Cat_EstatusOrdenTrabajo { get; set; }
        public Cat_AsignacionEmpresaViewModel? Cat_AsignacionEmpresa { get; set; }
        public CuadrillaViewModel? Cuadrilla { get; set;}
        public CuadrillaUsuarioViewModel? CuadrillaUsuario { get; set; }


        /// <summary>
        /// Lista de empleados que pertenecen a la cuadrialla
        /// </summary>
        public List<CuadrillaUsuarioViewModel>? ListaCuadrilla { get; set; }
        public Cat_EstadoViewModel? Cat_Estado { get; set; }
        public string? TiempoAtencion { get; set; }
        public string? ColorFechaAtencion { get; set; }
        public int NuevaFecha { get; set; }
        public DateTime NuevaFechaAtencion { get; set; }

        public TicketAtenderViewModel? TicketAtenderViewModel { get; set; }

        public string? Direccion { get; set; }
    }
}
