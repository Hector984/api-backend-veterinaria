using System.ComponentModel.DataAnnotations;

namespace API_Veterinaria.Core.DTOs.Veterinaria
{
    public class RegistrarVeterinariaDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerida")]
        public required string Nombre { get; set; }
        [Required]
        public required string Direccion { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$",
        ErrorMessage = "El teléfono debe tener 10 dígitos")]
        public required string Telefono { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
