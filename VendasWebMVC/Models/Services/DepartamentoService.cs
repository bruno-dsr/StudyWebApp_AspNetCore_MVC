using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace VendasWebMVC.Models.Services
{
    public class DepartamentoService
    {
        private readonly VendasWebMVCContext _context;

        public DepartamentoService(VendasWebMVCContext context)
        {
            _context = context;
        }

        public List<Departamento> FindAll()
        {
            return _context.Departamento.OrderBy(d => d.Nome).ToList();
        }
    }
}
