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
            ViewBag.Propietarios = repositorioPropietario.ObtenerLista()
                .Where(p => p.Estado)
                .ToList();

            ViewBag.Tipos = repositorioTipo.ObtenerLista()
                .Where(t => t.Estado)
                .ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            Inmueble inmueble,
            int DuenioId,
            int TipoId)
        {
            ModelState.Remove("Duenio");
            ModelState.Remove("Duenio.DNI");
            ModelState.Remove("Duenio.Mail");
            ModelState.Remove("Duenio.Nombre");
            ModelState.Remove("Duenio.Apellido");
            ModelState.Remove("Duenio.Telefono");

            ModelState.Remove("Tipo");
            ModelState.Remove("Tipo.Nombre");

            if (!ModelState.IsValid)
            {
                ViewBag.Propietarios = repositorioPropietario.ObtenerLista()
                    .Where(p => p.Estado)
                    .ToList();

                ViewBag.Tipos = repositorioTipo.ObtenerLista()
                    .Where(t => t.Estado)
                    .ToList();

                return View(inmueble);
            }

            var propietario = repositorioPropietario.ObtenerPorId(DuenioId);
            var tipo = repositorioTipo.ObtenerPorId(TipoId);

            if (propietario == null || !propietario.Estado ||
                tipo == null || !tipo.Estado)
            {
                ViewBag.Propietarios = repositorioPropietario.ObtenerLista()
                    .Where(p => p.Estado)
                    .ToList();

                ViewBag.Tipos = repositorioTipo.ObtenerLista()
                    .Where(t => t.Estado)
                    .ToList();

                ModelState.AddModelError(
                    "",
                    "El propietario o el tipo de inmueble seleccionado no es válido.");

                return View(inmueble);
            }

            inmueble.Duenio = propietario;
            inmueble.Tipo = tipo;
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

            ViewBag.Propietarios = repositorioPropietario.ObtenerLista()
                .Where(p => p.Estado)
                .ToList();

            ViewBag.Tipos = repositorioTipo.ObtenerLista()
                .Where(t => t.Estado)
                .ToList();

            return View(inmueble);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            Inmueble inmueble,
            int DuenioId,
            int TipoId)
        {
            ModelState.Remove("Duenio");
            ModelState.Remove("Duenio.DNI");
            ModelState.Remove("Duenio.Mail");
            ModelState.Remove("Duenio.Nombre");
            ModelState.Remove("Duenio.Apellido");
            ModelState.Remove("Duenio.Telefono");

            ModelState.Remove("Tipo");
            ModelState.Remove("Tipo.Nombre");

            if (!ModelState.IsValid)
            {
                ViewBag.Propietarios = repositorioPropietario.ObtenerLista()
                    .Where(p => p.Estado)
                    .ToList();

                ViewBag.Tipos = repositorioTipo.ObtenerLista()
                    .Where(t => t.Estado)
                    .ToList();

                return View(inmueble);
            }

            var inmuebleExistente = repositorio.ObtenerPorId(inmueble.ID_inmueble);

            if (inmuebleExistente == null)
            {
                return NotFound();
            }

            var propietario = repositorioPropietario.ObtenerPorId(DuenioId);
            var tipo = repositorioTipo.ObtenerPorId(TipoId);

            if (propietario == null || !propietario.Estado ||
                tipo == null || !tipo.Estado)
            {
                ViewBag.Propietarios = repositorioPropietario.ObtenerLista()
                    .Where(p => p.Estado)
                    .ToList();

                ViewBag.Tipos = repositorioTipo.ObtenerLista()
                    .Where(t => t.Estado)
                    .ToList();

                ModelState.AddModelError(
                    "",
                    "El propietario o el tipo de inmueble seleccionado no es válido.");

                return View(inmueble);
            }

            inmuebleExistente.Duenio = propietario;
            inmuebleExistente.Tipo = tipo;
            inmuebleExistente.Direccion = inmueble.Direccion;
            inmuebleExistente.Cupo = inmueble.Cupo;
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