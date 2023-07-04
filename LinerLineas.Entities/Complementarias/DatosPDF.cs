using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities.Tablas;

namespace LinerLineas.Entities.Complementarias
{
    public class DatosPDF
    {
        public string sNUMERO_REFERENCIA { get; set; }
        public string sNUMERO_BL { get; set; }
        public decimal dMONTO_MXN { get; set; }
        public decimal dMONTO_USD { get; set; }
        public int nIDLINEA { get; set; }
        public string sNOMBRE_BUQUE { get; set; }
        public string sNUMERO_VIAJE { get; set; }
        public string sPUERTO_DESCARGA { get; set; }
        public decimal dTIPO_CAMBIO { get; set; }
        public string sNOMBRE_CLIENTE { get; set; }
        public string sRFC_CLIENTE { get; set; }
        public string sCALLE { get; set; }
        public string sCOLONIA { get; set; }
        public string sMUNICIPIO { get; set; }
        public string sESTADO { get; set; }
        public string sPAIS { get; set; }
        public string sCODIGO_POSTAL { get; set; }
        public string sBANCO { get; set; }
        public string sCONVENIO { get; set; }
        public string sCLAVE_INTERBANCARIA { get; set; }
        public string sSIGLAS_LINEA { get; set; }
        public int nITINERARIO { get; set; }

        //Datos de la linea WebService
        public string sRFC_LINEA { get; set; }
        public string sRAZON_SOCIAL_LINEA { get; set; }

        public List<object> liCONTENEDORES { get; set; }
    }
}
