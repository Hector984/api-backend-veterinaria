
using API_Veterinaria.Core.DTOs.Consulta;

namespace API_Veterinaria.Business.Interfaces
{
    public interface IConsultaService
    {
        Task<ConsultaDTO> RegistrarConsultaAsync(RegistrarConsultaDTO dto);
    }
}
