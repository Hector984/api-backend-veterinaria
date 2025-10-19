using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API_Veterinaria.Core.Entities
{
    public class Usuario : IdentityUser, IAuditable
    {
        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required string ApellidoP { get; set; }

        [Required]
        public required string ApellidoM { get; set; }

        [Required]
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
