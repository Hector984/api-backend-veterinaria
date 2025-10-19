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
        }
        
    }
}
