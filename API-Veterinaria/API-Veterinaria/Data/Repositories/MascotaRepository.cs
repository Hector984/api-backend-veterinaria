using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Veterinaria.Data.Repositories
{
    public class MascotaRepository: IMascotaRepository
    {
        protected readonly VeterinariaDbContext _context;
        public MascotaRepository(VeterinariaDbContext context)
        {
            _context = context;
        }

        public async Task<Mascota> GetByIdAsync(int id)
        {
            return await _context.Mascotas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Mascota>> GetAllByClienteIdAsync(int clienteId)
        {
            return await _context.Mascotas.Where(x => x.ClienteId == clienteId).ToListAsync();
        }

        public async Task<IEnumerable<Mascota>> GetAllByVeterinariaAsync(int veterinariaId) 
        {
            return await _context.Mascotas
                .Where(x => x.Cliente.VeterinariaId == veterinariaId)
                .Include(x => x.Cliente)
                .ToListAsync();
        }

        public async Task AddAync(Mascota mascota) 
        {
            await _context.Mascotas.AddAsync(mascota);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Mascota mascota) 
        {
            _context.Mascotas.Update(mascota);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Mascota mascota) 
        {
            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();
        }
    }
}
