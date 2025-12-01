using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Consulta;
using API_Veterinaria.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API_Veterinaria.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/consultas")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Registra una nueva consulta", Description = "Crea una consulta con los datos indicados en el DTO. Devuelve la consulta creada.")]
        [ProducesResponseType(typeof(ConsultaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConsultaDTO>> RegistarConsulta(RegistrarConsultaDTO dto)
        {
            try
            {
                var consultaDTO = await _consultaService.RegistrarConsultaAsync(dto);

                return consultaDTO;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ForbidenException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
