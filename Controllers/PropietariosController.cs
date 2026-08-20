using Microsoft.AspNetCore.Mvc;
using AnaYAntonio_ProyectoInmobiliaria.Models;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    public class PropietariosController : Controller
    {
        private readonly IRepositorioPropietario repositorio;

        public PropietariosController(IRepositorioPropietario repositorio)
        {
            this.repositorio = repositorio;
        }

        public IActionResult Index()
        {
            var lista = repositorio.ObtenerLista();

            return View(lista);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Propietario propietario)
        {
            if (!ModelState.IsValid)
            {
                return View(propietario);
            }

            propietario.Estado = true;

            repositorio.Alta(propietario);

            return RedirectToAction(nameof(Index));
        }
    }



}
