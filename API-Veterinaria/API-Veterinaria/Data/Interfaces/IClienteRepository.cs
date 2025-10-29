using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Data.Interfaces
{
    public interface IClienteRepository
    {
        Task<Mascota> GetByIdAsync(int id);
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task AddAync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(Cliente cliente);
    }
}
