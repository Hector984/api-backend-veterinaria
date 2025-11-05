using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Cliente;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDTO>> ObtenerCliente([FromRoute] int id)
        {
            try
            {
                var veterinariasDTO = await _clienteService.ObtenerClientePorId(id);

                return Ok(veterinariasDTO);

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);

            }
        }

        [HttpGet("veterinaria/{id:int}")]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> ObtenerClientes([FromRoute] int id)
        {
            try
            {
                var veterinariasDTO = await _clienteService.ObtenerClientesPorVeterinariaId(id);

                return Ok(veterinariasDTO);

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);

            }
            catch (UnauthorizedAccessException ex)
            {
                return NotFound(ex.Message);
            }

            
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> RegistrarCliente(RegistrarClienteDTO dto)
        {
            try
            {
                var clienteDTO = await _clienteService.RegistrarCliente(dto);

                return Ok(clienteDTO);

            }catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);

            }catch (UnauthorizedAccessException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
