using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Data.Interfaces
{
    public interface IVeterinariaRepository
    {
        Task<Veterinaria> GetByIdAsync(int id);
        Task<Veterinaria> GetByUserIdAsync(string userId);
        Task<IEnumerable<Veterinaria>> GetAllAsync();
        Task AddAync(Veterinaria veterinaria);
        Task UpdateAsync(Veterinaria veterinaria);
        Task DeleteAsync(Veterinaria veterinaria);
    }
}
