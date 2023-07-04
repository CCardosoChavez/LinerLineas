using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Complementarias
{
    public class DatosBusqueda
    {
        public string sTexto { get; set; }
        public string sFechaInicial_Desde { get; set; }
        public string sFechaFinal_Hasta { get; set; }
        public int? nLinea { get; set; }

        public int nItinerario { get; set; }
        public string sNumBL { get; set; }

        //Paginacion
        public Paginacion rPAGINACION { get; set; }

        //Propiedades para obtener las referencias para tesorería
        public int nIdTipoConsultaTesoseria { get; set; }
        public string sRol { get; set; }
    }
}
