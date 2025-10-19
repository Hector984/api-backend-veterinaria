using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Veterinaria;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
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

        public async Task UpdateAsync(Veterinaria veterinaria)
        {
            await _veterinariaRepository.UpdateAsync(veterinaria);
        }
    }
}
