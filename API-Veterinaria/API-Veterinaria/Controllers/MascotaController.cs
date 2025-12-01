using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Mascota;
using API_Veterinaria.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Obtiene una mascota por id", Description = "Devuelve la mascota indicada por su identificador. Respuestas: 200 (OK) con MascotaDTO, 404 (No encontrado), 500 (Error interno).")]
        [ProducesResponseType(typeof(MascotaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [SwaggerOperation(Summary = "Registra una nueva mascota", Description = "Crea una mascota asociada a un cliente. Parámetro: RegistrarMascotaDTO. Respuestas: 201 (Creado) con MascotaDTO, 404 (No encontrado), 403 (Prohibido), 500 (Error interno).")]
        [ProducesResponseType(typeof(MascotaDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [SwaggerOperation(Summary = "Obtiene mascotas por cliente", Description = "Devuelve las mascotas asociadas al cliente indicado. Respuestas: 200 (OK) con IEnumerable<MascotaDTO>, 404 (No encontrado), 403 (Prohibido), 500 (Error interno).")]
        [ProducesResponseType(typeof(IEnumerable<MascotaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [SwaggerOperation(Summary = "Obtiene mascotas por veterinaria", Description = "Devuelve las mascotas asociadas a la veterinaria indicada. Respuestas: 200 (OK) con IEnumerable<MascotaDTO>, 404 (No encontrado), 409 (Veterinaria no activa), 403 (Prohibido), 500 (Error interno).")]
        [ProducesResponseType(typeof(IEnumerable<MascotaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [SwaggerOperation(Summary = "Actualiza una mascota", Description = "Actualiza los datos de la mascota indicada. Parámetros: id y ActualizarMascotaDTO. Respuestas: 204 (Sin contenido), 404 (No encontrado), 403 (Prohibido), 409 (Recurso no activo), 500 (Error interno).")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [SwaggerOperation(Summary = "Activa o desactiva una mascota", Description = "Alterna el estado activo de la mascota indicada. Parámetro: id. Respuestas: 204 (Sin contenido), 404 (No encontrado), 403 (Prohibido), 409 (Recurso no activo), 500 (Error interno).")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
