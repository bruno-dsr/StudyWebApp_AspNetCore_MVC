using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VendasWebMVC.Models.Services.Exceptions;

namespace VendasWebMVC.Models.Services
{
    public class VendaService
    {
        private readonly VendasWebMVCContext _context;

        public VendaService(VendasWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Venda>> FindAllAsync()
        {
            return await _context.Venda.Include(v => v.Vendedor).ToListAsync();
        }
    }
}
