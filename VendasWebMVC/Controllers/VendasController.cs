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

        public async Task<IActionResult> SimpleSearch(DateTime? dataInicial, DateTime? dataFinal)
        {
            if (!dataInicial.HasValue)
            {
                dataInicial = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!dataFinal.HasValue)
            {
                dataFinal = DateTime.Now.Date;
            }

            ViewData["dataInicial"] = dataInicial.Value.ToString("yyyy-MM-dd");
            ViewData["dataFinal"] = dataFinal.Value.ToString("yyyy-MM-dd");

            var list = await _vendaService.FindByDateAsync(dataInicial, dataFinal);
            return View(list);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? dataInicial, DateTime? dataFinal)
        {
            if (!dataInicial.HasValue)
            {
                dataInicial = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!dataFinal.HasValue)
            {
                dataFinal = DateTime.Now.Date;
            }

            ViewData["dataInicial"] = dataInicial.Value.ToString("yyyy-MM-dd");
            ViewData["dataFinal"] = dataFinal.Value.ToString("yyyy-MM-dd");

            var list = await _vendaService.FindByDateGroupingAsync(dataInicial, dataFinal);
            return View(list);
        }
    }
}