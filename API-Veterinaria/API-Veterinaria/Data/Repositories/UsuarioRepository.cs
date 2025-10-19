using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API_Veterinaria.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<Usuario> _userManager;

        public UsuarioRepository(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
