using API_Veterinaria.Core.DTOs.Mascota;

namespace API_Veterinaria.Core.DTOs.Veterinaria
{
    public class VeterinariaDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Direccion { get; set; }
        public required string Telefono { get; set; }
        public string? Email { get; set; }
        public bool Activo { get; set; }
        public List<MascotaDTO> Mascotas { get; set; } = new List<MascotaDTO>();
    }
}
