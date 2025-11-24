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

        public async Task<Cliente?> ObtenerClientePorId(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Cliente?> ObtenerClientePorIdConVeterinaria(int id)
        {
            return await _context.Clientes
                .Include(x => x.Veterinaria)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientesPorVeterinariaId(int veterinariaId)
        {
            return await _context.Clientes
                .Where(x => x.VeterinariaId == veterinariaId)
                .ToListAsync();
        }

        public async Task ReggistrarCliente(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task ActivarDesactivar(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
