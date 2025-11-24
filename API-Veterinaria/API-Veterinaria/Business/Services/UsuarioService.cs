using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Autenticacion;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Veterinaria.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IHttpContextAccessor _contextAccesor;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioService(IHttpContextAccessor contextAccesor, IUsuarioRepository usuarioRepository, UserManager<Usuario> userManager,
            IConfiguration configuration, SignInManager<Usuario> signInManager)
        {
            _contextAccesor = contextAccesor;
            _usuarioRepository = usuarioRepository;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
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

        public async Task<RespuestaAutenticacionDTO> Login(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var usuario = await _userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);

            if (usuario is null)
            {
                throw new InvalidOperationException("Las credenciales son invalidas");
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario,
                credencialesUsuarioDTO.Password!, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuarioDTO.Email);
            }

            throw new InvalidOperationException("Las credenciales son invalidas");
        }

        public async Task<RespuestaAutenticacionDTO> RegistrarUsuario(RegistrarUsuarioDTO credencialesUsuarioDTO)
        {
            var usuario = new Usuario
            {
                UserName = credencialesUsuarioDTO.Email,
                Nombre = credencialesUsuarioDTO.Nombre,
                ApellidoP = credencialesUsuarioDTO.ApellidoP,
                ApellidoM = credencialesUsuarioDTO.ApellidoM,
                Email = credencialesUsuarioDTO.Email,
                Activo = true
            };

            var resultado = await _userManager.CreateAsync(usuario, credencialesUsuarioDTO.Password);

            if (!resultado.Succeeded)
            {

                var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));

                throw new InvalidOperationException($"Error al crear usuario: {errores}");
            }
            
            return await ConstruirToken(credencialesUsuarioDTO.Email);
        }

        public async Task<RespuestaAutenticacionDTO> ConstruirToken(string emailUsuario)
        {
            var claims = new List<Claim>
            {
               new Claim("email", emailUsuario)
            };

            var usuario = await _userManager.FindByEmailAsync(emailUsuario);
            var claimsBD = await _userManager.GetClaimsAsync(usuario!);

            claims.Add(new Claim("UsuarioId", usuario!.Id));

            claims.AddRange(claimsBD);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["llavejwt"]!));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var tokenSeguridad = new JwtSecurityToken(issuer: null, audience: null,
                claims: claims, expires: expiracion, signingCredentials: credenciales);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSeguridad);

            return new RespuestaAutenticacionDTO { Token = token, Expiracion = expiracion };
        }

        public async Task<RespuestaAutenticacionDTO> RenovarToken()
        {
            var usuario = await GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }

            var credencialesUsuarioDTO = new CredencialesUsuarioDTO { Email = usuario.Email! };

            var respuestaAutenticacion = await ConstruirToken(credencialesUsuarioDTO.Email);

            return respuestaAutenticacion;
        }
    }
}
