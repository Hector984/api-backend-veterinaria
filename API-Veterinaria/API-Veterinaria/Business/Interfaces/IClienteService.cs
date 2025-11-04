using API_Veterinaria.Core.DTOs.Cliente;
using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Business.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDTO?> ObtenerClientePorId(int clienteId);
        Task<IEnumerable<ClienteDTO>> ObtenerClientes();
        Task<ClienteDTO> RegistrarCliente(RegistrarClienteDTO dto);
        Task ActualizarCliente(int clienteId, ActualizarClienteDTO dto);
        Task ActualizarEstatusCliente(int clienteId);
    }

}
