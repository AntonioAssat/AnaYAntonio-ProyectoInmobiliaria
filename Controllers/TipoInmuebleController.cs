using AnaYAntonio_ProyectoInmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    public class TipoInmueblesController : Controller
    {
        private readonly IRepositorioTipoInmueble repositorio;

        public TipoInmueblesController(IRepositorioTipoInmueble repositorio)
        {
            this.repositorio = repositorio;
        }

        public IActionResult Index()
        {
            var lista = repositorio.ObtenerLista();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TipoInmueble tipo)
        {
            if (!ModelState.IsValid)
            {
                return View(tipo);
            }

            tipo.Estado = true;

            repositorio.Alta(tipo);

            TempData["Mensaje"] = "Tipo de inmueble creado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var tipo = repositorio.ObtenerPorId(id);

            if (tipo == null)
            {
                return NotFound();
            }

            return View(tipo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TipoInmueble tipo)
        {
            if (!ModelState.IsValid)
            {
                return View(tipo);
            }

            var tipoExistente = repositorio.ObtenerPorId(tipo.ID_tipo);

            if (tipoExistente == null)
            {
                return NotFound();
            }

            tipoExistente.Nombre = tipo.Nombre;

            repositorio.Modificacion(tipoExistente);

            TempData["Mensaje"] = "Tipo de inmueble modificado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Baja(int id)
        {
            repositorio.Baja(id);

            TempData["Mensaje"] = "Tipo de inmueble dado de baja correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AltaEstado(int id)
        {
            repositorio.AltaEstado(id);

            TempData["Mensaje"] = "Tipo de inmueble dado de alta correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}