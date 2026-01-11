using System.ComponentModel.DataAnnotations;

namespace API_Veterinaria.Core.Entities
{
    public class Veterinaria : IAuditable
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string Telefono { get; set; }
        public required string? Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        // IAuditable interface
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaBaja { get; set; }

        // Propiedades de navegacion
        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
