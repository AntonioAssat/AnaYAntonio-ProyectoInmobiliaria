using AnaYAntonio_ProyectoInmobiliaria.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();


// =====================================================
// AUTENTICACIÓN
// =====================================================

// Esto le dice a ASP.NET que vamos a usar cookies
// para recordar qué usuario inició sesión.
//
// Si alguien intenta entrar a una página protegida
// sin estar logueado, será enviado al Login.
//
// Si alguien está logueado pero no tiene permisos,
// será enviado a AccesoDenegado.

builder.Services
    .AddAuthentication(
        CookieAuthenticationDefaults.AuthenticationScheme
    )
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Login";

        options.AccessDeniedPath =
            "/Usuario/AccesoDenegado";
    });


// =====================================================
// POLÍTICA / AUTORIZACIÓN DE ROLES
// =====================================================

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "Administrador",
        policy =>
        {
            policy.RequireRole("Administrador");
        }
    );


    options.AddPolicy(
        "Empleado",
        policy =>
        {
            policy.RequireRole("Empleado");
        }
    );
});


// =====================================================
// REPOSITORIOS
// =====================================================

// Inquilino
builder.Services.AddScoped<
    IRepositorioInquilino,
    RepositorioInquilino
>();


// Propietario
builder.Services.AddScoped<
    IRepositorioPropietario,
    RepositorioPropietario
>();


// Reserva
builder.Services.AddScoped<
    IRepositorioReserva,
    RepositorioReserva
>();


// Inmueble
builder.Services.AddScoped<
    IRepositorioInmueble,
    RepositorioInmueble
>();


// Tipo Inmueble
builder.Services.AddScoped<
    IRepositorioTipoInmueble,
    RepositorioTipoInmueble
>();


// Pago
builder.Services.AddScoped<
    IRepositorioPago,
    RepositorioPago
>();


// Usuario
builder.Services.AddScoped<
    IRepositorioUsuario,
    RepositorioUsuario
>();


// Imagen
builder.Services.AddScoped<
    IRepositorioImagen,
    RepositorioImagen
>();


// Auditoría
builder.Services.AddScoped<
    IRepositorioAuditoria,
    RepositorioAuditoria
>();


// Informes
builder.Services.AddScoped<
    IRepositorioInformes,
    RepositorioInformes
>();


var app = builder.Build();


// =====================================================
// CONFIGURAR PIPELINE
// =====================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


// IMPORTANTE:
// Authentication debe ir antes de Authorization.

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
)
.WithStaticAssets();


app.Run();