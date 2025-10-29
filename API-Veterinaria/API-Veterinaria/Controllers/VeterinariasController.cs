using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Veterinaria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> GetVeterinarias()
        {
            var veterinariasDTO = await _veterinariaService.GetVeterinariasAsync();

            return Ok(veterinariasDTO);
        }

        // GET: VeterinariasController/5
        [HttpGet("{id:int}", Name = "ObtenerVeterinaria")]
        public async Task<ActionResult> GetVeterinaria(int id)
        {
            var veterinaria = await _veterinariaService.GetVeterinariaByIdAsync(id);

            if (veterinaria is null)
            {
                return NotFound();
            }

            return Ok(veterinaria);
        }

        [HttpGet("mi-veterinaria")]
        public async Task<ActionResult> GetVeterinariaUsuario()
        {
            var veterinaria = await _veterinariaService.GetVeterinariaUsuarioAsync();

            return Ok(veterinaria);
        }

        // POST: VeterinariasController
        [HttpPost]
        public async Task<ActionResult> PostVeterinaria(RegistrarVeterinariaDTO registrarVeterinariaDTO)
        {

            var veterinariaDTO = await _veterinariaService.PostVeterinariaAsync(registrarVeterinariaDTO);

            return CreatedAtRoute("ObtenerVeterinaria", new { veterinariaDTO.Id }, veterinariaDTO); 
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateVeterinariaAsync(int id, RegistrarVeterinariaDTO registrarVeterinariaDTO)
        {
            try
            {
                await _veterinariaService.UpdateVeterinariaAsync(id, registrarVeterinariaDTO);

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
        public async Task<ActionResult> DeleteVeterinariaAsync(int id)
        {
            try
            {
                await _veterinariaService.DeleteVeterinariaAsync(id);

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
