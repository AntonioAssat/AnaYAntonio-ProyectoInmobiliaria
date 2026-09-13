using AnaYAntonio_ProyectoInmobiliaria.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//autentificacion
//Esto le dice a ASP.NET que vamos a usar cookies para recordar qué usuario inició sesión
//Y si alguien intenta entrar a una página protegida sin estar logueado:
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });
//politica/autorizacion de roles
builder.Services.AddAuthorization(options =>{
    options.AddPolicy("Administrador", policy =>
    {
        policy.RequireRole("Administrador");
    });

    options.AddPolicy("Empleado", policy =>
    {
        policy.RequireRole("Empleado");
    });
});
// Inquilino
builder.Services.AddScoped<IRepositorioInquilino, RepositorioInquilino>();

// Propietario
builder.Services.AddScoped<IRepositorioPropietario, RepositorioPropietario>();

//Reserva
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();

// Inmueble
builder.Services.AddScoped<IRepositorioInmueble, RepositorioInmueble>();

// TipoInmueble
builder.Services.AddScoped<IRepositorioTipoInmueble, RepositorioTipoInmueble>();

// Pago
builder.Services.AddScoped<IRepositorioPago, RepositorioPago>();

//Usuario
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

// Imagen
builder.Services.AddScoped<IRepositorioImagen, RepositorioImagen>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
