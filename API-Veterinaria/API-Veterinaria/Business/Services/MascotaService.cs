using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Mascota;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using AutoMapper;

namespace API_Veterinaria.Business.Services
{
    public class MascotaService: IMascotaService
    {
        protected readonly IMascotaRepository _mascotaRepository;
        private readonly IMapper _mapper;
        private readonly IVeterinariaRepository _veterinariaRepository;
        private readonly IUsuarioService _usuarioService;

        public MascotaService(IMascotaRepository mascotaRepository, IMapper mapper, 
            IVeterinariaRepository veterinariaRepository, IUsuarioService usuarioService)
        {
            _mascotaRepository = mascotaRepository;
            _mapper = mapper;
            _veterinariaRepository = veterinariaRepository;
            _usuarioService = usuarioService;
        }

        public async Task<MascotaDTO> ObtenerMascotaPorId(int id)
        {
            // Revisar que la mascota pertenezca a la veterinaria desde la que se hace la consulta
            var mascota = await _mascotaRepository.GetById(id);

            if (mascota is null)
            {
                throw new KeyNotFoundException("Mascota no encontrada");
            }

            var mascotaDTO = _mapper.Map<MascotaDTO>(mascota);

            return mascotaDTO;
        }

        public async Task<IEnumerable<MascotaDTO>> ObtenerMascotasPorClienteId(int clienteId)
        {
            //Revisar que el cliente pertenece a la veterinaria desde la que se hace la consulta
            var mascotas = await _mascotaRepository.GetAllByClienteId(clienteId);

            var mascotasDTO = _mapper.Map<IEnumerable<MascotaDTO>>(mascotas);

            return mascotasDTO;
        }

        public async Task<IEnumerable<MascotaDTO>> ObtenerMascotasPorVeterinariaId(int veterinariaId)
        {
            
            var usuario = await _usuarioService.GetUsuarioByEmail();

            if (usuario is null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            var veterinaria = await _veterinariaRepository.GetByIdAsync(veterinariaId);

            if (veterinaria is null)
            {
                throw new KeyNotFoundException("La veterinaria no existe");
            }

            // Revisar que el usuario que hace la peticion sea el dueño de la veterinaria
            if (usuario.Id != veterinaria.UsuarioId)
            {
                throw new InvalidOperationException("No tienes acceso a estos datos");
            }

            var mascotas = await _mascotaRepository.GetAllByVeterinariaId(veterinariaId);

            var mascotasDTO = _mapper.Map<IEnumerable<MascotaDTO>>(mascotas);

            return mascotasDTO;

        }

        public async Task<MascotaDTO> RegistrarMascota(RegistrarMascotaDTO dto)
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();
            
            var veterinaria = await _veterinariaRepository.GetByIdAsync(dto.VeterinariaId);

            // Validamos que la veterinaria exista
            if (veterinaria == null)
            {
                throw new KeyNotFoundException("Veterinaria no encontrada");
            }

            // Validamos que la veterinaria pertenezca al usuario
            if (veterinaria.UsuarioId != usuario!.Id)
            {
                throw new UnauthorizedAccessException("No puedes registrar mascotas en esta veterinaria");
            }

            // Validamos que el cliente pertenezca a esta veterinaria

            var mascota = _mapper.Map<Mascota>(dto);

            await _mascotaRepository.Add(mascota);

            var mascotaDTO = _mapper.Map<MascotaDTO>(mascota);

            return mascotaDTO;
                
        }

        public async Task ActualizarInformacionMascota(int mascotaId, ActualizarMascotaDTO actualizarMascotaDTO)
        {

            var usuario = await _usuarioService.GetUsuarioByEmail();

            var veterinaria = await _veterinariaRepository.GetByIdAsync(actualizarMascotaDTO.VeterinariaId);

            // Validamos que la veterinaria exista
            if (veterinaria == null)
            {
                throw new KeyNotFoundException("Veterinaria no encontrada");
            }

            // Validamos que la veterinaria pertenezca al usuario
            if (veterinaria.UsuarioId != usuario!.Id)
            {
                throw new UnauthorizedAccessException("No tienes permiso");
            }

            // validamos que la mascota exista
            var mascotaDB = await _mascotaRepository.GetByIdConClienteYVeterinaria(mascotaId);

            if (mascotaDB is null)
            {
                throw new KeyNotFoundException("Mascota no encontrada");
            }

            if (!mascotaDB.Activo)
            {
                throw new InvalidOperationException("No puedes actualizar los datos si la mascota inactiva");
            }

            // Validamos que el cliente este activo
            if (!mascotaDB.Cliente.Activo)
            {
                throw new InvalidOperationException("No puedes actualizar los datos si el dueño esta inactivo");
            }

            var mascota = _mapper.Map(actualizarMascotaDTO, mascotaDB);

            await _mascotaRepository.Update(mascota);

        }

        public async Task ActualizarEstatusMascota(int mascotaId)
        {
            var usuario = await _usuarioService.GetUsuarioByEmail();

            // validamos que la mascota exista
            var mascota = await _mascotaRepository.GetByIdConClienteYVeterinaria(mascotaId);

            if (mascota is null)
            {
                throw new KeyNotFoundException("Mascota no encontrada");
            }

            // Validamos que la veterinaria pertenezca al usuario
            if (mascota.Cliente.Veterinaria.UsuarioId != usuario!.Id)
            {
                throw new UnauthorizedAccessException("No tienes permiso");
            }

            // Validamos que el cliente este activo
            if (!mascota.Cliente.Activo)
            {
                throw new InvalidOperationException("No puedes activar/desactivar una mascota si el dueño esta inactivo");
            }

            await _mascotaRepository.Delete(mascota);
        }
    }
}
