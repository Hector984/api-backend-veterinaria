using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API_Veterinaria.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

//builder.Services.AddDbContext<VeterinariaDbContext>(opciones => 
//    opciones.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnection")));

//builder.Services.AddIdentityCore<Usuario>()
//    .AddEntityFrameworkStores<VeterinariaDbContext>()
//    .AddDefaultTokenProviders();

// Inyecta el servicio UserManager<IdentityUser>, que se encarga de:
// - Crear usuarios
// - Cambiar contrase�as
// - Asignar roles
// - Consultar informaci�n de usuarios
//builder.Services.AddScoped<UserManager<Usuario>>();

// Inyecta el servicio SignInManager<IdentityUser>, que se encarga de:
// - Validar credenciales de usuario (login)
// - Manejar cookies de autenticaci�n
// - Manejar login externo (Google, Facebook, etc.)
//builder.Services.AddScoped<SignInManager<Usuario>>();

// Permite acceder al contexto HTTP actual (petici�n, usuario logueado, headers, etc.)
// Esto es �til para casos donde necesitas informaci�n de la request en servicios.
//builder.Services.AddHttpContextAccessor();

// Configura el sistema de autenticaci�n de la app para usar JWT (tokens JSON Web Tokens).
//builder.Services.AddAuthentication().AddJwtBearer(opciones =>
//{
//    // Desactiva el mapeo autom�tico de claims de JWT a claims de .NET
//    // (as� manejas los claims tal cual vienen en el token).
//    opciones.MapInboundClaims = false;

//    // Configuraci�n de validaci�n de tokens
//    opciones.TokenValidationParameters = new TokenValidationParameters
//    {
//        // No valida el emisor del token (issuer). En producci�n normalmente se valida.
//        ValidateIssuer = false,

//        // No valida el destinatario (audience). Tambi�n en producci�n suele validarse.
//        ValidateAudience = false,

//        // S� valida la fecha de expiraci�n del token.
//        ValidateLifetime = true,

//        // S� valida la firma del token (clave secreta usada para generarlo).
//        ValidateIssuerSigningKey = true,

//        // Clave secreta que se usar� para validar la firma del token.
//        IssuerSigningKey =
//            new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"]!)
//            ),

//        // Ajusta la tolerancia de tiempo en la expiraci�n del token.
//        // Por defecto hay 5 minutos de tolerancia, aqu� se pone a cero.
//        ClockSkew = TimeSpan.Zero
//    };
//});

var app = builder.Build();

// Area de middlewares

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
