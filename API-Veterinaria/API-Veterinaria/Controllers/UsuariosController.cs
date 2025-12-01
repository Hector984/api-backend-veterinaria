using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Autenticacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Inicia sesión", Description = "Valida las credenciales del usuario y devuelve un token de autenticación.")]
        [ProducesResponseType(typeof(RespuestaAutenticacionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [SwaggerOperation(Summary = "Registra un usuario", Description = "Crea un nuevo usuario y devuelve datos de autenticación.")]
        [ProducesResponseType(typeof(RespuestaAutenticacionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [SwaggerOperation(Summary = "Renueva token", Description = "Renueva el token del usuario autenticado y devuelve los nuevos datos de autenticación.")]
        [ProducesResponseType(typeof(RespuestaAutenticacionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
