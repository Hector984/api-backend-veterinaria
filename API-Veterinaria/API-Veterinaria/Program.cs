using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API_Veterinaria.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Se agrega para activar la encriptacion de datos
builder.Services.AddDataProtection();

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCORS =>
    {
        opcionesCORS.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

//builder.Services.AddSwagger();
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Yolcatl (Náhuatl - \"Animal con corazón\")",
        Description = " Web API para la administración de veterinarias.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "hectorantoniojimenezmanzo@gmail.com",
            Name = "Héctor Antonio Jiménez Manzo",
            Url = new Uri("https://www.linkedin.com/in/h%C3%A9ctor-antonio-jim%C3%A9nez-manzo/")
        }
    });

    opciones.EnableAnnotations();
});

var app = builder.Build();

// Area de middlewares

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    //app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
