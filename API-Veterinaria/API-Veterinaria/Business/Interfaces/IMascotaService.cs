using API_Veterinaria.Core.DTOs.Compartidos;
using API_Veterinaria.Core.DTOs.Mascota;
using API_Veterinaria.Core.DTOs.Veterinaria;
using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Business.Interfaces
{
    public interface IMascotaService
    {
        Task<MascotaDTO> ObtenerMascotaPorId(int id);
        Task<IEnumerable<MascotaDTO>> ObtenerMascotasPorClienteId(int clienteId);
        Task<IEnumerable<MascotaDTO>> ObtenerMascotasPorVeterinariaId(int veterinariaId);
        Task<MascotaDTO> RegistrarMascota(RegistrarMascotaDTO registrarVeterinariaDTO);
        Task ActualizarInformacionMascota(int mascotaId, ActualizarMascotaDTO registrarVeterinariaDTO);
        Task ActualizarEstatusMascota(int mascotaId);

    }
}
