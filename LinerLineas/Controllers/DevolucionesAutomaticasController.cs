using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinerLineas.Entities;
using LinerLineas.Entities.Tablas;
using LinerLineas.Entities.Catalogos;
using LinerLineas.Entities.Complementarias;
using Microsoft.AspNetCore.Authorization;
using LinerLineas.Http;

namespace LinerLineas.Controllers
{
   
    public class DevolucionesAutomaticasController : Controller
    {
       DevolucionesAutomaticasHttp http = new DevolucionesAutomaticasHttp();

        public ActionResult Index()
        {
            return View();
        }

        //Metodo Control solicitud de devoluciones
        public Task<bool> SolicitarDevolucionSaldo(ControlDevolucion controlDevolucion)
        {
            return http.SolicitarDevolucionSaldo(controlDevolucion);
        }
    }
}
