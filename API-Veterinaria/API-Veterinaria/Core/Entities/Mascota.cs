namespace API_Veterinaria.Core.Entities
{
    public class Mascota : IAuditable
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string Especie { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public int Edad { get; set; }
        public decimal? Peso { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Observaciones { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        // IAuditable interface
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
