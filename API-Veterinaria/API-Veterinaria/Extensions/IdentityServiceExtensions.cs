using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API_Veterinaria.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            
            services.AddIdentityCore<Usuario>()
                .AddEntityFrameworkStores<VeterinariaDbContext>()
                .AddDefaultTokenProviders();

            // Inyecta el servicio UserManager<IdentityUser>, que se encarga de:
            // - Crear usuarios
            // - Cambiar contraseñas
            // - Asignar roles
            // - Consultar información de usuarios
            services.AddScoped<UserManager<Usuario>>();

            // Inyecta el servicio SignInManager<IdentityUser>, que se encarga de:
            // - Validar credenciales de usuario (login)
            // - Manejar cookies de autenticación
            // - Manejar login externo (Google, Facebook, etc.)
            services.AddScoped<SignInManager<Usuario>>();

            // Permite acceder al contexto HTTP actual (petición, usuario logueado, headers, etc.)
            // Esto es útil para casos donde necesitas información de la request en servicios.
            services.AddHttpContextAccessor();

            // Configura el sistema de autenticación de la app para usar JWT (tokens JSON Web Tokens).
            services.AddAuthentication().AddJwtBearer(opciones =>
            {
                // Desactiva el mapeo automático de claims de JWT a claims de .NET
                // (así manejas los claims tal cual vienen en el token).
                opciones.MapInboundClaims = false;

                // Configuración de validación de tokens
                opciones.TokenValidationParameters = new TokenValidationParameters
                {
                    // No valida el emisor del token (issuer). En producción normalmente se valida.
                    ValidateIssuer = false,

                    // No valida el destinatario (audience). También en producción suele validarse.
                    ValidateAudience = false,

                    // Sí valida la fecha de expiración del token.
                    ValidateLifetime = true,

                    // Sí valida la firma del token (clave secreta usada para generarlo).
                    ValidateIssuerSigningKey = true,

                    // Clave secreta que se usará para validar la firma del token.
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["llavejwt"]!)
                        ),

                    // Ajusta la tolerancia de tiempo en la expiración del token.
                    // Por defecto hay 5 minutos de tolerancia, aquí se pone a cero.
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
