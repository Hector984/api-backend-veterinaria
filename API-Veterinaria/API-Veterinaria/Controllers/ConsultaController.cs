using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Consulta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<ConsultaDTO>> RegistarConsulta(RegistrarConsultaDTO dto)
        {
            try
            {
                var consultaDTO = await _consultaService.RegistrarConsulta(dto);

                return consultaDTO;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
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
