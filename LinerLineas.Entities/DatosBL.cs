using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities
{
    public class DatosBL
    {
        public string sNUMERO_BL { get; set; }
        public int nNUMERO_ITINERARIO { get; set; }
        public int nNUMERO_LINEA { get; set; }
        public int nNUMERO_COMPANIA { get; set; }
        public string sNOMBRE_LINEA { get; set; }
        public string sBUQUE { get; set; }
        public string sPUERTO_DESCARGA { get; set; }
        public string sPUERTO_OPERATIVO { get; set; }
        public string sETA { get; set; }
        public string sCLIENTE { get; set; }
        public string sNUMERO_VIAJE { get; set; }
        public decimal dTIPO_CAMBIO { get; set; }
        public decimal dMONTO_USD { get; set; }
        public decimal dMONTO_MXN { get; set; }
        public int nTIPO_REMESA { get; set; }
        //public DatosContenedor rDATOS_CONTENEDOR { get; set; }
    }
}
