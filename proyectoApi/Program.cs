using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using proyectoApi;
using proyectoApi.Interfaces;
using proyectoApi.Models;
using proyectoApi.Services;
using proyectoApi.ViewModels;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Añadiendo DbContext
builder.Services.AddDbContext<PruebaContext>(opciones=> opciones.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<db_libreriaContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionLibreria")));

// Inyeccion de dependencias
builder.Services.AddScoped<ISolicitudService, SolicitudService>();
builder.Services.AddScoped<IAutorService, AutorService>();


// Añadiendo Cors para permitir los request desde cualquier lugar
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
          builder =>
          {
              builder.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod();
          });
});


var app = builder.Build();

app.UseCors("AllowAllHeaders");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// Listar personas
app.MapGet("/Personas", ([FromServices] PruebaContext context) =>
{
    return context.Personas.ToList();

});

// Listar solicitudes
app.MapGet("/Solicitudes", ([FromServices] PruebaContext context) =>
{
    return context.Solicituds.ToList();

});

// Cambiar el estado de solicitud
app.MapPost("/CambiarEstado", async (ISolicitudService soli, [FromBody] cambiarEstadoVM vm) =>
{
    var response = await soli.UpdateState(vm);
    if(response) return Results.Ok("Hecho!");
    return Results.Ok(response);

});

// Buscar autor, recibiendo nombre y apellido
app.MapPost("/BuscarAutor", async (IAutorService autor, [FromBody] BuscarAutorVM vm, [FromServices] db_libreriaContext context) =>
{
    var response = autor.BuscarAutor(vm);
    if (response == null) return Results.NotFound();
    return Results.Ok(response);

});

app.Run();
