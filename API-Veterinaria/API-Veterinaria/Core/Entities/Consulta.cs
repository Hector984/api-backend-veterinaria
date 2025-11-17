using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Veterinaria.Core.Entities
{
    public class Consulta : IAuditable
    {
        public int Id { get; set; }

        public required int MascotaId { get; set; }

        public required int VeterinariaId { get; set; }

        public required string UsuarioId { get; set; }

        public required string Motivo { get; set; }

        public string? Sintomas { get; set; }

        public string? Diagnostico { get; set; }

        public string? Tratamiento { get; set; }

        public string? Observaciones { get; set; }

        public decimal? Costo { get; set; }

        // IAuditable interface
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
