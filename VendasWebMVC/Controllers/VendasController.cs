using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMVC.Models.Services;

namespace VendasWebMVC.Controllers
{
    public class VendasController : Controller
    {
        private readonly VendaService _vendaService;

        public VendasController(VendaService vendaService)
        {
            _vendaService = vendaService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _vendaService.FindAllAsync();
            return View(list);
        }

        public IActionResult SimpleSearchIndex()
        {
            return View();
        }

        public IActionResult GroupingSearchIndex()
        {
            return View();
        }

        public IActionResult SimpleSearch()
        {
            return View();
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}