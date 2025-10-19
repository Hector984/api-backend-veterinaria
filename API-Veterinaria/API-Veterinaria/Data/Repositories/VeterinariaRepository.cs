using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Veterinaria.Data.Repositories
{
    public class VeterinariaRepository : IVeterinariaRepository
    {
        private readonly VeterinariaDbContext _context;

        public VeterinariaRepository(VeterinariaDbContext context)
        {
            _context = context;
        }

        public async Task<Veterinaria> GetByIdAsync(int id)
        {
            return await _context.Veterinarias.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Veterinaria>> GetAllAsync()
        {
            return await _context.Veterinarias.ToListAsync();
        }

        public async Task AddAync(Veterinaria veterinaria)
        {
            await _context.Veterinarias.AddAsync(veterinaria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Veterinaria veterinaria)
        {
            _context.Veterinarias.Update(veterinaria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Veterinaria veterinaria)
        {
            _context.Veterinarias.Remove(veterinaria);
            await _context.SaveChangesAsync();
        }
    }
}
