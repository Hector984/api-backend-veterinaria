namespace API_Veterinaria.Core.DTOs.Veterinaria
{
    public class VeterinariaDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Direccion { get; set; }
        public required string Telefono { get; set; }
        public string? Email { get; set; }
    }
}
