namespace API_Veterinaria.Core.DTOs.Autenticacion
{
    public class RegistrarUsuarioDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Nombre { get; set; }
        public required string ApellidoP { get; set; }
        public required string ApellidoM { get; set; }
    }
}
