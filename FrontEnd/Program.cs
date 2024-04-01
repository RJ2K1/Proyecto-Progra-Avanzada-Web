using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Configuración del cliente HTTP para el ServiceRepository.
builder.Services.AddHttpClient<IServiceRepository, ServiceRepository>();

// Registro de dependencias para los helpers y servicios.
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IUsuarioHelper, UsuarioHelper>();
// Registra otros helpers y servicios según sea necesario.
// Configuración de autenticación basada en cookies.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MiCookieDeAutenticacion";
        options.LoginPath = "/Account/Login"; // Ruta al controlador de inicio de sesión.
        options.LogoutPath = "/Account/Logout"; // Ruta al controlador de cierre de sesión.
    });

//logger

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();






// Importante: el llamado a UseAuthentication debe ir antes de UseAuthorization.
app.UseAuthentication();
app.UseAuthorization();

// Si estás usando Razor Pages, debes mapearlas.
app.MapRazorPages();

// Configura la ruta predeterminada para que apunte al login.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
