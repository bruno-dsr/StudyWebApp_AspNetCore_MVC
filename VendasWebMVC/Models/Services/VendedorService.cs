using System.Collections.Generic;
using System.Linq;
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

        public List<Vendedor> FindAll()
        {
            return _context.Vendedor.ToList();
        }

        public void Insert(Vendedor obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Vendedor FindByID(int id)
        {
            return _context.Vendedor.Include(v => v.Departamento).FirstOrDefault(v => v.ID == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Vendedor.Find(id);
            _context.Vendedor.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Vendedor obj)
        {
            if(!_context.Vendedor.Any(v => v.ID == obj.ID))
            {
                throw new NotFoundException("ID não encontrado!");
            }

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }

            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }

}
