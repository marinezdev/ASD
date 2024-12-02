using ASD.Areas.Dhasbord.Models;
using System;
namespace ASD.Areas.WebApi.Dhasbord.Models
{
	public class DOperadorModels
	{
       public List<DPrioridadViewModel>? TicketsPrioridad { get; set; }
       public DPendientesViewModel?  TicketsPendientes { get; set; }
    }
}

