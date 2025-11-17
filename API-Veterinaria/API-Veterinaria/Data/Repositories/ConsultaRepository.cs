using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;

namespace API_Veterinaria.Data.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly VeterinariaDbContext _context;

        public ConsultaRepository(VeterinariaDbContext context)
        {
            _context = context;
        }

        public async Task Add(Consulta consulta)
        {
            await _context.Consultas.AddAsync(consulta);

            await _context.SaveChangesAsync();
        }
    }
}
