namespace API_Veterinaria.Core.Entities
{
    public interface IAuditable
    {
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
