using System.ComponentModel.DataAnnotations;

namespace API_Veterinaria.Core.DTOs.Mascota
{
    public class RegistrarMascotaDTO
    {
        private DateTime _fechaNacimiento;

        [Required(ErrorMessage ="El campo {0} es requerido.")]
        public required string Nombre { get; set; }
        public string Especie { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        [Range(0.01, 100, ErrorMessage ="El peso debe estar entre 0.01 g y 100 kg")]
        public decimal? Peso { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateTime FechaNacimiento {
            get => _fechaNacimiento;
            // DateTime.SpecifyKind le dice a EF Core: "Confía en mí, esto es UTC"
            set => _fechaNacimiento = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        [StringLength(500)]
        public string? Observaciones { get; set; }
        public int ClienteId { get; set; }
        public int VeterinariaId { get; set; }
    }
}
