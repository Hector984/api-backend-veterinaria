using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Cliente;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using API_Veterinaria.Data.Repositories;
using AutoMapper;
using System.Net.WebSockets;

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

        public async Task<ClienteDTO?> ObtenerClientePorId(int clienteId)
        {
            var cliente = await _clienteRepository.GetById(clienteId);

            if (cliente == null)
            {
                throw new KeyNotFoundException("Cliente no encontrado"); 
            }

            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

            return clienteDTO;
        }

        public async Task<IEnumerable<ClienteDTO>> ObtenerClientesPorVeterinariaId(int veterinariaId)
        {

            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            var veterinaria = await _veterinariaRepository.GetByIdAsync(veterinariaId);

            if (veterinaria is null)
            {
                throw new KeyNotFoundException("Veterinaria no encontrada");
            }

            if (usuario.Id != veterinaria.UsuarioId)
            {
                throw new UnauthorizedAccessException("No tienes permiso para esta acción");
            }

            var clientes = await _clienteRepository.GetAllClientesByVeterinariaId(veterinariaId);

            var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);

            return clientesDTO;
        }

        public async Task<ClienteDTO> RegistrarCliente(RegistrarClienteDTO dto)
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            var veterinaria = await _veterinariaRepository.GetByIdAsync(dto.VeterinariaId);

            if (veterinaria is null)
            {
                throw new KeyNotFoundException("Veterinaria no encontrada");
            }

            if (usuario.Id != veterinaria.UsuarioId)
            {
                throw new UnauthorizedAccessException("No tienes permiso para esta acción");
            }

            var cliente = _mapper.Map<Cliente>(dto);

            await _clienteRepository.Add(cliente);

            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

            return clienteDTO;

        }

        public async Task ActualizarCliente(int clienteId, ActualizarClienteDTO dto)
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            var veterinaria = await _veterinariaRepository.GetByIdAsync(dto.VeterinariaId);

            if (veterinaria is null)
            {
                throw new KeyNotFoundException("Veterinaria no encontrada");
            }

            if (usuario.Id != veterinaria.UsuarioId)
            {
                throw new UnauthorizedAccessException("No tienes permiso para esta acción");
            }

            var clienteDB = await _clienteRepository.GetById(clienteId);

            if (clienteDB is null)
            {
                throw new KeyNotFoundException("Cliente no encontrado");
            }

            if (veterinaria.Id != clienteDB.VeterinariaId)
            {
                throw new UnauthorizedAccessException("No tienes permiso para esta acción");
            }

            var cliente = _mapper.Map(dto, clienteDB);

            await _clienteRepository.Update(cliente);

        }

        public async Task ActualizarEstatusCliente(int clienteId)
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            var clienteDB = await _clienteRepository.GetClienteByIdConVeterinaria(clienteId);

            if (clienteDB is null)
            {
                throw new KeyNotFoundException("Cliente no encontrado");
            }

            if (clienteDB.Veterinaria.UsuarioId != usuario.Id)
            {
                throw new UnauthorizedAccessException("No tienes permiso para esta acción");
            }

            await _clienteRepository.Delete(clienteDB);
        }
    }
}
