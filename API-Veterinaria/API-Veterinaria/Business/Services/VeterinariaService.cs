using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Veterinaria;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using API_Veterinaria.Exceptions;
using AutoMapper;

namespace API_Veterinaria.Business.Services
{
    public class VeterinariaService : IVeterinariaService
    {
        private readonly IVeterinariaRepository _veterinariaRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public VeterinariaService(IVeterinariaRepository veterinariaRepository, IUsuarioService usuarioService, IMapper mapper)
        {
            _veterinariaRepository = veterinariaRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public async Task<VeterinariaDTO> RegistrarVeterinariaAsync(RegistrarVeterinariaDTO registrarVeterinariaDTO)
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            var veterinaria = _mapper.Map<Veterinaria>(registrarVeterinariaDTO);

            veterinaria.UsuarioId = usuario.Id;

            await _veterinariaRepository.RegistrarVeterinaria(veterinaria);

            var veterinariaDTO = _mapper.Map<VeterinariaDTO>(veterinaria);

            return veterinariaDTO;
        }

        public async Task<IEnumerable<VeterinariaDTO>> ObtenerVeterinariasAsync()
        {
            var veterinarias = await _veterinariaRepository.ObtenerVeterinarias();

            var veterinariasDTO = _mapper.Map<IEnumerable<VeterinariaDTO>>(veterinarias);

            return veterinariasDTO;
        }

        public async Task<VeterinariaDTO> ObtenerVeterinariaPorIdAsync(int id)
        {
            var veterinaria = await _veterinariaRepository.ObtenerVeterinariaPorId(id);

            if (veterinaria is null)
            {
                throw new NotFoundException("Veterinaria no encontrada");
            }

            var veterinariaDTO = _mapper.Map<VeterinariaDTO>(veterinaria);

            return veterinariaDTO;

        }

        public async Task<VeterinariaDTO> ObtenerVeterinariaPorDuenoIdAsync()
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            var veterinaria = await _veterinariaRepository.ObtenerVeterinariaPorDuenoId(usuario.Id);

            var veterinariaDTO = _mapper.Map<VeterinariaDTO>(veterinaria);

            return veterinariaDTO;
        }

        public async Task ActualizarVeterinariaAsync(int id, RegistrarVeterinariaDTO registrarVeterinariaDTO)
        {

            // revisar que el usuario esta autenticado y que esta registrado en la base de datos

            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            // Verificamos que la veterinaria existe
            var veterinariaExiste = await _veterinariaRepository.ObtenerVeterinariaPorId(id);

            if (veterinariaExiste is null)
            {
                throw new NotFoundException("La veterinaria no existe");
            }

            // Verificamos que la veterinaria le pertenezca al usuario que esta actualizando la ínfo
            if(usuario.Id != veterinariaExiste.UsuarioId)
            {
                throw new ForbidenException("No tienes permiso para actualizar la veterinaria");
            }

            var veterinaria = _mapper.Map(registrarVeterinariaDTO, veterinariaExiste);

            await _veterinariaRepository.ActualizarVeterinaria(veterinaria);
            
        }

        public async Task ActivarDesactivarVeterinariaAsync(int id)
        {
            // revisar que el usuario esta autenticado y que esta registrado en la base de datos

            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            // Verificamos que la veterinaria existe
            var veterinariaExiste = await _veterinariaRepository.ObtenerVeterinariaPorId(id);

            if (veterinariaExiste is null)
            {
                throw new NotFoundException("La veterinaria no existe");
            }

            // Verificamos que la veterinaria le pertenezca al usuario que esta actualizando la ínfo
            if (usuario.Id != veterinariaExiste.UsuarioId)
            {
                throw new ForbidenException("No tienes permiso para realizar esta acción");
            }

            await _veterinariaRepository.ActivarDesactivarVeterinaria(veterinariaExiste);
        }
    }
}
