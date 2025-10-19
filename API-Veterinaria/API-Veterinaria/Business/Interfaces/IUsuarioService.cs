using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Business.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> GetUsuarioByEmail();
    }
}
