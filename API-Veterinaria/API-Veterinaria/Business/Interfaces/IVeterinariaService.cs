using API_Veterinaria.Core.DTOs.Veterinaria;

namespace API_Veterinaria.Business.Interfaces
{
    public interface IVeterinariaService
    {
        Task<VeterinariaDTO> GetVeterinariaByIdAsync(int id);
        Task<IEnumerable<VeterinariaDTO>> GetVeterinariasAsync();
        Task<VeterinariaDTO> PostVeterinariaAsync(RegistrarVeterinariaDTO registrarVeterinariaDTO);
    }
}
