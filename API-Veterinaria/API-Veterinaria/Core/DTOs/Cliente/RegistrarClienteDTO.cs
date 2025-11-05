using System.ComponentModel.DataAnnotations;

namespace API_Veterinaria.Core.DTOs.Cliente
{
    public class RegistrarClienteDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "La dirección de correo es invalida")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string Telefono { get; set; }

        [Required]
        public required int VeterinariaId { get; set; }
    }
}
