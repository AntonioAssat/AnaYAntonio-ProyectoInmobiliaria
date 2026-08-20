using System;
using AnaYAntonio_ProyectoInmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    public class InquilinosController : Controller
    {
        private readonly IRepositorioInquilino repositorio;
        private readonly ILogger<InquilinosController> logger;

        public InquilinosController(
            IRepositorioInquilino repo,
            ILogger<InquilinosController> logger)
        {
            this.repositorio = repo;
            this.logger = logger;
        }

        // LISTADO

        public ActionResult Index()
        {
            try
            {
                var lista = repositorio.ObtenerLista();
                ViewBag.Mensaje = TempData["Mensaje"];

                return View(lista);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en Index");
                throw;
            }
        }

        // ALTA

        // GET: Inquilinos/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en Create");
                throw;
            }
        }

        // POST: Inquilinos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino inquilino)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorio.Alta(inquilino);

                    TempData["Mensaje"] = "Inquilino registrado correctamente";

                    return RedirectToAction(nameof(Index));
                }

                return View(inquilino);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en Create");
                throw;
            }
        }

        // MODIFICACIÓN
        

        // GET: Inquilinos/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);

                if (entidad == null)
                    return NotFound();

                return View(entidad);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en Edit");
                throw;
            }
        }

        // POST: Inquilinos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inquilino entidad)
        {
            try
            {
                var inquilino = repositorio.ObtenerPorId(id);

                if (inquilino == null)
                    return NotFound();

                inquilino.NombreCompleto = entidad.NombreCompleto;
                inquilino.DNI = entidad.DNI;
                inquilino.Telefono = entidad.Telefono;
                inquilino.Mail = entidad.Mail;
                inquilino.Estado = entidad.Estado;

                repositorio.Modificacion(inquilino);

                TempData["Mensaje"] = "Datos guardados correctamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en Edit");
                throw;
            }
        }
        // BAJA

        // GET: Inquilinos/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);

                if (entidad == null)
                    return NotFound();

                return View(entidad);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en Eliminar");
                throw;
            }
        }

        // POST: Inquilinos/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, Inquilino entidad)
        {
            try
            {
                repositorio.Baja(id);

                TempData["Mensaje"] = "Eliminación realizada correctamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error en Eliminar");
                throw;
            }
        }
    }
}