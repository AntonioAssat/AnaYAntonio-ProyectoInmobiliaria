using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AnaYAntonio_ProyectoInmobiliaria.Models;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    [Authorize(Policy = "Administrador")]
    public class InformesController : Controller
    {
        private readonly IRepositorioInmueble repositorioInmueble;
        private readonly IRepositorioPropietario repositorioPropietario;
        private readonly IRepositorioInformes repositorioInformes;

        public InformesController(
            IRepositorioInmueble repositorioInmueble,
            IRepositorioPropietario repositorioPropietario,
            IRepositorioInformes repositorioInformes)
        {
            this.repositorioInmueble = repositorioInmueble;
            this.repositorioPropietario = repositorioPropietario;
            this.repositorioInformes = repositorioInformes;
        }


        // =========================================================
        // PÁGINA PRINCIPAL
        // =========================================================

        public IActionResult Index()
        {
            var inmuebles = repositorioInmueble.ObtenerLista();
            var propietarios = repositorioPropietario.ObtenerLista();

            ViewBag.Propietarios = propietarios;

            return View(inmuebles);
        }


        // =========================================================
        // INFORME 2
        // Inmuebles de un propietario
        // =========================================================

        [HttpGet]
        public IActionResult Informe2(int idPropietario)
        {
            var inmuebles =
                repositorioInformes.ObtenerInmueblesPorPropietario(
                    idPropietario
                );

            return Json(inmuebles.Select(i => new
            {
                id = i.ID_inmueble,
                direccion = i.Direccion,
                cupo = i.Cupo,
                precioPorDia = i.PrecioPorDia,
                estado = i.Estado,
                tipo = i.Tipo?.Nombre ?? "",
                propietario = i.Duenio != null
                    ? $"{i.Duenio.Nombre} {i.Duenio.Apellido}"
                    : ""
            }));
        }


        // =========================================================
        // INFORME 3
        // Inmuebles más reservados últimos 365 días
        // =========================================================

        [HttpGet]
        public IActionResult Informe3()
        {
            var desde = DateTime.Today.AddDays(-365);

            var inmuebles =
                repositorioInformes.ObtenerInmueblesMasReservados(
                    desde
                );

            return Json(inmuebles.Select(i => new
            {
                id = i.ID_inmueble,
                direccion = i.Direccion,
                cupo = i.Cupo,
                precioPorDia = i.PrecioPorDia,
                tipo = i.Tipo?.Nombre ?? "",
                propietario = i.Duenio != null
                    ? $"{i.Duenio.Nombre} {i.Duenio.Apellido}"
                    : ""
            }));
        }


        // =========================================================
        // INFORME 4
        // Inmuebles sin reservas durante X días
        // =========================================================

        [HttpGet]
        public IActionResult Informe4(int dias)
        {
            if (dias <= 0)
            {
                return BadRequest(
                    "La cantidad de días debe ser mayor a 0."
                );
            }

            var desde = DateTime.Today.AddDays(-dias);

            var inmuebles =
                repositorioInformes.ObtenerInmueblesSinReservas(
                    desde
                );

            return Json(inmuebles.Select(i => new
            {
                id = i.ID_inmueble,
                direccion = i.Direccion,
                cupo = i.Cupo,
                precioPorDia = i.PrecioPorDia,
                estado = i.Estado,
                tipo = i.Tipo?.Nombre ?? "",
                propietario = i.Duenio != null
                    ? $"{i.Duenio.Nombre} {i.Duenio.Apellido}"
                    : ""
            }));
        }


        // =========================================================
        // INFORME 5
        // Reservas vigentes entre dos fechas
        // =========================================================

        [HttpGet]
        public IActionResult Informe5(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                return BadRequest(
                    "La fecha de inicio no puede ser posterior a la fecha de fin."
                );
            }

            var reservas =
                repositorioInformes.ObtenerReservasVigentes(
                    fechaInicio,
                    fechaFin
                );

            return Json(reservas.Select(r => new
            {
                id = r.ID_reserva,
                idInquilino = r.ID_inquilino,
                idInmueble = r.ID_inmueble,
                fechaInicio = r.FechaInicio.ToString("yyyy-MM-dd"),
                fechaFin = r.FechaFin.ToString("yyyy-MM-dd"),
                montoPorDia = r.MontoPorDia,
                estado = r.Estado
            }));
        }


        // =========================================================
        // INFORME 6
        // Reservas que terminan dentro de X días
        // =========================================================

        [HttpGet]
        public IActionResult Informe6(int dias)
        {
            if (dias <= 0)
            {
                return BadRequest(
                    "La cantidad de días debe ser mayor a 0."
                );
            }

            var hasta = DateTime.Today.AddDays(dias);

            var reservas =
                repositorioInformes.ObtenerReservasQueTerminan(
                    hasta
                );

            return Json(reservas.Select(r => new
            {
                id = r.ID_reserva,
                idInquilino = r.ID_inquilino,
                idInmueble = r.ID_inmueble,
                fechaInicio = r.FechaInicio.ToString("yyyy-MM-dd"),
                fechaFin = r.FechaFin.ToString("yyyy-MM-dd"),
                montoPorDia = r.MontoPorDia,
                estado = r.Estado
            }));
        }


        // =========================================================
        // INFORME 7
        // Pagos de una reserva
        // =========================================================

        [HttpGet]
        public IActionResult Informe7(int idReserva)
        {
            var pagos =
                repositorioInformes.ObtenerPagosPorReserva(
                    idReserva
                );

            return Json(pagos.Select(p => new
            {
                id = p.ID_pago,
                idReserva = p.ID_reserva,
                concepto = p.Concepto,
                fechaPago = p.FechaPago.ToString("yyyy-MM-dd"),
                monto = p.Monto,
                estado = p.Estado
            }));
        }
        [HttpGet]
        public IActionResult BuscarReservas(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return Json(new List<object>());
            }

            var reservas = repositorioInformes.BuscarReservas(texto.Trim());

            return Json(reservas.Select(r => new
            {
                id = r.ID_reserva,
                idInquilino = r.ID_inquilino,
                idInmueble = r.ID_inmueble,
                fechaInicio = r.FechaInicio.ToString("yyyy-MM-dd"),
                fechaFin = r.FechaFin.ToString("yyyy-MM-dd"),
                montoPorDia = r.MontoPorDia,
                estado = r.Estado
            }));
        }

        // =========================================================
        // INFORME 8
        // Inmuebles disponibles entre dos fechas
        // =========================================================

        [HttpGet]
        public IActionResult Informe8(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            if (fechaInicio >= fechaFin)
            {
                return BadRequest(
                    "La fecha de inicio debe ser anterior a la fecha de fin."
                );
            }

            var inmuebles =
                repositorioInformes.ObtenerInmueblesDisponibles(
                    fechaInicio,
                    fechaFin
                );

            return Json(inmuebles.Select(i => new
            {
                id = i.ID_inmueble,
                direccion = i.Direccion,
                cupo = i.Cupo,
                precioPorDia = i.PrecioPorDia,
                estado = i.Estado,
                tipo = i.Tipo?.Nombre ?? "",
                propietario = i.Duenio != null
                    ? $"{i.Duenio.Nombre} {i.Duenio.Apellido}"
                    : ""
            }));
        }
    }
}