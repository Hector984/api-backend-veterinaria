using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Business.Services;
using API_Veterinaria.Core.DTOs.Mascota;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Veterinaria.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/mascotas")]
    public class MascotaController: ControllerBase
    {

        private readonly IMascotaService _mascotaService;

        public MascotaController(IMascotaService mascotaService)
        {
            _mascotaService = mascotaService;
        }

        [HttpGet("{id:int}", Name ="ObtenerMascota")]
        public async Task<ActionResult<MascotaDTO>> ObtenerMascota(int id)
        {
            try
            {
                var mascotaDTO = await _mascotaService.ObtenerMascotaPorId(id);

                return Ok(mascotaDTO);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<MascotaDTO>> RegistrarMascota(RegistrarMascotaDTO dto)
        {
            try
            {
                var mascotaDTO = await _mascotaService.RegistrarMascota(dto);

                return CreatedAtRoute("ObtenerMascota", new { mascotaDTO.Id }, mascotaDTO);
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

        [HttpGet("cliente/{id:int}")]
        public async Task<ActionResult<IEnumerable<MascotaDTO>>> ObtenerMascotasPorCliente(int id)
        {
            try
            {
                var mascotasDTO = await _mascotaService.ObtenerMascotasPorClienteId(id);

                return Ok(mascotasDTO);
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
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("veterinaria/{id:int}")]
        public async Task<ActionResult<IEnumerable<MascotaDTO>>> ObtenerMascotasPorVeterinaria(int id)
        {
            try
            {
                var mascotasDTO = await _mascotaService.ObtenerMascotasPorVeterinariaId(id);

                return Ok(mascotasDTO);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Forbid(ex.Message);
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<IEnumerable<MascotaDTO>>> ActualizarMascota(int id, ActualizarMascotaDTO dto)
        {
            try
            {
                await _mascotaService.ActualizarInformacionMascota(id, dto);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Regresa un 409
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> ActivarDesactivarMascota(int id)
        {
            try
            {
                await _mascotaService.ActualizarEstatusMascota(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Regresa un 409
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

    }
}
