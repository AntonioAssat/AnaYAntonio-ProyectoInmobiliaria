using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AnaYAntonio_ProyectoInmobiliaria.Models;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        private readonly IRepositorioPago repositorioPago;
        private readonly IRepositorioReserva repositorioReserva;

        public PagoController(
            IRepositorioPago repositorioPago,
            IRepositorioReserva repositorioReserva)
        {
            this.repositorioPago = repositorioPago;
            this.repositorioReserva = repositorioReserva;
        }

        // LISTADO DE PAGOS
        public IActionResult Index()
        {
            var lista = repositorioPago.ObtenerLista();

            ViewBag.Reservas = repositorioReserva.ObtenerLista();

            return View(lista);
        }

        // MOSTRAR FORMULARIO
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Reservas = repositorioReserva.ObtenerLista();

            return View();
        }

        // GUARDAR PAGO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pago pago)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Reservas = repositorioReserva.ObtenerLista();

                return View(pago);
            }

            pago.Estado = true;

            repositorioPago.Alta(pago);

            TempData["Mensaje"] = "Pago registrado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // MOSTRAR FORMULARIO DE MODIFICACIÓN
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var pago = repositorioPago.ObtenerPorId(id);

            if (pago == null)
            {
                return NotFound();
            }

            ViewBag.Reservas = repositorioReserva.ObtenerLista();

            return View(pago);
        }

        // GUARDAR MODIFICACIÓN
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int ID_pago, string Concepto)
        {
            if (string.IsNullOrWhiteSpace(Concepto))
            {
                ModelState.AddModelError("Concepto", "El concepto es obligatorio.");

                var pagoError = repositorioPago.ObtenerPorId(ID_pago);

                if (pagoError == null)
                {
                    return NotFound();
                }

                pagoError.Concepto = Concepto;

                return View(pagoError);
            }

            var pago = new Pago
            {
                ID_pago = ID_pago,
                Concepto = Concepto
            };

            repositorioPago.Modificacion(pago);

            TempData["Mensaje"] = "Pago modificado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // DAR DE BAJA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            repositorioPago.Baja(id);

            TempData["Mensaje"] = "Pago dado de baja correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // DAR DE ALTA NUEVAMENTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Activate(int id)
        {
            repositorioPago.AltaEstado(id);

            TempData["Mensaje"] = "Pago activado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}