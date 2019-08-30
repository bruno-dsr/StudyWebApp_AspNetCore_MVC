using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMVC.Models;
using VendasWebMVC.Models.Services;

namespace VendasWebMVC.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;

        public VendedoresController(VendedorService vendedorService)
        {
            _vendedorService = vendedorService;
        }

        public IActionResult Index()
        {
            var list = _vendedorService.FindAll();
            return View(list);
        }

        //Todos os actions são por padrão GET
        public IActionResult Create()
        {
            return View();
        }

        //Notação para POST
        [HttpPost]
        //Anti Forgery Token, contra ataques via sessão autenticada
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor obj)
        {
            _vendedorService.Insert(obj);
            return RedirectToAction(nameof(Index));
        }
    }
}