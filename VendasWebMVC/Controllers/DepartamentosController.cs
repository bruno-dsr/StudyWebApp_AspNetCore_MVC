using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMVC.Models;

namespace VendasWebMVC.Controllers
{
    public class DepartamentosController : Controller
    {
        public IActionResult Index()
        {
            List<Departamento> departamentos = new List<Departamento>();

            departamentos.Add(new Departamento(1, "Eletrônicos"));
            departamentos.Add(new Departamento(2, "Moda"));

            return View(departamentos);
        }
    }
}