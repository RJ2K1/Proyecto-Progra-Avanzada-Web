using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();



// Configuración de CORS para permitir solicitudes del frontend.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendApp",
        policyBuilder => policyBuilder
            .WithOrigins("http://localhost:5120") 
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); 
});

// Configuración de la cadena de conexión de la base de datos.
builder.Services.AddDbContext<ProyectoWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionz")));

// Configuración de autenticación basada en cookies.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MiCookieDeAutenticacion";
        options.LoginPath = "/Account/Login"; 
        options.LogoutPath = "/Account/Logout"; 
    });
builder.Services.AddHttpContextAccessor();
#region DI
// Registro de implementaciones de DAL y Servicios en el contenedor de inyección de dependencias.

// Usuarios y Roles
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
builder.Services.AddScoped<IUsuariosDAL, UsuariosDALImpl>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IRolesDAL, RolesDALImpl>();
builder.Services.AddScoped<IRolesService, RolesService>();

// Auditoría
builder.Services.AddScoped<IAuditoriaDAL, AuditoriaDALImpl>();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();

// Tickets 
builder.Services.AddScoped<ITicketsDAL, TicketsDALImpl>();
builder.Services.AddScoped<ITicketsService, TicketsService>();

// Departamentos 
builder.Services.AddScoped<IDepartamentosDAL, DepartamentosDALImpl>();
builder.Services.AddScoped<IDepartamentosService, DepartamentosService>();

// Authentication
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontendApp");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
