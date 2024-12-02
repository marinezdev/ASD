
using ASD.Areas.Administracion.Models;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Empresa;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Interface.Persona;
using ASD.Repository.Interface.TicketAtencionMovil;
using ASD.Repository.Services.Administracion;
using ASD.Repository.Services.Empresa;
using ASD.Repository.Services.Inventario;
using ASD.Repository.Services.Operacion;
using ASD.Repository.Services.Ticket;
using ASD.Repository.Services.Persona;
using ASD.Repository.Services.TicketAtencionMovil;


using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using ASD.Areas.Ticket.Controllers;
using ASD.Repository.Interface.Dhasbord;
using ASD.Repository.Services.Dhasbord;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUsuarioRepository, UsuarioService>();
builder.Services.AddScoped<IControlArchivoRepository, ControlArchivoService>();
builder.Services.AddScoped<IFlujoRepository, FlujoService>();
builder.Services.AddScoped<ITicketEtapaRepository, TicketEtapaService>();
builder.Services.AddScoped<ITicketRepository, TicketService>();
builder.Services.AddScoped<ICat_PrioridadRepository, Cat_PrioridadService>();
builder.Services.AddScoped<ICat_AsignacionEmpresaRepository, Cat_AsignacionEmpresaService>();
builder.Services.AddScoped<ISucursalRepository, SucursalService>();
builder.Services.AddScoped<ICuadrillaRepository, CuadrillaService>();
builder.Services.AddScoped<ICuadrillaUsuarioRepository, CuadrillaUsuarioService>();
builder.Services.AddScoped<ICat_EstatusTicketRepository, Cat_EstatusTicketService>();
builder.Services.AddScoped<ICat_EstatusOrdenTrabajoRepository, Cat_EstatusOrdenTrabajoService>();
builder.Services.AddScoped<ICat_TipoServicioRepository, Cat_TipoServicioService>();
builder.Services.AddScoped<ICat_EstadoRepository, Cat_EstadoService>();
builder.Services.AddScoped<ICat_PoblacionRepository, Cat_PoblacionService>();
builder.Services.AddScoped<ICat_CPRepository, Cat_CPService>();
builder.Services.AddScoped<ICat_ColoniaRepository, Cat_ColoniaService>();
builder.Services.AddScoped<ICat_TipoSucursalRepository, Cat_TipoSucursalService>();
builder.Services.AddScoped<ICat_CategoriaRepository, Cat_CategoriaService>();
builder.Services.AddScoped<ICat_EquipoImagenRepository, Cat_EquipoImagenService>();
builder.Services.AddScoped<ICat_EquipoRutinaRepository, Cat_EquipoRutinaService>();
builder.Services.AddScoped<ICat_EstatusEquipoRepository, Cat_EstatusEquipoService>();
builder.Services.AddScoped<ICat_ModeloRepository, Cat_ModeloService>();
builder.Services.AddScoped<ICat_TipoEquipoRepository, Cat_TipoEquipoService>();
builder.Services.AddScoped<IEquipoRepository, EquipoService>();
builder.Services.AddScoped<IEquipoUsuarioRepository, EquipoUsuarioService>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaService>();
builder.Services.AddScoped<ITicketTipoServicioRepository, TicketTipoServicioService>();
builder.Services.AddScoped<ITicketEquipoRepository, TicketEquipoService>();
builder.Services.AddScoped<ITicketEquipoRutinaRepository, TicketEquipoRutinaService>();
builder.Services.AddScoped<ITicketEquipoImagenRepository, TicketEquipoImagenService>();
builder.Services.AddScoped<IBitacoraRepository, BitacoraService>();
builder.Services.AddScoped<INuevaFechaAtencionRepository, NuevaFechaAtencionService>();
builder.Services.AddScoped<IOrdenTrabajoRepository, OrdenTrabajoService>();
builder.Services.AddScoped<IFirmaElectronicaRepository, FirmaElectronicaService>();
builder.Services.AddScoped<ITicketAsignacionEmpresaRepository, TicketAsignacionEmpresaService>();
builder.Services.AddScoped<ITicketCuadrillaRepository, TicketCuadrillaService>();
builder.Services.AddScoped<ICat_TipoEmailRepository, Cat_TipoEmailService>();
builder.Services.AddScoped<ISucursalImgRepository, SucursalImgService>();
builder.Services.AddScoped<ISucursalVideoRepository, SucursalVideoService>();
builder.Services.AddScoped<IArchivoRepository, ArchivoService>();
builder.Services.AddScoped<IPersonaRepository, PersonaService>();
builder.Services.AddScoped<ICat_EstatusFechaAtencionRepository, Cat_EstatusFechaAtencionService>();
builder.Services.AddScoped<ICierreClienteRepository, CierreClienteService>();
builder.Services.AddScoped<ICuadrillaZonaRepository, CuadrillaZonaService>();
builder.Services.AddScoped<IDTicketRepository, DTicketService>();
builder.Services.AddScoped<IDPrioridadRepository, DPrioridadService>();
builder.Services.AddScoped<IDPendientesRepository, DPendientesService>();
builder.Services.AddScoped<IDTiempoAtencionRepository, DTiempoAtencionService>();
builder.Services.AddScoped<IDFirmaRecibidoRepository, DFirmaRecibidoService>();
builder.Services.AddScoped<IDEstatusTicketRepository, DEstatusTicketService>();

builder.Services.AddScoped<ICat_RolRepository, Cat_RepositoryService>();
builder.Services.AddScoped<IUsuarioEtapaRepository, UsuarioEtapaService>();
builder.Services.AddScoped<IMenuRepository, MenuService>();

builder.Services.AddScoped<ITicketFechaAtencionRepository, TicketFechaAtencionService>();
builder.Services.AddScoped<ICat_EstatusTicketEtapaRepository, Cat_EstatusTicketEtapaService>();

builder.Services.AddScoped<IEscalacionRepository, EscalacionService>();
builder.Services.AddScoped<IEscalacionTiempoRepository, EscalacionTiempoService>();
builder.Services.AddScoped<ITicketEscalacionRepository, TicketEscalacionService>();

builder.Services.AddScoped<IUsuarioFlujoRepository, UsuarioFlujoService>();
builder.Services.AddScoped<IPasswordResetRequestsRepository, PasswordResetRequestsService>();

builder.Services.AddScoped<ICat_TipoEquipoRutinaRepository, Cat_TipoEquipoRutinaService>();
builder.Services.AddScoped<ICat_TipoEquipoImagenRepository, Cat_TipoEquipoImagenService>();


///CAMBIO PARA RECARGA ACTIVA EN EL PROYECTO
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Administracion/Login/";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
	});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(
        new ResponseCacheAttribute
        {
            NoStore =true,
            Location = ResponseCacheLocation.None,
        }
    );

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Administracion}/{controller=Login}/{action=Index}/{id?}");

app.Run();
