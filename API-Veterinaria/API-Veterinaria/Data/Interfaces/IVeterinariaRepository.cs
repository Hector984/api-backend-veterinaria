using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Data.Interfaces
{
    public interface IVeterinariaRepository
    {
        Task<Veterinaria?> ObtenerVeterinariaPorId(int id);
        Task<Veterinaria?> ObtenerVeterinariaPorDuenoId(string userId);
        Task<IEnumerable<Veterinaria>> ObtenerVeterinarias();
        Task RegistrarVeterinaria(Veterinaria veterinaria);
        Task ActualizarVeterinaria(Veterinaria veterinaria);
        Task ActivarDesactivarVeterinaria(Veterinaria veterinaria);
    }
}
