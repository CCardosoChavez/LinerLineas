using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LinerLineas.Entities;
using LinerLineas.Entities.Tablas;
using LinerLineas.Entities.Complementarias;
using LinerLineas.Http;
using Microsoft.AspNetCore.Authorization;

namespace LinerLineas.Controllers
{
    //[Authorize(Roles = "CONTROL DE EQUIPO")]
    public class ControlEquipoController : Controller
    {
        ControlEquipoHttp http = new ControlEquipoHttp();
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ControlEquipo(Stock datosContenedor)
        {
            return View(datosContenedor);
        }

        [HttpGet]
        public IActionResult GetBLS()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetContenedores()
        {
            return View();
        }

        public Task<Result> AgregarDesperfectoContenedor(Desperfecto_Contenedor desperfectoContenedor)
        {
            return http.AgregarDesperfectoContenedor(desperfectoContenedor);
        }

        public Task<Result> AddEstatusContenedorSinDesperfecto(Entities.Tablas.Estatus_Dano_Contenedores danoContenedores)
        {
            return http.AddEstatusContenedorSinDesperfecto(danoContenedores);
        }

        public Task<Result> UpdateDesperfectoContenedor(CamposExtra datos)
        {
            return http.UpdateDesperfectoContenedor(datos);
        }

        public Task<Result> DeleteDesperfectoContenedor(CamposExtra datos)
        {
            return http.DeleteDesperfectoContenedor(datos);
        }

        public Task<Result> GetBLS(DatosBusqueda datosBusqueda)
        {
            return http.GetBLS(datosBusqueda);
        }

        public Task<Result> GetContenedores(DatosBusqueda datosBusqueda)
        {
            return http.GetContenedores(datosBusqueda);
        }

        public Task<int> ObtenerNumeroTotalBLS(DatosBusqueda datosBusqueda)
        {
            return http.ObtenerNumeroTotalBLS(datosBusqueda);
        }

        public Task<Result> UpdateEstatusDesperfectoContenedores(Referencias referencia)
        {
            return http.UpdateEstatusDesperfectoContenedores(referencia);
        }
    }
}
