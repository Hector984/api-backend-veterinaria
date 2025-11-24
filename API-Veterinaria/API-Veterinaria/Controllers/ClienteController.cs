using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Cliente;
using API_Veterinaria.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
