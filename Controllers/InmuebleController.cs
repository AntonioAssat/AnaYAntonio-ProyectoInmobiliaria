using AnaYAntonio_ProyectoInmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    public class InmueblesController : Controller
    {
        private readonly IRepositorioInmueble repositorio;
        private readonly IRepositorioPropietario repositorioPropietario;
        private readonly IRepositorioTipoInmueble repositorioTipo;

        public InmueblesController(
            IRepositorioInmueble repositorio,
            IRepositorioPropietario repositorioPropietario,
            IRepositorioTipoInmueble repositorioTipo)
        {
            this.repositorio = repositorio;
            this.repositorioPropietario = repositorioPropietario;
            this.repositorioTipo = repositorioTipo;
        }

        public IActionResult Index()
        {
            var lista = repositorio.ObtenerLista();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Propietarios = repositorioPropietario.ObtenerLista();
            ViewBag.Tipos = repositorioTipo.ObtenerLista();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inmueble inmueble)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Propietarios = repositorioPropietario.ObtenerLista();
                ViewBag.Tipos = repositorioTipo.ObtenerLista();

                return View(inmueble);
            }

            inmueble.Estado = true;

            repositorio.Alta(inmueble);

            TempData["Mensaje"] = "Inmueble creado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var inmueble = repositorio.ObtenerPorId(id);

            if (inmueble == null)
            {
                return NotFound();
            }

            ViewBag.Propietarios = repositorioPropietario.ObtenerLista();
            ViewBag.Tipos = repositorioTipo.ObtenerLista();

            return View(inmueble);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Inmueble inmueble)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Propietarios = repositorioPropietario.ObtenerLista();
                ViewBag.Tipos = repositorioTipo.ObtenerLista();

                return View(inmueble);
            }

            var inmuebleExistente = repositorio.ObtenerPorId(inmueble.ID_inmueble);

            if (inmuebleExistente == null)
            {
                return NotFound();
            }

            inmuebleExistente.Duenio = inmueble.Duenio;
            inmuebleExistente.Direccion = inmueble.Direccion;
            inmuebleExistente.Cupo = inmueble.Cupo;
            inmuebleExistente.Tipo = inmueble.Tipo;
            inmuebleExistente.Coordenadas = inmueble.Coordenadas;
            inmuebleExistente.PrecioPorDia = inmueble.PrecioPorDia;
            inmuebleExistente.PorcentajeReserva = inmueble.PorcentajeReserva;

            repositorio.Modificacion(inmuebleExistente);

            TempData["Mensaje"] = "Inmueble modificado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Baja(int id)
        {
            repositorio.Baja(id);

            TempData["Mensaje"] = "Inmueble dado de baja correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AltaEstado(int id)
        {
            repositorio.AltaEstado(id);

            TempData["Mensaje"] = "Inmueble dado de alta correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}