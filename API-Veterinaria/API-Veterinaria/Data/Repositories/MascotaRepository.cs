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

        public async Task<Mascota?> GetById(int id)
        {
            return await _context.Mascotas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Mascota>> GetAllByClienteId(int clienteId)
        {
            return await _context.Mascotas.Where(x => x.ClienteId == clienteId).ToListAsync();
        }

        public async Task<IEnumerable<Mascota>> GetAllByVeterinariaId(int veterinariaId) 
        {
            return await _context.Mascotas
                .Where(x => x.Cliente.VeterinariaId == veterinariaId)
                .Include(x => x.Cliente)
                .ToListAsync();
        }

        public async Task<Mascota?> GetByIdConClienteYVeterinaria(int mascotaId)
        {
            return await _context.Mascotas
                .Include(m => m.Cliente)
                    .ThenInclude(m => m.Veterinaria)
                .FirstOrDefaultAsync(m => m.Id == mascotaId);
        }

        public async Task Add(Mascota mascota) 
        {
            await _context.Mascotas.AddAsync(mascota);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Mascota mascota) 
        {
            _context.Mascotas.Update(mascota);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Mascota mascota) 
        {
            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();
        }
    }
}
