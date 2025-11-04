using API_Veterinaria.Core.DTOs.Autenticacion;
using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Business.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> GetUsuarioByEmail();
        Task<RespuestaAutenticacionDTO> RegistrarUsuario(RegistrarUsuarioDTO credencialesUsuarioDTO);
        Task<RespuestaAutenticacionDTO> ConstruirToken(string emailUsuario);
        Task<RespuestaAutenticacionDTO> Login(CredencialesUsuarioDTO credencialesUsuarioDTO);
        Task<RespuestaAutenticacionDTO> RenovarToken();
    }
}
