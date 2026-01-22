using API_Veterinaria.Core.DTOs.Mascota;
using API_Veterinaria.Core.DTOs.Veterinaria;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using AutoMapper.QueryableExtensions;
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

        public async Task<Veterinaria?> ObtenerVeterinariaPorId(int id)
        {
            return await _context.Veterinarias
               .Include(v => v.Clientes)
                    .ThenInclude(c => c.Mascotas)
               .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Veterinaria?> ObtenerVeterinariaPorDuenoId(string userId)
        {
            return await _context.Veterinarias
                .Include(v => v.Clientes)
                    .ThenInclude(c => c.Mascotas)
                .FirstOrDefaultAsync(x => x.UsuarioId == userId);
        }

        public async Task<IEnumerable<Veterinaria>> ObtenerVeterinarias()
        {
            return await _context.Veterinarias.ToListAsync();
        }

        public async Task RegistrarVeterinaria(Veterinaria veterinaria)
        {
            await _context.Veterinarias.AddAsync(veterinaria);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarVeterinaria(Veterinaria veterinaria)
        {
            _context.Veterinarias.Update(veterinaria);
            await _context.SaveChangesAsync();
        }

        public async Task ActivarDesactivarVeterinaria(Veterinaria veterinaria)
        {
            _context.Veterinarias.Remove(veterinaria);
            await _context.SaveChangesAsync();
        }
    }
}
