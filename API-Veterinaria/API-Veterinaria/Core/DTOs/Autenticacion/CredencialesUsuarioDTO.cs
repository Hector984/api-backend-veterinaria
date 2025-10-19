using System.ComponentModel.DataAnnotations;

namespace API_Veterinaria.Core.DTOs.Autenticacion
{
    public class CredencialesUsuarioDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
