using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Veterinaria.Data.Repositories
{
    public class ClienteRepository: IClienteRepository
    {
        private readonly VeterinariaDbContext _context;

        public ClienteRepository(VeterinariaDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> GetById(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Cliente?> GetClienteByIdConVeterinaria(int id)
        {
            return await _context.Clientes
                .Include(x => x.Veterinaria)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task Add(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
