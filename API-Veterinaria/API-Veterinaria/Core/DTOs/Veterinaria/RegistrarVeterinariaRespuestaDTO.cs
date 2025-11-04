using API_Veterinaria.Core.DTOs.Autenticacion;

namespace API_Veterinaria.Core.DTOs.Veterinaria
{
    public class RegistrarVeterinariaRespuestaDTO
    {
        public VeterinariaDTO Veterinaria { get; set; } = default!;
        public RespuestaAutenticacionDTO Autenticacion { get; set; } = default!;
    }
}
