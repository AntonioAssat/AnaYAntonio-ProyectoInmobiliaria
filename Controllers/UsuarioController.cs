using System.Security.Claims;
using AnaYAntonio_ProyectoInmobiliaria.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnaYAntonio_ProyectoInmobiliaria.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IRepositorioUsuario repositorio;

        public UsuarioController(IRepositorioUsuario repositorio)
        {
            this.repositorio = repositorio;
        }


        // =====================================================
        // LOGIN
        // =====================================================

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginView login,
            string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;

                return View(login);
            }


            var usuario = repositorio.ObtenerPorEmail(login.Usuario);


            if (usuario == null ||
                !usuario.Estado ||
                !PasswordHelper.VerifyPassword(
                    login.Clave,
                    usuario.Clave
                ))
            {
                ModelState.AddModelError(
                    "",
                    "El email o la contraseña son incorrectos."
                );

                ViewBag.ReturnUrl = returnUrl;

                return View(login);
            }


            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.Id.ToString()
                ),

                new Claim(
                    ClaimTypes.Name,
                    usuario.Email
                ),

                new Claim(
                    "FullName",
                    usuario.Nombre + " " + usuario.Apellido
                ),

                new Claim(
                    ClaimTypes.Role,
                    usuario.RolNombre
                )
            };


            var claimsIdentity =
                new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );


            var claimsPrincipal =
                new ClaimsPrincipal(claimsIdentity);


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal
            );


            if (!string.IsNullOrEmpty(returnUrl) &&
                Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }


            return RedirectToAction(
                "Index",
                "Home"
            );
        }


        // =====================================================
        // LOGOUT
        // =====================================================

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction(
                "Index",
                "Home"
            );
        }


        // =====================================================
        // ACCESO DENEGADO
        // =====================================================

        [AllowAnonymous]
        public IActionResult AccesoDenegado()
        {
            return View();
        }


        // =====================================================
        // PERFIL PROPIO
        // Cualquier usuario autenticado puede ver su propio perfil
        // =====================================================

        [Authorize]
        [HttpGet]
        public IActionResult Perfil()
        {
            var claimId =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            if (!int.TryParse(claimId, out int id))
            {
                return Unauthorized();
            }


            var usuario =
                repositorio.ObtenerPorId(id);


            if (usuario == null ||
                !usuario.Estado)
            {
                return NotFound();
            }


            return View(usuario);
        }


        // =====================================================
        // EDITAR PERFIL PROPIO
        // =====================================================

        [Authorize]
        [HttpGet]
        public IActionResult EditPerfil()
        {
            var claimId =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            if (!int.TryParse(claimId, out int id))
            {
                return Unauthorized();
            }


            var usuario =
                repositorio.ObtenerPorId(id);


            if (usuario == null ||
                !usuario.Estado)
            {
                return NotFound();
            }


            return View(usuario);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPerfil(
            Usuario usuario,
            [FromServices] IWebHostEnvironment environment)
        {
            var claimId =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            if (!int.TryParse(claimId, out int id))
            {
                return Unauthorized();
            }


            // Buscamos el usuario real
            var usuarioActual =
                repositorio.ObtenerPorId(id);


            if (usuarioActual == null ||
                !usuarioActual.Estado)
            {
                return NotFound();
            }


            // El usuario solamente puede modificar
            // su propio perfil
            usuario.Id = id;


            // No puede modificar su rol
            usuario.Rol = usuarioActual.Rol;


            // Conservamos el estado
            usuario.Estado = usuarioActual.Estado;


            // =================================================
            // CONTRASEÑA
            // =================================================

            ModelState.Remove(nameof(usuario.Clave));


            if (string.IsNullOrWhiteSpace(usuario.Clave))
            {
                usuario.Clave =
                    usuarioActual.Clave;

                ModelState.Remove("Clave");
            }
            else
            {
                usuario.Clave =
                    PasswordHelper.HashPassword(
                        usuario.Clave
                    );
            }


            // =================================================
            // AVATAR
            // =================================================

            if (usuario.AvatarFile != null &&
                usuario.AvatarFile.Length > 0)
            {
                string path =
                    Path.Combine(
                        environment.WebRootPath,
                        "Uploads",
                        "Usuarios",
                        id.ToString()
                    );


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


                string extension =
                    Path.GetExtension(
                        usuario.AvatarFile.FileName
                    );


                string nombreArchivo =
                    $"{Guid.NewGuid()}{extension}";


                string rutaArchivo =
                    Path.Combine(
                        path,
                        nombreArchivo
                    );


                using (var stream =
                    new FileStream(
                        rutaArchivo,
                        FileMode.Create))
                {
                    await usuario.AvatarFile
                        .CopyToAsync(stream);
                }


                // Eliminar avatar anterior si existe
                if (!string.IsNullOrWhiteSpace(
                    usuarioActual.Avatar))
                {
                    string avatarAnterior =
                        Path.Combine(
                            environment.WebRootPath,
                            usuarioActual.Avatar
                                .TrimStart('/')
                                .Replace(
                                    "/",
                                    Path.DirectorySeparatorChar
                                        .ToString()
                                )
                        );


                    if (System.IO.File.Exists(
                        avatarAnterior))
                    {
                        System.IO.File.Delete(
                            avatarAnterior
                        );
                    }
                }


                // Guardamos la ruta
                // en la base de datos
                usuario.Avatar =
                    $"/Uploads/Usuarios/{id}/{nombreArchivo}";
            }
            else
            {
                // Si no seleccionó una imagen nueva,
                // mantenemos el avatar actual
                usuario.Avatar =
                    usuarioActual.Avatar;
            }


            // =================================================
            // VALIDACIÓN
            // =================================================

            if (!ModelState.IsValid)
            {
                return View(usuario);
            }


            repositorio.Modificacion(usuario);


            TempData["Mensaje"] =
                "Perfil modificado correctamente.";


            return RedirectToAction(
                nameof(Perfil)
            );
        }


        // =====================================================
        // GESTIÓN DE USUARIOS
        // SOLO ADMINISTRADOR
        // =====================================================

        [Authorize(Policy = "Administrador")]
        public IActionResult Index()
        {
            var lista =
                repositorio.ObtenerLista();

            return View(lista);
        }


        // =====================================================
        // CREAR USUARIO
        // =====================================================

        [Authorize(Policy = "Administrador")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles =
                Usuario.ObtenerRoles();

            return View();
        }


        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles =
                    Usuario.ObtenerRoles();

                return View(usuario);
            }


            // Convertimos la contraseña
            // en un hash antes de guardarla
            usuario.Clave =
                PasswordHelper.HashPassword(
                    usuario.Clave
                );


            usuario.Estado = true;


            repositorio.Alta(usuario);


            TempData["Mensaje"] =
                "Usuario registrado correctamente.";


            return RedirectToAction(
                nameof(Index)
            );
        }


        // =====================================================
        // MODIFICAR USUARIO
        // =====================================================

        [Authorize(Policy = "Administrador")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var usuario =
                repositorio.ObtenerPorId(id);


            if (usuario == null)
            {
                return NotFound();
            }


            ViewBag.Roles =
                Usuario.ObtenerRoles();


            return View(usuario);
        }


        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario usuario)
        {
            // Obtenemos el usuario actual
            // desde la base de datos
            var usuarioActual =
                repositorio.ObtenerPorId(
                    usuario.Id
                );


            if (usuarioActual == null)
            {
                return NotFound();
            }


            // Si la contraseña quedó vacía,
            // conservamos la contraseña anterior
            if (string.IsNullOrWhiteSpace(
                usuario.Clave))
            {
                usuario.Clave =
                    usuarioActual.Clave;

                ModelState.Remove("Clave");
            }
            else
            {
                // Si se escribió una contraseña nueva,
                // la convertimos a hash PBKDF2
                usuario.Clave =
                    PasswordHelper.HashPassword(
                        usuario.Clave
                    );
            }


            if (!ModelState.IsValid)
            {
                ViewBag.Roles =
                    Usuario.ObtenerRoles();

                return View(usuario);
            }


            repositorio.Modificacion(usuario);


            TempData["Mensaje"] =
                "Usuario modificado correctamente.";


            return RedirectToAction(
                nameof(Index)
            );
        }


        // =====================================================
        // BAJA DE USUARIO
        // =====================================================

        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            repositorio.Baja(id);


            TempData["Mensaje"] =
                "Usuario dado de baja correctamente.";


            return RedirectToAction(
                nameof(Index)
            );
        }


        // =====================================================
        // REACTIVAR USUARIO
        // =====================================================

        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Activate(int id)
        {
            repositorio.AltaEstado(id);


            TempData["Mensaje"] =
                "Usuario activado correctamente.";


            return RedirectToAction(
                nameof(Index)
            );
        }
    }
}