using API_Veterinaria.Core.Entities;

namespace API_Veterinaria.Data.Interfaces
{
    public interface IConsultaRepository
    {
        Task Add(Consulta consulta);
    }
}
