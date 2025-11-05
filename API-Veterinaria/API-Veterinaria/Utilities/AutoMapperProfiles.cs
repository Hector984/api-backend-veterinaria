using API_Veterinaria.Core.DTOs.Cliente;
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

            //Mapeos de Mascota
            CreateMap<Mascota, MascotaDTO>()
                .ForMember(ent => ent.NombreCliente,
                    config => config.MapFrom(ent => MapearNombreCompletoCliente(ent.Cliente)))
                .ForMember(ent => ent.TelefonoCliente,
                    config => config.MapFrom(ent => ent.Cliente.Telefono));

            CreateMap<RegistrarMascotaDTO, MascotaDTO>();

            CreateMap<ActualizarMascotaDTO, Mascota>();
        }

        private string MapearNombreCompletoCliente(Cliente cliente) => $"{cliente.Nombre} {cliente.ApellidoPaterno} {cliente.ApellidoMaterno}";
        
    }
}
