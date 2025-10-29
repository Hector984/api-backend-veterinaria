using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Veterinaria;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

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

        public async Task<VeterinariaDTO> PostVeterinariaAsync(RegistrarVeterinariaDTO registrarVeterinariaDTO)
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }

            var veterinaria = _mapper.Map<Veterinaria>(registrarVeterinariaDTO);

            veterinaria.UsuarioId = usuario.Id;

            await _veterinariaRepository.AddAync(veterinaria);

            var veterinariaDTO = _mapper.Map<VeterinariaDTO>(veterinaria);

            return veterinariaDTO;
        }

        public async Task DeleteAsync(Veterinaria veterinaria)
        {
            await _veterinariaRepository.DeleteAsync(veterinaria);
        }

        public async Task<IEnumerable<VeterinariaDTO>> GetVeterinariasAsync()
        {
            var veterinarias = await _veterinariaRepository.GetAllAsync();

            var veterinariasDTO = _mapper.Map<IEnumerable<VeterinariaDTO>>(veterinarias);

            return veterinariasDTO;
        }

        public async Task<VeterinariaDTO> GetVeterinariaByIdAsync(int id)
        {
            var veterinaria = await _veterinariaRepository.GetByIdAsync(id);

            var veterinariaDTO = _mapper.Map<VeterinariaDTO>(veterinaria);

            return veterinariaDTO;

        }

        public async Task<VeterinariaDTO> GetVeterinariaUsuarioAsync()
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new UnauthorizedAccessException("Usuario no encontrado");
            }

            var veterinaria = await _veterinariaRepository.GetByUserIdAsync(usuario.Id);

            var veterinariaDTO = _mapper.Map<VeterinariaDTO>(veterinaria);

            return veterinariaDTO;
        }

        public async Task UpdateVeterinariaAsync(int id, RegistrarVeterinariaDTO registrarVeterinariaDTO)
        {

            // revisar que el usuario esta autenticado y que esta registrado en la base de datos

            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new UnauthorizedAccessException("Usuario no encontrado");
            }

            // Verificamos que la veterinaria existe
            var veterinariaExiste = await _veterinariaRepository.GetByIdAsync(id);

            if (veterinariaExiste is null)
            {
                throw new KeyNotFoundException("La veterinaria no existe");
            }

            // Verificamos que la veterinaria le pertenezca al usuario que esta actualizando la ínfo
            if(usuario.Id != veterinariaExiste.UsuarioId)
            {
                throw new UnauthorizedAccessException("No tienes permiso para actualizar la veterinaria");
            }

            var veterinaria = _mapper.Map(registrarVeterinariaDTO, veterinariaExiste);

            await _veterinariaRepository.UpdateAsync(veterinaria);
            
        }

        public async Task DeleteVeterinariaAsync(int id)
        {
            // revisar que el usuario esta autenticado y que esta registrado en la base de datos

            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new UnauthorizedAccessException("Usuario no encontrado");
            }

            // Verificamos que la veterinaria existe
            var veterinariaExiste = await _veterinariaRepository.GetByIdAsync(id);

            if (veterinariaExiste is null)
            {
                throw new KeyNotFoundException("La veterinaria no existe");
            }

            // Verificamos que la veterinaria le pertenezca al usuario que esta actualizando la ínfo
            if (usuario.Id != veterinariaExiste.UsuarioId)
            {
                throw new UnauthorizedAccessException("No tienes permiso para realizar esta acción");
            }

            await _veterinariaRepository.DeleteAsync(veterinariaExiste);
        }
    }
}
