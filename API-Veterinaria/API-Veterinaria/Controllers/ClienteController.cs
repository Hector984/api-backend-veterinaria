using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Cliente;
using API_Veterinaria.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API_Veterinaria.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/clientes")]
    public class ClienteController: ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("{id:int}", Name ="ObtenerCliente")]
        [SwaggerOperation(Summary = "Obtiene un cliente por id", Description = "Devuelve el cliente indicado por su identificador. Respuestas: 200 (OK) con ClienteDTO, 404 (No encontrado), 500 (Error interno).")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClienteDTO>> ObtenerCliente([FromRoute] int id)
        {
            try
            {
                var veterinariasDTO = await _clienteService.ObtenerClientePorIdAsync(id);

                return Ok(veterinariasDTO);

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("veterinaria/{id:int}")]
        [SwaggerOperation(Summary = "Obtiene clientes por veterinaria", Description = "Devuelve los clientes asociados a la veterinaria indicada. Respuestas: 200 (OK) con IEnumerable<ClienteDTO>, 404 (No encontrado), 403 (Prohibido), 500 (Error interno).")]
        [ProducesResponseType(typeof(IEnumerable<ClienteDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> ObtenerClientesPorVeterinariaId([FromRoute] int id)
        {
            try
            {
                var veterinariasDTO = await _clienteService.ObtenerClientesPorVeterinariaIdAsync(id);

                return Ok(veterinariasDTO);

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);

            }
            catch (ForbidenException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }


        }

        [HttpPost]
        [SwaggerOperation(Summary = "Registra un nuevo cliente", Description = "Crea un cliente asociado a una veterinaria. Parámetro: RegistrarClienteDTO. Respuestas: 201 (Creado) con ClienteDTO, 404 (No encontrado), 403 (Prohibido), 500 (Error interno).")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClienteDTO>> RegistrarCliente(RegistrarClienteDTO dto)
        {
            try
            {
                var clienteDTO = await _clienteService.RegistrarClienteAsync(dto);

                return CreatedAtRoute("ObtenerCliente", new { clienteDTO.Id }, clienteDTO);

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);

            }catch (ForbidenException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Actualiza un cliente", Description = "Actualiza los datos del cliente indicado. Parámetros: id y ActualizarClienteDTO. Respuestas: 204 (Sin contenido), 404 (No encontrado), 403 (Prohibido), 500 (Error interno).")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ActualizarCliente(int id, ActualizarClienteDTO dto)
        {
            try
            {
                await _clienteService.ActualizarClienteAsync(id, dto);

                return NoContent();
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
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Activa o desactiva un cliente", Description = "Alterna el estado activo del cliente indicado. Parámetro: id. Respuestas: 204 (Sin contenido), 404 (No encontrado), 403 (Prohibido), 500 (Error interno).")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ActivarDesactivarCliente(int id)
        {
            try
            {
                await _clienteService.ActivarDesactivarClienteAsync(id);

                return NoContent();
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
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
