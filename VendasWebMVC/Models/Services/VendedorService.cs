﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;

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
    }

}