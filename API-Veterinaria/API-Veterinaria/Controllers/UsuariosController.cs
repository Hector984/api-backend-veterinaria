using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Autenticacion;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Veterinaria.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IUsuarioService _usuarioService;
        private readonly VeterinariaDbContext _context;

        public UsuariosController(UserManager<Usuario> userManager, IConfiguration configuration,
            SignInManager<Usuario> signInManager, IUsuarioService servicioUsuarios,
            VeterinariaDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _usuarioService = servicioUsuarios;
            _context = context;
        }

        [HttpPost("iniciar-sesion")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Login (CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var usuario = await _userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);

            if (usuario is null)
            {
                return RetornarLoginIncorrecto();
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario,
                credencialesUsuarioDTO.Password, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuarioDTO);
            }

            return RetornarLoginIncorrecto();
        }

        [HttpPost("registro")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Registrar(RegistrarUsuarioDTO credencialesUsuarioDTO)
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

            if (resultado.Succeeded)
            {
                var respuestaAutenticacion = await ConstruirToken(new CredencialesUsuarioDTO 
                { Email = credencialesUsuarioDTO.Email, Password = credencialesUsuarioDTO.Password });

                return respuestaAutenticacion;
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return ValidationProblem();
            }
        }

        [HttpGet("renovar-token")]
        [Authorize]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> RenovarToken()
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                return NotFound();
            }

            var credencialesUsuarioDTO = new CredencialesUsuarioDTO { Email = usuario.Email! };

            var respuestaAutenticacion = await ConstruirToken(credencialesUsuarioDTO);

            return respuestaAutenticacion;
        }

        private async Task<RespuestaAutenticacionDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var claims = new List<Claim>
            {
                new Claim("email", credencialesUsuarioDTO.Email)
            };

            var usuario = await _userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);
            var claimsBD = await _userManager.GetClaimsAsync(usuario!);

            claims.AddRange(claimsBD);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["llavejwt"]!));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var tokenSeguridad = new JwtSecurityToken(issuer: null, audience: null,
                claims: claims, expires: expiracion, signingCredentials: credenciales);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSeguridad);

            return new RespuestaAutenticacionDTO { Token = token, Expiracion = expiracion };
        }

        private ActionResult RetornarLoginIncorrecto()
        {
            ModelState.AddModelError(string.Empty, "Las credenciales son invalidas");

            return ValidationProblem();
        }

    }
}
