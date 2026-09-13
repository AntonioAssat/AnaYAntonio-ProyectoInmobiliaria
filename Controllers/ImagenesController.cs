using Microsoft.AspNetCore.Mvc;
using AnaYAntonio_ProyectoInmobiliaria.Models;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    public class ImagenesController : Controller
    {
        private readonly IRepositorioImagen repositorio;

        public ImagenesController(IRepositorioImagen repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpPost]
        public async Task<IActionResult> Alta(
            int id,
            List<IFormFile> imagenes,
            [FromServices] IWebHostEnvironment environment)
        {
            if (imagenes == null || imagenes.Count == 0)
                return BadRequest("No se recibieron archivos.");

            string wwwPath = environment.WebRootPath;

            string path = Path.Combine(
                wwwPath,
                "Uploads",
                "Inmuebles",
                id.ToString()
            );

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var file in imagenes)
            {
                if (file.Length > 0)
                {
                    var extension = Path.GetExtension(file.FileName);

                    var nombreArchivo = $"{Guid.NewGuid()}{extension}";

                    var rutaArchivo = Path.Combine(
                        path,
                        nombreArchivo
                    );

                    using (var stream = new FileStream(
                        rutaArchivo,
                        FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    Imagen imagen = new Imagen
                    {
                        InmuebleId = id,
                        Url = $"/Uploads/Inmuebles/{id}/{nombreArchivo}"
                    };

                    repositorio.Alta(imagen);
                }
            }

            return Ok(repositorio.BuscarPorInmueble(id));
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                var entidad = repositorio.ObtenerPorId(id);

                repositorio.Baja(id);

                return Ok(
                    repositorio.BuscarPorInmueble(entidad.InmuebleId)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}