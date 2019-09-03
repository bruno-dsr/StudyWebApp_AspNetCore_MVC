using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VendasWebMVC.Models.ViewModels;
using VendasWebMVC.Models.Services;
using VendasWebMVC.Models;
using System.Collections.Generic;
using VendasWebMVC.Models.Services.Exceptions;
using System.Diagnostics;

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
            if (!ModelState.IsValid)
            {
                var departamentos = _departamentoService.FindAll();
                var viewModel = new VendedorFormViewModel() { Departamentos = departamentos, Vendedor = obj.Vendedor };
                return View(viewModel);
            }

            _vendedorService.Insert(obj.Vendedor);
            return RedirectToAction(nameof(Index));
        }

        //O '?' no parâmetro indica que o mesmo é opcional
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não informado." });
            }

            var vend = _vendedorService.FindByID(id.Value);
            if (vend == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não encontrado." });
            }

            else
            {
                return View(vend);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _vendedorService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não informado." });
            }

            var vend = _vendedorService.FindByID(id.Value);
            if (vend == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não encontrado." });
            }

            else
            {
                return View(vend);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não informado." });
            }

            var vend = _vendedorService.FindByID(id.Value);
            if (vend == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não encontrado." }); ;
            }

            else
            {
                List<Departamento> departamentos = _departamentoService.FindAll();
                VendedorFormViewModel viewModel = new VendedorFormViewModel() { Vendedor = vend, Departamentos = departamentos };
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = _departamentoService.FindAll();
                var viewModel = new VendedorFormViewModel() { Departamentos = departamentos, Vendedor = vendedor };
                return View(viewModel);
            }

            if (id != vendedor.ID)
            {
                return RedirectToAction(nameof(Error), new { message = "ID incompatível com o objeto." });
            }

            try
            {
                _vendedorService.Update(vendedor);
                return RedirectToAction(nameof(Index));
            }

            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel()
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}