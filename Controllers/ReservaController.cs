using Microsoft.AspNetCore.Mvc;
using AnaYAntonio_ProyectoInmobiliaria.Models;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IRepositorioReserva repositorio;

        public ReservaController(IRepositorioReserva repositorio)
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
        public IActionResult Create(Reserva reserva)
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
                return View(reserva);
            }

            reserva.Estado = true;

            repositorio.Alta(reserva);

            TempData["Mensaje"] = "Reserva registrada correctamente.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var reserva = repositorio.ObtenerPorId(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        [HttpPost]
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
                return View(reserva);
            }

            repositorio.Modificacion(reserva);

            TempData["Mensaje"] = "Reserva modificada correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            repositorio.Baja(id);

            TempData["Mensaje"] = "Reserva dada de baja correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Activate(int id)
        {
            repositorio.AltaEstado(id);

            TempData["Mensaje"] = "Reserva activada correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}