using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Data.Interfaces
{
    public interface IMascotaRepository
    {
        Task<Mascota?> GetById(int id);
        Task<IEnumerable<Mascota>> GetAllByClienteId(int clienteId);
        Task<IEnumerable<Mascota>> GetAllByVeterinariaId(int veterinariaId);
        Task<Mascota?> GetByIdConClienteYVeterinaria(int veterinariaId);
        Task Add(Mascota mascota);
        Task Update(Mascota mascota);
        Task Delete(Mascota mascota);
    }
}
