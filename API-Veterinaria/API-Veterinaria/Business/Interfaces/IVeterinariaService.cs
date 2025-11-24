using API_Veterinaria.Core.DTOs.Veterinaria;

namespace API_Veterinaria.Business.Interfaces
{
    public interface IVeterinariaService
    {
        Task<VeterinariaDTO> ObtenerVeterinariaPorIdAsync(int id);
        Task<IEnumerable<VeterinariaDTO>> ObtenerVeterinariasAsync();
        Task<VeterinariaDTO> ObtenerVeterinariaPorDuenoIdAsync();
        Task<VeterinariaDTO> RegistrarVeterinariaAsync(RegistrarVeterinariaDTO registrarVeterinariaDTO);
        Task ActualizarVeterinariaAsync(int id, RegistrarVeterinariaDTO registrarVeterinariaDTO);
        Task ActivarDesactivarVeterinariaAsync(int id);
    }
}
