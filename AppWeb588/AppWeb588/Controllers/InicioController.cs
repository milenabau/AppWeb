using AppWeb588.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace AppWeb588.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

        public IActionResult Nosotros()
        {
            return View();
        }

        public IActionResult Productos()
        {
            return View();
        }

        [Authorize]
        public IActionResult OficinaVirtual()
        {
            return View();
        }
        //------------------Atenticacion------------------------//

        [HttpGet("Login")]

        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult>Validar(string username, string password, string returnUrl)
        {
            //lectura de la Base de Datos y validacion

            if (username == "batman" && password == "robin")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("username", username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                claims.Add(new Claim(ClaimTypes.Name, "Bruce wine"));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUrl);
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                TempData["Error"] = "El Ususario o la Contraseña no son validos";
                return View("Login");
            }
        }
        [Authorize]
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}

