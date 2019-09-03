using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VendasWebMVC.Models.ViewModels;
using VendasWebMVC.Models.Services;
using VendasWebMVC.Models;
using System.Collections.Generic;
using VendasWebMVC.Models.Services.Exceptions;
using System.Diagnostics;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var list = await _vendedorService.FindAllAsync();
            return View(list);
        }

        //Todos os actions são por padrão GET
        public async Task<IActionResult> Create()
        {
            var dep = await _departamentoService.FindAllAsync();
            var viewModel = new VendedorFormViewModel() { Departamentos = dep };
            return View(viewModel);
        }

        //Notação para POST
        [HttpPost]
        //Anti Forgery Token, contra ataques via sessão autenticada
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendedorFormViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel() { Departamentos = departamentos, Vendedor = obj.Vendedor };
                return View(viewModel);
            }

            await _vendedorService.InsertAsync(obj.Vendedor);
            return RedirectToAction(nameof(Index));
        }

        //O '?' no parâmetro indica que o mesmo é opcional
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não informado." });
            }

            var vend = await _vendedorService.FindByIDAsync(id.Value);
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
        public async Task<IActionResult> Delete(int id)
        {
            await _vendedorService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não informado." });
            }

            var vend = await _vendedorService.FindByIDAsync(id.Value);
            if (vend == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não encontrado." });
            }

            else
            {
                return View(vend);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não informado." });
            }

            var vend = _vendedorService.FindByIDAsync(id.Value);
            if (vend == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não encontrado." }); ;
            }

            else
            {
                var departamentos = await _departamentoService.FindAllAsync();
                VendedorFormViewModel viewModel = new VendedorFormViewModel() { Vendedor = vend.Result, Departamentos = departamentos };
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel() { Departamentos = departamentos, Vendedor = vendedor };
                return View(viewModel);
            }

            if (id != vendedor.ID)
            {
                return RedirectToAction(nameof(Error), new { message = "ID incompatível com o objeto." });
            }

            try
            {
                await _vendedorService.UpdateAsync(vendedor);
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