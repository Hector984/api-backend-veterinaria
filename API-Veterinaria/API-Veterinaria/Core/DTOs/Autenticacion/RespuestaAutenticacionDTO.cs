namespace API_Veterinaria.Core.DTOs.Autenticacion
{
    public class RespuestaAutenticacionDTO
    {
        public required string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
