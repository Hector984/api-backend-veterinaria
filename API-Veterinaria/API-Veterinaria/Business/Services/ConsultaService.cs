using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Consulta;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using AutoMapper;

namespace API_Veterinaria.Business.Services
{
    public class ConsultaService: IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMascotaRepository _mascotaRepository;
        private readonly IVeterinariaRepository _veterinariaRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public ConsultaService(IConsultaRepository consultaRepository, IMascotaRepository mascotaRepository, IVeterinariaRepository veterinariaRepository,
            IUsuarioService usuarioService, IMapper mapper)
        {
            _consultaRepository = consultaRepository;
            _mascotaRepository = mascotaRepository;
            _veterinariaRepository = veterinariaRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public async Task<ConsultaDTO> RegistrarConsulta(RegistrarConsultaDTO dto)
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            var mascotaDB = await _mascotaRepository.GetById(dto.MascotaId);

            if (mascotaDB is null)
            {
                throw new KeyNotFoundException("Mascota no encontrada");
            }

            var veteriariaDB = await _veterinariaRepository.GetByIdAsync(mascotaDB.Cliente.VeterinariaId);

            if (veteriariaDB is null)
            {
                throw new KeyNotFoundException("Veterinaria no encontrada");
            }

            // Validamos que la veterinaria pertenezca al veterinario
            if (veteriariaDB.UsuarioId != usuario!.Id)
            {
                throw new UnauthorizedAccessException("No puedes registrar la consulta en esta veterinaria");
            }

            // Validamos que la mascota pertenezca a la veterinaria
            if (mascotaDB.Cliente.VeterinariaId != veteriariaDB!.Id)
            {
                throw new UnauthorizedAccessException("No puedes registrar la consulta en esta veterinaria");
            }


            var consulta = _mapper.Map<Consulta>(dto);

            consulta.VeterinariaId = veteriariaDB.Id;
            consulta.UsuarioId = usuario!.Id;

            await _consultaRepository.Add(consulta);

            var consultaDTO = _mapper.Map<ConsultaDTO>(consulta);

            return consultaDTO;

        }
    }
}
