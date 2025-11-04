using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Autenticacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Veterinaria.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService servicioUsuarios)
        {
            _usuarioService = servicioUsuarios;
        }

        [HttpPost("iniciar-sesion")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Login (CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            try
            {
                return await _usuarioService.Login(credencialesUsuarioDTO);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("registro")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Registrar(RegistrarUsuarioDTO credencialesUsuarioDTO)
        {
            try
            {
                var respuesta = await _usuarioService.RegistrarUsuario(credencialesUsuarioDTO);
                return Ok(respuesta);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("renovar-token")]
        [Authorize]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> RenovarToken()
        {
            try
            {
                return await _usuarioService.RenovarToken();
            }
            catch(InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
