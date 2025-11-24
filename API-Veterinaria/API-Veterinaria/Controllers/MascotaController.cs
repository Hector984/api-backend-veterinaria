using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Business.Services;
using API_Veterinaria.Core.DTOs.Mascota;
using API_Veterinaria.Exceptions;
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
            catch (NotFoundException ex)
            {
                return NotFound();
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
            catch (NotFoundException ex)
            {
                return NotFound();
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

        [HttpGet("cliente/{id:int}")]
        public async Task<ActionResult<IEnumerable<MascotaDTO>>> ObtenerMascotasPorCliente(int id)
        {
            try
            {
                var mascotasDTO = await _mascotaService.ObtenerMascotasPorClienteId(id);

                return Ok(mascotasDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (ForbidenException ex)
            {
                return Forbid();
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
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (NotActiveException ex)
            {
                return Conflict();
            }
            catch (ForbidenException ex)
            {
                return Forbid();
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
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ForbidenException ex)
            {
                return Forbid();
            }
            catch (NotActiveException ex)
            {
                // Regresa un 409
                return Conflict();
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
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (ForbidenException ex)
            {
                return Forbid();
            }
            catch (NotActiveException ex)
            {
                // Regresa un 409
                return Conflict();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

    }
}
