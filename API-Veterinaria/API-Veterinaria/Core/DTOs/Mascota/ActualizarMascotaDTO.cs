using System.ComponentModel.DataAnnotations;

namespace API_Veterinaria.Core.DTOs.Mascota
{
    public class ActualizarMascotaDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public required string Nombre { get; set; }
        public string Especie { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;

        [Range(0.01, 100, ErrorMessage = "El peso debe estar entre 0.01 g y 100 kg")]
        public decimal? Peso { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(500)]
        public string? Observaciones { get; set; }

        public int VeterinariaId { get; set; }
    }
}
