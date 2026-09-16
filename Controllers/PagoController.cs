using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AnaYAntonio_ProyectoInmobiliaria.Models;
using System.Security.Claims;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        private readonly IRepositorioPago repositorioPago;
        private readonly IRepositorioReserva repositorioReserva;
        private readonly IRepositorioAuditoria repositorioAuditoria;

        public PagoController(
            IRepositorioPago repositorioPago,
            IRepositorioReserva repositorioReserva,
            IRepositorioAuditoria repositorioAuditoria)
        {
            this.repositorioPago = repositorioPago;
            this.repositorioReserva = repositorioReserva;
            this.repositorioAuditoria = repositorioAuditoria;
        }

        // LISTADO DE PAGOS
        public IActionResult Index(string? buscar, int pagina = 1)
        {
            int cantidadPorPagina = 10;

            if (pagina < 1)
                pagina = 1;

            int cantidadTotal = repositorioPago.ObtenerCantidad(buscar);

            int cantidadPaginas = (int)Math.Ceiling(
                cantidadTotal / (double)cantidadPorPagina
            );

            if (cantidadPaginas > 0 && pagina > cantidadPaginas)
                pagina = cantidadPaginas;

            var lista = repositorioPago.ObtenerListaPaginada(
                buscar,
                pagina,
                cantidadPorPagina
            );

            ViewBag.Reservas = repositorioReserva.ObtenerLista();

            ViewBag.Buscar = buscar;
            ViewBag.PaginaActual = pagina;
            ViewBag.CantidadPaginas = cantidadPaginas;
            ViewBag.CantidadTotal = cantidadTotal;

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

            var idPago = repositorioPago.Alta(pago);

            var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(claimId, out int idUsuario))
            {
                var auditoria = new Auditoria
                {
                    ID_usuario = idUsuario,
                    Entidad = "Pago",
                    ID_entidad = idPago,
                    Accion = "Creación",
                    Fecha = DateTime.Now
                };

                repositorioAuditoria.Alta(auditoria);
            }

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

        // MOSTRAR DETALLE DEL PAGO
        [HttpGet]
        public IActionResult Details(int id)
        {
            var pago = repositorioPago.ObtenerPorId(id);

            if (pago == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Administrador"))
            {
                ViewBag.Auditoria =
                    repositorioAuditoria.ObtenerPorEntidad("Pago", id);
            }

            return View(pago);
        }

        // DAR DE BAJA
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var pago = repositorioPago.ObtenerPorId(id);

            if (pago == null)
            {
                return NotFound();
            }

            var resultado = repositorioPago.Baja(id);

            if (resultado > 0)
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(claimId, out int idUsuario))
                {
                    var auditoria = new Auditoria
                    {
                        ID_usuario = idUsuario,
                        Entidad = "Pago",
                        ID_entidad = id,
                        Accion = "Anulación",
                        Fecha = DateTime.Now
                    };

                    repositorioAuditoria.Alta(auditoria);
                }
            }

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