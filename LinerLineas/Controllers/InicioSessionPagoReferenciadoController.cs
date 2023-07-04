using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LinerLineas.Entities;
using LinerLineas.Entities.PagoReferenciado;
using LinerLineas.Entities.Complementarias;
using LinerLineas.Entities.Extranet.Catalogos;
using LinerLineas.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace LinerLineas.Controllers
{
    public class InicioSessionPagoReferenciadoController : Controller
    {
        InicioSessionPagoReferenciadoHttp http = new InicioSessionPagoReferenciadoHttp();
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public Task<Result> GetDatosUsuario(AspNetUsers usuario)
        {
            return http.GetDatosUsuario(usuario);
        }
    }
}
