using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Data.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObtenerClientePorId(int id);
        Task<IEnumerable<Cliente>> ObtenerClientesPorVeterinariaId(int veterinariaId);
        Task<Cliente?> ObtenerClientePorIdConVeterinaria(int id);
        Task ReggistrarCliente(Cliente cliente);
        Task ActualizarCliente(Cliente cliente);
        Task ActivarDesactivar(Cliente cliente);
    }
}
