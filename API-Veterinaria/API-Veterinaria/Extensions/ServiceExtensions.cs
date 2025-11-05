using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Business.Services;
using API_Veterinaria.Data;
using API_Veterinaria.Data.Interfaces;
using API_Veterinaria.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API_Veterinaria.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                IConfiguration configuration)
        {
            services.AddDbContext<VeterinariaDbContext>(opciones =>
               opciones.UseNpgsql(
                   configuration.GetConnectionString("LocalConnection")));

            //Repositories
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IVeterinariaRepository, VeterinariaRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IMascotaRepository, MascotaRepository>();

            // Services
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IVeterinariaService, VeterinariaService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IMascotaService, MascotaService>();

            return services;
        }
    }
}
