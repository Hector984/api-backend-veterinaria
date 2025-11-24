using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Cliente;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using API_Veterinaria.Exceptions;
using AutoMapper;

namespace API_Veterinaria.Business.Services
{
    public class ClienteService: IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        private readonly IVeterinariaRepository _veterinariaRepository;

        public ClienteService(IClienteRepository clienteRepository, IUsuarioService usuarioService, IVeterinariaRepository veterinariaRepository,
            IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _usuarioService = usuarioService;
            _veterinariaRepository = veterinariaRepository;
            _mapper = mapper;
        }

        public async Task<ClienteDTO?> ObtenerClientePorIdAsync(int clienteId)
        {
            var cliente = await _clienteRepository.ObtenerClientePorId(clienteId);

            if (cliente == null)
            {
                throw new NotFoundException("Cliente no encontrado"); 
            }

            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

            return clienteDTO;
        }

        public async Task<IEnumerable<ClienteDTO>> ObtenerClientesPorVeterinariaIdAsync(int veterinariaId)
        {

            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            var veterinaria = await _veterinariaRepository.ObtenerVeterinariaPorId(veterinariaId);

            if (veterinaria is null)
            {
                throw new NotFoundException("Veterinaria no encontrada");
            }

            if (usuario.Id != veterinaria.UsuarioId)
            {
                throw new ForbidenException("No tienes permiso para esta acción");
            }

            var clientes = await _clienteRepository.ObtenerClientesPorVeterinariaId(veterinariaId);

            var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);

            return clientesDTO;
        }

        public async Task<ClienteDTO> RegistrarClienteAsync(RegistrarClienteDTO dto)
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            var veterinaria = await _veterinariaRepository.ObtenerVeterinariaPorId(dto.VeterinariaId);

            if (veterinaria is null)
            {
                throw new NotFoundException("Veterinaria no encontrada");
            }

            if (usuario.Id != veterinaria.UsuarioId)
            {
                throw new ForbidenException("No tienes permiso para esta acción");
            }

            var cliente = _mapper.Map<Cliente>(dto);

            await _clienteRepository.ReggistrarCliente(cliente);

            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

            return clienteDTO;

        }

        public async Task ActualizarClienteAsync(int clienteId, ActualizarClienteDTO dto)
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            var veterinaria = await _veterinariaRepository.ObtenerVeterinariaPorId(dto.VeterinariaId);

            if (veterinaria is null)
            {
                throw new NotFoundException("Veterinaria no encontrada");
            }

            if (usuario.Id != veterinaria.UsuarioId)
            {
                throw new ForbidenException("No tienes permiso para esta acción");
            }

            var clienteDB = await _clienteRepository.ObtenerClientePorId(clienteId);

            if (clienteDB is null)
            {
                throw new NotFoundException("Cliente no encontrado");
            }

            if (veterinaria.Id != clienteDB.VeterinariaId)
            {
                throw new ForbidenException("No tienes permiso para esta acción");
            }

            var cliente = _mapper.Map(dto, clienteDB);

            await _clienteRepository.ActualizarCliente(cliente);

        }

        public async Task ActivarDesactivarClienteAsync(int clienteId)
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            var clienteDB = await _clienteRepository.ObtenerClientePorIdConVeterinaria(clienteId);

            if (clienteDB is null)
            {
                throw new NotFoundException("Cliente no encontrado");
            }

            if (clienteDB.Veterinaria.UsuarioId != usuario.Id)
            {
                throw new ForbidenException("No tienes permiso para esta acción");
            }

            await _clienteRepository.ActivarDesactivar(clienteDB);
        }
    }
}
