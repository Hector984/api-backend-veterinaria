using API_Veterinaria.Core.DTOs.Cliente;
using API_Veterinaria.Core.DTOs.Consulta;
using API_Veterinaria.Core.DTOs.Mascota;
using API_Veterinaria.Core.DTOs.Veterinaria;
using API_Veterinaria.Core.Entities;
using AutoMapper;

namespace API_Veterinaria.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // CreateMap<Fuente, Destino>

            // Mepeos de Veterinaria
            CreateMap<RegistrarVeterinariaDTO, Veterinaria>().ReverseMap();
            CreateMap<Veterinaria, VeterinariaDTO>();

            // Mapeos de Cliente
            CreateMap<Cliente, ClienteDTO>()
                .ForMember(ent => ent.Nombre,
                    config => config.MapFrom(ent => MapearNombreCompletoCliente(ent)));

            CreateMap<RegistrarClienteDTO, Cliente>();

            CreateMap<ActualizarClienteDTO, Cliente>();

            //Mapeos de Mascota
            CreateMap<Mascota, MascotaDTO>()
                .ForMember(ent => ent.NombreCliente,
                    config => config.MapFrom(ent => MapearNombreCompletoCliente(ent.Cliente)))
                .ForMember(ent => ent.TelefonoCliente,
                    config => config.MapFrom(ent => ent.Cliente.Telefono))
                .ForMember(ent => ent.Edad, 
                config => config.MapFrom(ent => MapearFechaNacimiento(ent.FechaNacimiento)));

            CreateMap<RegistrarMascotaDTO, Mascota>();

            CreateMap<ActualizarMascotaDTO, Mascota>();

            // Mapeos de Consulta
            CreateMap<RegistrarConsultaDTO, Consulta>();
            CreateMap<Consulta, ConsultaDTO>();
        }

        private string MapearNombreCompletoCliente(Cliente cliente) => $"{cliente.Nombre} {cliente.ApellidoPaterno} {cliente.ApellidoMaterno}";
        private int MapearFechaNacimiento(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > hoy.AddYears(-edad))
                edad--;

            return edad;
        }
        
    }
}
