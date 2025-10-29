using API_Veterinaria.Core.DTOs.Veterinaria;

namespace API_Veterinaria.Business.Interfaces
{
    public interface IVeterinariaService
    {
        Task<VeterinariaDTO> GetVeterinariaByIdAsync(int id);
        Task<IEnumerable<VeterinariaDTO>> GetVeterinariasAsync();
        Task<VeterinariaDTO> GetVeterinariaUsuarioAsync();
        Task<VeterinariaDTO> PostVeterinariaAsync(RegistrarVeterinariaDTO registrarVeterinariaDTO);
        Task UpdateVeterinariaAsync(int id, RegistrarVeterinariaDTO registrarVeterinariaDTO);
        Task DeleteVeterinariaAsync(int id);
    }
}
