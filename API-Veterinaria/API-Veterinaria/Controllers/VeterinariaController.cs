using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Veterinaria;
using API_Veterinaria.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API_Veterinaria.Controllers
{
    [ApiController]
    [Route("/api/veterinarias")]
    [Authorize]
    public class VeterinariasController : ControllerBase
    {
        private readonly IVeterinariaService _veterinariaService;

        public VeterinariasController(IVeterinariaService veterinariaService)
        {
            _veterinariaService = veterinariaService;
        }

        //GET: VeterinariasController
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las veterinarias", Description = "Devuelve la lista completa de veterinarias.")]
        [ProducesResponseType(typeof(IEnumerable<VeterinariaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ObtenerVeterinarias()
        {
            var veterinariasDTO = await _veterinariaService.ObtenerVeterinariasAsync();

            return Ok(veterinariasDTO);
        }

        // GET: VeterinariasController/5
        [HttpGet("{id:int}", Name = "ObtenerVeterinaria")]
        [SwaggerOperation(Summary = "Obtiene una veterinaria por id", Description = "Parámetro: id (int) identificador de la veterinaria. Respuestas: 200 (OK) con VeterinariaDTO, 404 (No encontrada), 500 (Error interno).")]
        [ProducesResponseType(typeof(VeterinariaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ObtenerVeterinariaPorId(int id)
        {
            try
            {
                var veterinaria = await _veterinariaService.ObtenerVeterinariaPorIdAsync(id);

                return Ok(veterinaria);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("mi-veterinaria")]
        [SwaggerOperation(Summary = "Obtiene la veterinaria del usuario autenticado", Description = "No requiere parámetros. Devuelve la veterinaria asociada al usuario autenticado. Respuestas: 200 (OK) con VeterinariaDTO, 404 si usuario o veterinaria no encontrada.")]
        [ProducesResponseType(typeof(VeterinariaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ObtenerMiVeterinaria()
        {
            try
            {
                var veterinaria = await _veterinariaService.ObtenerVeterinariaPorDuenoIdAsync();

                return Ok(veterinaria);
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

        // POST: VeterinariasController
        [HttpPost]
        [SwaggerOperation(Summary = "Registra una nueva veterinaria", Description = "Crea una veterinaria asociada al usuario autenticado. Parámetro: RegistrarVeterinariaDTO con los datos. Respuestas: 201 (Creado) con VeterinariaDTO, 404 si usuario no encontrado, 500 en caso de error.")]
        [ProducesResponseType(typeof(VeterinariaDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RegistrarVeterinaria(RegistrarVeterinariaDTO registrarVeterinariaDTO)
        {
            try
            {
                var veterinariaDTO = await _veterinariaService.RegistrarVeterinariaAsync(registrarVeterinariaDTO);

                return CreatedAtRoute("ObtenerVeterinaria", new { veterinariaDTO.Id }, veterinariaDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Actualiza una veterinaria", Description = "Actualiza los datos de la veterinaria indicada. Parámetros: id (int) y RegistrarVeterinariaDTO con los nuevos datos. Respuestas: 204 (Sin contenido), 404 si no existe, 403 si no tiene permiso, 500 en error interno.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ActualizarVeterinaria(int id, RegistrarVeterinariaDTO registrarVeterinariaDTO)
        {
            try
            {
                await _veterinariaService.ActualizarVeterinariaAsync(id, registrarVeterinariaDTO);

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
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Activa o desactiva una veterinaria", Description = "Alterna el estado activo de la veterinaria indicada. Parámetro: id (int). Respuestas: 204 en caso de éxito, 403 si no tiene permiso, 404 si no existe, 500 en error interno.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ActivarDesactivarVeterinaria(int id)
        {
            try
            {
                await _veterinariaService.ActivarDesactivarVeterinariaAsync(id);

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
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
        
    }
}
