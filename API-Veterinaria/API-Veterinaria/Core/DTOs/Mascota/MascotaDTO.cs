using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Core.DTOs.Mascota
{
    public class MascotaDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string Especie { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public int Edad { get; set; }
        public decimal? Peso { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Observaciones { get; set; }
        public bool Activo { get; set; }
        public int ClienteId { get; set; }
        public required string NombreCliente { get; set; }
        public required string TelefonoCliente { get; set; }
    }
}
