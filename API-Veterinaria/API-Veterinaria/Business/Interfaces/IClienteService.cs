using API_Veterinaria.Core.DTOs.Cliente;
using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Business.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDTO?> ObtenerClientePorIdAsync(int clienteId);
        Task<IEnumerable<ClienteDTO>> ObtenerClientesPorVeterinariaIdAsync(int veterinariaId);
        Task<ClienteDTO> RegistrarClienteAsync(RegistrarClienteDTO dto);
        Task ActualizarClienteAsync(int clienteId, ActualizarClienteDTO dto);
        Task ActivarDesactivarClienteAsync(int clienteId);
    }

}
