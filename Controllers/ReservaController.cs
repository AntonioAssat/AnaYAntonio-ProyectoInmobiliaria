using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AnaYAntonio_ProyectoInmobiliaria.Models;
using System.Security.Claims;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    [Authorize]
    public class ReservaController : Controller
    {
        private readonly IRepositorioReserva repositorio;
        private readonly IRepositorioInquilino repositorioInquilino;
        private readonly IRepositorioInmueble repositorioInmueble;
        private readonly IRepositorioPago repositorioPago;

        private readonly IRepositorioAuditoria repositorioAuditoria;

        public ReservaController(
            IRepositorioReserva repositorio,
            IRepositorioInquilino repositorioInquilino,
            IRepositorioInmueble repositorioInmueble,
            IRepositorioPago repositorioPago,
            IRepositorioAuditoria repositorioAuditoria)
        {
            this.repositorio = repositorio;
            this.repositorioInquilino = repositorioInquilino;
            this.repositorioInmueble = repositorioInmueble;
            this.repositorioPago = repositorioPago;
            this.repositorioAuditoria = repositorioAuditoria;
        }

        public IActionResult Index(string? buscar, int pagina = 1)
{
    int cantidadPorPagina = 10;

    if (pagina < 1)
        pagina = 1;

    int cantidadTotal = repositorio.ObtenerCantidad(buscar);

    int cantidadPaginas = (int)Math.Ceiling(
        cantidadTotal / (double)cantidadPorPagina
    );

    if (cantidadPaginas > 0 && pagina > cantidadPaginas)
        pagina = cantidadPaginas;

    var lista = repositorio.ObtenerListaPaginada(
        buscar,
        pagina,
        cantidadPorPagina
    );

    // Se mantienen para mostrar los datos de inquilinos e inmuebles
    // en la vista.
    ViewBag.Inquilinos = repositorioInquilino.ObtenerLista();
    ViewBag.Inmuebles = repositorioInmueble.ObtenerLista();

    // Auditoría solamente para administradores
    if (User.IsInRole("Administrador"))
    {
        var auditorias = new Dictionary<int, IList<Auditoria>>();

        foreach (var reserva in lista)
        {
            auditorias[reserva.ID_reserva] =
                repositorioAuditoria.ObtenerPorEntidad(
                    "Reserva",
                    reserva.ID_reserva
                );
        }

        ViewBag.Auditorias = auditorias;
    }

    ViewBag.Buscar = buscar;
    ViewBag.PaginaActual = pagina;
    ViewBag.CantidadPaginas = cantidadPaginas;
    ViewBag.CantidadTotal = cantidadTotal;

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
            // Validar que la fecha de inicio sea anterior a la fecha de finalización
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
            // OBTENER EL INMUEBLE Y SU PORCENTAJE DE RESERVA

            var inmueble = repositorioInmueble.ObtenerPorId(reserva.ID_inmueble);

            if (inmueble == null)
            {
                ModelState.AddModelError(
                    "ID_inmueble",
                    "El inmueble seleccionado no existe."
                );

                ViewBag.Inquilinos = repositorioInquilino.ObtenerLista();
                ViewBag.Inmuebles = repositorioInmueble.ObtenerLista();

                return View(reserva);
            }

            // Usamos el precio real del inmueble
            reserva.MontoPorDia = inmueble.PrecioPorDia;
            // CALCULAR EL IMPORTE TOTAL DE LA RESERVA

            var cantidadDias =
                (reserva.FechaFin - reserva.FechaInicio).Days;

            var importeTotal =
                cantidadDias * reserva.MontoPorDia;

            // CALCULAR EL PAGO INICIAL SEGÚN EL PORCENTAJE
  
            var importeReserva =
                importeTotal * inmueble.PorcentajeReserva / 100m;
            // GUARDAR LA RESERVA
            reserva.Estado = true;

            var idReserva = repositorio.Alta(reserva);

            // REGISTRAR EL PAGO INICIAL

            var pago = new Pago
            {
                ID_reserva = idReserva,
                FechaPago = DateTime.Today,
                Monto = importeReserva,
                Concepto =
                    $"Pago inicial de reserva ({inmueble.PorcentajeReserva}%)",
                Estado = true
            };

            repositorioPago.Alta(pago);

            // REGISTRAR AUDITORÍA DE LA RESERVA

            var idUsuario = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            repositorioAuditoria.Alta(new Auditoria
            {
                ID_usuario = idUsuario,
                Entidad = "Reserva",
                ID_entidad = idReserva,
                Accion = "CREACION",
                Fecha = DateTime.Now
            });

            TempData["Mensaje"] =
                $"Reserva registrada correctamente. " +
                $"Pago inicial: ${importeReserva:N2}.";

            return RedirectToAction(nameof(Index));
        }

        // RENOVAR / EXTENDER RESERVA
        // La reserva original NO se modifica.
        // Se crea una nueva reserva.


        [HttpGet]
        public IActionResult Renovar(int id)
        {
            var reserva = repositorio.ObtenerPorId(id);

            if (reserva == null)
            {
                return NotFound();
            }

            if (!reserva.Estado)
            {
                TempData["Mensaje"] =
                    "No se puede renovar una reserva que está inactiva.";

                return RedirectToAction(nameof(Index));
            }

            var inquilinos =
                repositorioInquilino.ObtenerLista();

            var inmuebles =
                repositorioInmueble.ObtenerLista();

            var inquilino =
                inquilinos.FirstOrDefault(
                    i => i.ID_inquilino == reserva.ID_inquilino
                );

            var inmueble =
                inmuebles.FirstOrDefault(
                    i => i.ID_inmueble == reserva.ID_inmueble
                );

            ViewBag.InquilinoNombre =
                inquilino != null
                    ? $"{inquilino.Nombre} {inquilino.Apellido}"
                    : "No encontrado";

            ViewBag.InmuebleDireccion =
                inmueble != null
                    ? inmueble.Direccion
                    : "No encontrado";

            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Renovar(Reserva reserva)
        {
            // Buscamos la reserva original
            var reservaOriginal =
                repositorio.ObtenerPorId(reserva.ID_reserva);

            if (reservaOriginal == null)
            {
                return NotFound();
            }

            if (!reservaOriginal.Estado)
            {
                TempData["Mensaje"] =
                    "No se puede renovar una reserva que está inactiva.";

                return RedirectToAction(nameof(Index));
            }

            // ========================================================
            // CONSERVAMOS INQUILINO E INMUEBLE DE LA RESERVA ORIGINAL
            // ========================================================

            reserva.ID_inquilino =
                reservaOriginal.ID_inquilino;

            reserva.ID_inmueble =
                reservaOriginal.ID_inmueble;

            // ========================================================
            // VALIDAR FECHAS
            // ========================================================

            if (reserva.FechaInicio >= reserva.FechaFin)
            {
                ModelState.AddModelError(
                    "FechaFin",
                    "La fecha de finalización debe ser posterior a la fecha de inicio."
                );
            }

            // La renovación debe comenzar después
            // de la reserva original.
            if (reserva.FechaInicio < reservaOriginal.FechaFin)
            {
                ModelState.AddModelError(
                    "FechaInicio",
                    "La nueva reserva debe comenzar después de la fecha de finalización de la reserva original."
                );
            }

            if (!ModelState.IsValid)
            {
                return View(reserva);
            }

            // ========================================================
            // VALIDAR SUPERPOSICIÓN
            // ========================================================

            if (repositorio.ExisteReservaSuperpuesta(reserva))
            {
                ModelState.AddModelError(
                    "",
                    "El inmueble ya tiene una reserva activa en ese período."
                );

                return View(reserva);
            }

            // ========================================================
            // CREAR NUEVA RESERVA
            // ========================================================

            var nuevaReserva = new Reserva
            {
                ID_inquilino = reservaOriginal.ID_inquilino,
                ID_inmueble = reservaOriginal.ID_inmueble,
                FechaInicio = reserva.FechaInicio,
                FechaFin = reserva.FechaFin,
                MontoPorDia = reserva.MontoPorDia,
                Estado = true
            };

            repositorio.Alta(nuevaReserva);

            TempData["Mensaje"] =
                "Reserva renovada correctamente. Se creó una nueva reserva sin modificar la original.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var reserva = repositorio.ObtenerPorId(id);

            if (reserva == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Administrador"))
            {
                ViewBag.Auditoria =
                    repositorioAuditoria.ObtenerPorEntidad("Reserva", id);
            }

            return View(reserva);
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

        [HttpGet]
        public IActionResult FinalizarAnticipadamente(int id)
        {
            var reserva = repositorio.ObtenerPorId(id);

            if (reserva == null)
            {
                return NotFound();
            }

            if (!reserva.Estado)
            {
                TempData["Mensaje"] =
                    "No se puede finalizar una reserva que está inactiva.";

                return RedirectToAction(nameof(Index));
            }

            return View(reserva);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FinalizarAnticipadamente(
            int id,
            DateTime fechaFinEfectiva,
            bool multaPagada)
        {
            var reserva = repositorio.ObtenerPorId(id);

            if (reserva == null)
            {
                return NotFound();
            }

            if (!reserva.Estado)
            {
                TempData["Mensaje"] =
                    "La reserva ya se encuentra inactiva.";

                return RedirectToAction(nameof(Index));
            }

            // ========================================================
            // VALIDAR QUE LA MULTA HAYA SIDO PAGADA
            // ========================================================

            if (!multaPagada)
            {
                TempData["Mensaje"] =
                    "No se puede finalizar la reserva porque la multa no fue pagada.";

                return RedirectToAction(nameof(Index));
            }

            // ========================================================
            // VALIDAR FECHA EFECTIVA
            // ========================================================

            if (fechaFinEfectiva < reserva.FechaInicio ||
                fechaFinEfectiva > reserva.FechaFin)
            {
                ModelState.AddModelError(
                    "",
                    "La fecha de finalización efectiva debe estar dentro del período de la reserva."
                );

                return View(reserva);
            }

            // ========================================================
            // CALCULAR DURACIÓN ORIGINAL
            // ========================================================

            var totalDias =
                (reserva.FechaFin - reserva.FechaInicio).Days;

            // ========================================================
            // CALCULAR DÍAS CUMPLIDOS
            // ========================================================

            var diasCumplidos =
                (fechaFinEfectiva - reserva.FechaInicio).Days;

            // ========================================================
            // CALCULAR DÍAS RESTANTES
            // ========================================================

            var diasRestantes =
                (reserva.FechaFin - fechaFinEfectiva).Days;

            // ========================================================
            // DETERMINAR PORCENTAJE DE MULTA
            // ========================================================

            decimal porcentajeMulta;

            if (diasCumplidos < totalDias / 2.0)
            {
                porcentajeMulta = 0.50m;
            }
            else
            {
                porcentajeMulta = 0.25m;
            }

            // ========================================================
            // CALCULAR IMPORTE RESTANTE
            // ========================================================

            var importeRestante =
                diasRestantes * reserva.MontoPorDia;

            // ========================================================
            // CALCULAR MULTA
            // ========================================================

            var multa =
                importeRestante * porcentajeMulta;

            // ========================================================
            // REGISTRAR PAGO DE LA MULTA
            // ========================================================

            var pago = new Pago
            {
                ID_reserva = reserva.ID_reserva,
                FechaPago = DateTime.Today,
                Monto = multa,
                Concepto =
                    $"Multa por finalización anticipada ({porcentajeMulta * 100}%)",
                Estado = true
            };

            repositorioPago.Alta(pago);

            // ========================================================
            // REGISTRAR FECHA REAL DE FINALIZACIÓN
            // ========================================================

            repositorio.FinalizarAnticipadamente(
                reserva.ID_reserva,
                fechaFinEfectiva
            );

            // ========================================================
            // FINALIZAR RESERVA
            // ========================================================

            repositorio.Baja(reserva.ID_reserva);

            // ========================================================
            // REGISTRAR USUARIO QUE FINALIZÓ
            // ========================================================

            var claimId = User.FindFirst(
                ClaimTypes.NameIdentifier
            )?.Value;

            if (int.TryParse(claimId, out int idUsuario))
            {
                repositorioAuditoria.Alta(new Auditoria
                {
                    ID_usuario = idUsuario,
                    Entidad = "Reserva",
                    ID_entidad = reserva.ID_reserva,
                    Accion = "FINALIZACION",
                    Fecha = DateTime.Now
                });
            }

            TempData["Mensaje"] =
                $"Reserva finalizada anticipadamente. " +
                $"Multa pagada: ${multa:N2}.";

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var reserva = repositorio.ObtenerPorId(id);

            if (reserva == null)
            {
                return NotFound();
            }

            if (!reserva.Estado)
            {
                TempData["Mensaje"] =
                    "La reserva ya se encuentra inactiva.";

                return RedirectToAction(nameof(Index));
            }

            var hoy = DateTime.Today;

            // La reserva todavía no comenzó:
            // se puede cancelar normalmente.
            if (hoy < reserva.FechaInicio)
            {
                repositorio.Baja(id);

                TempData["Mensaje"] =
                    "La reserva fue cancelada correctamente.";

                return RedirectToAction(nameof(Index));
            }

            // La reserva ya terminó según la fecha original.
            if (hoy >= reserva.FechaFin)
            {
                repositorio.Baja(id);

                TempData["Mensaje"] =
                    "La reserva fue finalizada correctamente.";

                return RedirectToAction(nameof(Index));
            }

            // La reserva está actualmente en curso.
            // No permitimos darla de baja directamente.
            TempData["Mensaje"] =
                "La reserva se encuentra en curso. Para finalizarla antes de la fecha prevista, utilice la opción 'Finalizar anticipadamente'.";

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