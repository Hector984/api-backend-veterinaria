using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Data.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetById(int id);
        Task<IEnumerable<Cliente>> GetAllClientesByVeterinariaId(int veterinariaId);
        Task<Cliente?> GetClienteByIdConVeterinaria(int id);
        Task Add(Cliente cliente);
        Task Update(Cliente cliente);
        Task Delete(Cliente cliente);
    }
}
