namespace API_Veterinaria.Core.DTOs.Cliente
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string Email { get; set; } = string.Empty;
        public required string Telefono { get; set; }
        public bool Activo { get; set; }
    }
}
