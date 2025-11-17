using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Veterinaria.Core.DTOs.Consulta
{
    public class RegistrarConsultaDTO
    {
        [Required(ErrorMessage = "El campo MascotaId es obligatorio.")]
        public int MascotaId { get; set; }

        [Required(ErrorMessage = "El motivo de la consulta es obligatorio.")]
        [StringLength(255, ErrorMessage = "El motivo no puede exceder los 255 caracteres.")]
        public required string Motivo { get; set; }

        public string? Sintomas { get; set; }

        public string? Diagnostico { get; set; }

        public string? Tratamiento { get; set; }

        public string? Observaciones { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        [Range(0, 9999999999.99, ErrorMessage = "El costo debe ser un valor positivo.")]
        public decimal? Costo { get; set; }
    }
}
