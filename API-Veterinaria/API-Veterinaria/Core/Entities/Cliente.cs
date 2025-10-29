namespace API_Veterinaria.Core.Entities
{
    public class Cliente : IAuditable
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string ApellidoPaterno { get; set; }
        public required string ApellidoMaterno { get; set; }
        public string Email { get; set; } = string.Empty;
        public required string Telefono { get; set; }
        public required int VeterinariaId { get; set; }
        public required Veterinaria Veterinaria { get; set; }

        // IAuditable interface
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
