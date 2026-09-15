using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AnaYAntonio_ProyectoInmobiliaria.Models;
using Microsoft.AspNetCore.Http;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    [Authorize]
    public class InmueblesController : Controller
    {
        private readonly IRepositorioInmueble repositorio;
        private readonly IRepositorioPropietario repositorioPropietario;
        private readonly IRepositorioTipoInmueble repositorioTipo;
        private readonly IRepositorioImagen repositorioImagen;

        public InmueblesController(
            IRepositorioInmueble repositorio,
            IRepositorioPropietario repositorioPropietario,
            IRepositorioTipoInmueble repositorioTipo,
            IRepositorioImagen repositorioImagen)
        {
            this.repositorio = repositorio;
            this.repositorioPropietario = repositorioPropietario;
            this.repositorioTipo = repositorioTipo;
            this.repositorioImagen = repositorioImagen;
        }

        // =========================
        // LISTADO
        // =========================
        public IActionResult Index(
            string? buscar,
            int pagina = 1)
        {
            int cantidadPorPagina = 10;

            if (pagina < 1)
            {
                pagina = 1;
            }

            int cantidadTotal =
                repositorio.ObtenerCantidad(buscar);

            int cantidadPaginas =
                (int)Math.Ceiling(
                    cantidadTotal / (double)cantidadPorPagina
                );

            if (cantidadPaginas > 0 && pagina > cantidadPaginas)
            {
                pagina = cantidadPaginas;
            }

            var lista =
                repositorio.ObtenerListaPaginada(
                    buscar,
                    pagina,
                    cantidadPorPagina
                );

            foreach (var inmueble in lista)
            {
                inmueble.Imagenes =
                    repositorioImagen
                        .BuscarPorInmueble(
                            inmueble.ID_inmueble
                        );
            }

            ViewBag.Buscar = buscar;
            ViewBag.PaginaActual = pagina;
            ViewBag.CantidadPaginas = cantidadPaginas;
            ViewBag.CantidadTotal = cantidadTotal;

            return View(lista);
        }

        // =========================
        // CREAR
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Propietarios =
                repositorioPropietario
                    .ObtenerLista()
                    .Where(p => p.Estado)
                    .ToList();

            ViewBag.Tipos =
                repositorioTipo
                    .ObtenerLista()
                    .Where(t => t.Estado)
                    .ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Inmueble inmueble,
            int DuenioId,
            int TipoId,
            List<IFormFile> imagenes)
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
                ViewBag.Propietarios =
                    repositorioPropietario
                        .ObtenerLista()
                        .Where(p => p.Estado)
                        .ToList();

                ViewBag.Tipos =
                    repositorioTipo
                        .ObtenerLista()
                        .Where(t => t.Estado)
                        .ToList();

                return View(inmueble);
            }

            var propietario =
                repositorioPropietario
                    .ObtenerPorId(DuenioId);

            var tipo =
                repositorioTipo
                    .ObtenerPorId(TipoId);

            if (propietario == null ||
                !propietario.Estado ||
                tipo == null ||
                !tipo.Estado)
            {
                ViewBag.Propietarios =
                    repositorioPropietario
                        .ObtenerLista()
                        .Where(p => p.Estado)
                        .ToList();

                ViewBag.Tipos =
                    repositorioTipo
                        .ObtenerLista()
                        .Where(t => t.Estado)
                        .ToList();

                ModelState.AddModelError(
                    "",
                    "El propietario o el tipo de inmueble seleccionado no es válido."
                );

                return View(inmueble);
            }

            inmueble.Duenio = propietario;
            inmueble.Tipo = tipo;
            inmueble.Estado = true;

            repositorio.Alta(inmueble);

            if (imagenes != null &&
                imagenes.Count > 0)
            {
                string path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Uploads",
                    "Inmuebles",
                    inmueble.ID_inmueble.ToString()
                );

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var file in imagenes)
                {
                    if (file.Length > 0)
                    {
                        var extension =
                            Path.GetExtension(file.FileName);

                        var nombreArchivo =
                            $"{Guid.NewGuid()}{extension}";

                        var rutaArchivo =
                            Path.Combine(
                                path,
                                nombreArchivo
                            );

                        using (var stream =
                               new FileStream(
                                   rutaArchivo,
                                   FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var imagen = new Imagen
                        {
                            InmuebleId =
                                inmueble.ID_inmueble,

                            Url =
                                $"/Uploads/Inmuebles/{inmueble.ID_inmueble}/{nombreArchivo}"
                        };

                        repositorioImagen.Alta(imagen);
                    }
                }
            }

            TempData["Mensaje"] =
                "Inmueble creado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DETALLES
        // =========================
        [HttpGet]
        public IActionResult Details(int id)
        {
            var inmueble =
                repositorio.ObtenerPorId(id);

            if (inmueble == null)
            {
                return NotFound();
            }

            inmueble.Imagenes =
                repositorioImagen
                    .BuscarPorInmueble(
                        inmueble.ID_inmueble
                    );

            return View(inmueble);
        }

        // =========================
        // EDITAR
        // =========================
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var inmueble =
                repositorio.ObtenerPorId(id);

            if (inmueble == null)
            {
                return NotFound();
            }

            inmueble.Imagenes =
                repositorioImagen
                    .BuscarPorInmueble(
                        inmueble.ID_inmueble
                    );

            ViewBag.Propietarios =
                repositorioPropietario
                    .ObtenerLista()
                    .Where(p => p.Estado)
                    .ToList();

            ViewBag.Tipos =
                repositorioTipo
                    .ObtenerLista()
                    .Where(t => t.Estado)
                    .ToList();

            return View(inmueble);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            Inmueble inmueble,
            int DuenioId,
            int TipoId,
            List<IFormFile> imagenes)
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
                inmueble.Imagenes =
                    repositorioImagen
                        .BuscarPorInmueble(
                            inmueble.ID_inmueble
                        );

                ViewBag.Propietarios =
                    repositorioPropietario
                        .ObtenerLista()
                        .Where(p => p.Estado)
                        .ToList();

                ViewBag.Tipos =
                    repositorioTipo
                        .ObtenerLista()
                        .Where(t => t.Estado)
                        .ToList();

                return View(inmueble);
            }

            var inmuebleExistente =
                repositorio.ObtenerPorId(
                    inmueble.ID_inmueble
                );

            if (inmuebleExistente == null)
            {
                return NotFound();
            }

            var propietario =
                repositorioPropietario
                    .ObtenerPorId(DuenioId);

            var tipo =
                repositorioTipo
                    .ObtenerPorId(TipoId);

            if (propietario == null ||
                !propietario.Estado ||
                tipo == null ||
                !tipo.Estado)
            {
                inmueble.Imagenes =
                    repositorioImagen
                        .BuscarPorInmueble(
                            inmueble.ID_inmueble
                        );

                ViewBag.Propietarios =
                    repositorioPropietario
                        .ObtenerLista()
                        .Where(p => p.Estado)
                        .ToList();

                ViewBag.Tipos =
                    repositorioTipo
                        .ObtenerLista()
                        .Where(t => t.Estado)
                        .ToList();

                ModelState.AddModelError(
                    "",
                    "El propietario o el tipo de inmueble seleccionado no es válido."
                );

                return View(inmueble);
            }

            inmuebleExistente.Duenio =
                propietario;

            inmuebleExistente.Tipo =
                tipo;

            inmuebleExistente.Direccion =
                inmueble.Direccion;

            inmuebleExistente.Cupo =
                inmueble.Cupo;

            inmuebleExistente.Coordenadas =
                inmueble.Coordenadas;

            inmuebleExistente.PrecioPorDia =
                inmueble.PrecioPorDia;

            inmuebleExistente.PorcentajeReserva =
                inmueble.PorcentajeReserva;

            repositorio.Modificacion(
                inmuebleExistente
            );

            // AGREGAR NUEVAS IMÁGENES
            if (imagenes != null &&
                imagenes.Count > 0)
            {
                string path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Uploads",
                    "Inmuebles",
                    inmueble.ID_inmueble.ToString()
                );

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var file in imagenes)
                {
                    if (file.Length > 0)
                    {
                        var extension =
                            Path.GetExtension(file.FileName);

                        var nombreArchivo =
                            $"{Guid.NewGuid()}{extension}";

                        var rutaArchivo =
                            Path.Combine(
                                path,
                                nombreArchivo
                            );

                        using (var stream =
                               new FileStream(
                                   rutaArchivo,
                                   FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var imagen = new Imagen
                        {
                            InmuebleId =
                                inmueble.ID_inmueble,

                            Url =
                                $"/Uploads/Inmuebles/{inmueble.ID_inmueble}/{nombreArchivo}"
                        };

                        repositorioImagen.Alta(
                            imagen
                        );
                    }
                }
            }

            TempData["Mensaje"] =
                "Inmueble modificado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // ELIMINAR IMAGEN
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarImagen(int id)
        {
            var imagen =
                repositorioImagen.ObtenerPorId(id);

            if (imagen == null)
            {
                return NotFound();
            }

            // Eliminar archivo físico
            if (!string.IsNullOrEmpty(imagen.Url))
            {
                var rutaArchivo =
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        imagen.Url
                            .TrimStart('/')
                            .Replace(
                                "/",
                                Path.DirectorySeparatorChar
                                    .ToString()
                            )
                    );

                if (System.IO.File.Exists(
                        rutaArchivo))
                {
                    System.IO.File.Delete(
                        rutaArchivo
                    );
                }
            }

            // Eliminar registro de la base de datos
            repositorioImagen.Baja(id);

            TempData["Mensaje"] =
                "Imagen eliminada correctamente.";

            return RedirectToAction(
                nameof(Edit),
                new { id = imagen.InmuebleId }
            );
        }

        // =========================
        // BAJA
        // =========================
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Baja(int id)
        {
            repositorio.Baja(id);

            TempData["Mensaje"] =
                "Inmueble dado de baja correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // ALTA ESTADO
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AltaEstado(int id)
        {
            repositorio.AltaEstado(id);

            TempData["Mensaje"] =
                "Inmueble dado de alta correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}