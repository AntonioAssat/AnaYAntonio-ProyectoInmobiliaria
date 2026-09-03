using Microsoft.AspNetCore.Mvc;
using AnaYAntonio_ProyectoInmobiliaria.Models;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IRepositorioReserva repositorio;
        private readonly IRepositorioInquilino repositorioInquilino;
        private readonly IRepositorioInmueble repositorioInmueble;

        public ReservaController(
            IRepositorioReserva repositorio,
            IRepositorioInquilino repositorioInquilino,
            IRepositorioInmueble repositorioInmueble)
        {
            this.repositorio = repositorio;
            this.repositorioInquilino = repositorioInquilino;
            this.repositorioInmueble = repositorioInmueble;
        }

        public IActionResult Index()
        {
            var lista = repositorio.ObtenerLista();

            ViewBag.Inquilinos = repositorioInquilino.ObtenerLista();
            ViewBag.Inmuebles = repositorioInmueble.ObtenerLista();

            return View(lista);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Inquilinos = repositorioInquilino.ObtenerLista();
            ViewBag.Inmuebles = repositorioInmueble.ObtenerLista();

            return View();
        }

        //create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Inquilinos = repositorioInquilino.ObtenerLista();
                ViewBag.Inmuebles = repositorioInmueble.ObtenerLista();
                return View(reserva);
            }

            // Validación de fechas superpuestas
            if (repositorio.ExisteReservaSuperpuesta(reserva))
            {
                ModelState.AddModelError(
                    "",
                    "El inmueble ya tiene una reserva activa en ese período."
                );

                ViewBag.Inquilinos = repositorioInquilino.ObtenerLista();
                ViewBag.Inmuebles = repositorioInmueble.ObtenerLista();

                return View(reserva);
            }

            reserva.Estado = true;

            repositorio.Alta(reserva);

            TempData["Mensaje"] = "Reserva registrada correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var reserva = repositorio.ObtenerPorId(id);

            if (reserva == null)
            {
                return NotFound();
            }

            ViewBag.Inquilinos = repositorioInquilino.ObtenerLista();
            ViewBag.Inmuebles = repositorioInmueble.ObtenerLista();

            return View(reserva);
        }
        //edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Reserva reserva)
        {
            if (reserva.FechaInicio >= reserva.FechaFin)
            {
                ModelState.AddModelError(
                    "FechaFin",
                    "La fecha de finalización debe ser posterior a la fecha de inicio."
                );
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Inquilinos = repositorioInquilino.ObtenerLista();
                ViewBag.Inmuebles = repositorioInmueble.ObtenerLista();

                return View(reserva);
            }

            // Validar que no exista otra reserva superpuesta
            if (repositorio.ExisteReservaSuperpuesta(reserva, reserva.ID_reserva))
            {
                ModelState.AddModelError(
                    "",
                    "El inmueble ya tiene otra reserva activa en ese período."
                );

                ViewBag.Inquilinos = repositorioInquilino.ObtenerLista();
                ViewBag.Inmuebles = repositorioInmueble.ObtenerLista();

                return View(reserva);
            }

            repositorio.Modificacion(reserva);

            TempData["Mensaje"] = "Reserva modificada correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            repositorio.Baja(id);

            TempData["Mensaje"] = "Reserva dada de baja correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Activate(int id)
        {
            repositorio.AltaEstado(id);

            TempData["Mensaje"] = "Reserva activada correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}