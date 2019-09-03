using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VendasWebMVC.Models.Services.Exceptions;

namespace VendasWebMVC.Models.Services
{
    public class VendedorService
    {
        private readonly VendasWebMVCContext _context;

        public VendedorService(VendasWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Vendedor>> FindAllAsync()
        {
            return await _context.Vendedor.ToListAsync();
        }

        public async Task InsertAsync(Vendedor obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Vendedor> FindByIDAsync(int id)
        {
            return await _context.Vendedor.Include(v => v.Departamento).FirstOrDefaultAsync(v => v.ID == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = _context.Vendedor.Find(id);
                _context.Vendedor.Remove(obj);
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Vendedor obj)
        {
            if(!await _context.Vendedor.AnyAsync(v => v.ID == obj.ID))
            {
                throw new NotFoundException("ID não encontrado!");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }

            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }

}
