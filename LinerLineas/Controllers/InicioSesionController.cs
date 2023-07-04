using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LinerLineas.Entities;
using LinerLineas.Entities.Tablas;
using LinerLineas.Entities.Complementarias;
using LinerLineas.Entities.Extranet.Catalogos;
using LinerLineas.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace LinerLineas.Controllers
{
    public class InicioSesionController : Controller
    {
        InicioSesionHttp http = new InicioSesionHttp();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public Task<Result> IniciarSesion(Usuarios usuario)
        {
            return http.IniciarSesion(usuario);
        }

        //[HttpPost]
        //public async Task<bool> ValidarUsuario(Usuarios usuarios)
        //{
        //    //Pasamos los datod para crar el claim del usuario 
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, usuarios.sFSUSUARIO),
        //        new Claim("Correo",usuarios.sFSCORREO)
        //    };

        //    //Agregamos el rol y perfil que guardamos en la propiedead liROLES_LOGIN
        //    foreach (string rol in usuarios.liROLES_LOGIN)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, rol));
        //    }
            
        //    //Para crear la cookie dentor de esta session de usuario
        //    var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        //    return true;
        //    //return RedirectToAction("Index", "Principal");
        //}

        //[HttpGet]
        //public async Task<bool> CerrarSesion()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return true; 
        //}
    }
}
