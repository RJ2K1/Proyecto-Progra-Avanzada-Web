using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// cadena de conexión de la base de datos.
builder.Services.AddDbContext<ProyectoWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// implementaciones de DAL y Servicios al contenedor de inyección de dependencias.
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
builder.Services.AddScoped<IUsuariosDAL, UsuariosDALImpl>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IRolesDAL, RolesDALImpl>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IAuditoriaDAL, AuditoriaDALImpl>();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();

// Agrega las implementaciones de DAL y Servicio para Departamentos.
builder.Services.AddScoped<IDepartamentosDAL, DepartamentosDALImpl>();
builder.Services.AddScoped<IDepartamentosService, DepartamentosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
