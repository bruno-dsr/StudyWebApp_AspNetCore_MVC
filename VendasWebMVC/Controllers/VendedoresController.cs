using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMVC.Models;
using VendasWebMVC.Models.ViewModels;
using VendasWebMVC.Models.Services;

namespace VendasWebMVC.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController(VendedorService vendedorService, DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }

        public IActionResult Index()
        {
            var list = _vendedorService.FindAll();
            return View(list);
        }

        //Todos os actions são por padrão GET
        public IActionResult Create()
        {
            var dep = _departamentoService.FindAll();
            var viewModel = new VendedorFormViewModel() { Departamentos = dep };
            return View(viewModel);
        }

        //Notação para POST
        [HttpPost]
        //Anti Forgery Token, contra ataques via sessão autenticada
        [ValidateAntiForgeryToken]
        public IActionResult Create(VendedorFormViewModel obj)
        {
            _vendedorService.Insert(obj.Vendedor);
            return RedirectToAction(nameof(Index));
        }
    }
}