using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        //O '?' no parâmetro indica que o mesmo é opcional
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var vend = _vendedorService.FindByID(id.Value);
            if(vend == null)
            {
                return NotFound();
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
                return NotFound();
            }

            var vend = _vendedorService.FindByID(id.Value);
            if (vend == null)
            {
                return NotFound();
            }

            else
            {
                return View(vend);
            }
        }
    }
}