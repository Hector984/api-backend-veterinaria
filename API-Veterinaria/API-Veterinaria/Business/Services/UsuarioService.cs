using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;

namespace API_Veterinaria.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IHttpContextAccessor _contextAccesor;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IHttpContextAccessor contextAccesor, IUsuarioRepository usuarioRepository)
        {
            _contextAccesor = contextAccesor;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario?> GetUsuarioByEmail()
        {
            var emailClaim = _contextAccesor.HttpContext!
                .User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim is null)
            {
                return null;
            }

            var email = emailClaim.Value;

            return await _usuarioRepository.ObtenerPorEmailAsync(email);
        }
    }
}
