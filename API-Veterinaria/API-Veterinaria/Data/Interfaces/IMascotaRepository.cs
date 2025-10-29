using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Data.Interfaces
{
    public interface IMascotaRepository
    {
        Task<Mascota> GetByIdAsync(int id);
        Task<IEnumerable<Mascota>> GetAllByClienteIdAsync(int clienteId);
        Task<IEnumerable<Mascota>> GetAllByVeterinariaAsync(int veterinariaId);
        Task AddAync(Mascota mascota);
        Task UpdateAsync(Mascota mascota);
        Task DeleteAsync(Mascota mascota);
    }
}
