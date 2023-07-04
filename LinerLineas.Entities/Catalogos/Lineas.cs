using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Lineas
    {
        public decimal dNUM_LINEA { get; set; } //numeric
        public Paises rPAISES { get; set; }
        public Companias rCOMPANIAS { get; set; }
        public string sNOM_LINEA { get; set; }
        public string sRFC { get; set; }
        public string sCALLE { get; set; }
        public string sCOLONIA { get; set; }
        public string sCIUDAD { get; set; }
        public int nPAIS { get; set; }
        public string sTEL { get; set; }
        public int nFAX { get; set; }
        public string sTELEX { get; set; }
        public string sMEMAIL { get; set; }
        public string sDIR_CABLE { get; set; }
        public string sCONTACTO { get; set; }
        public string sPUESTO_C { get; set; }
        public string sTEL_C { get; set; }
        public string sCONTACTO2 { get; set; }
        public string sPUESTO_C2 { get; set; }
        public string sTEL_C2 { get; set; }
        public string sLINEA_EVEN { get; set; } //CHAR
        public string sSTATUS { get; set; } //CHAR
        public string sTIPO_MON { get; set; } //CHAR
        public DateTime daFEC_ALTA { get; set; }
        public int nCVE_LINEA { get; set; }
        public int nNUM_CARTA { get; set; }
        public string sCVE_TRANS { get; set; }
        public string sCVE_CAAT { get; set; } //CHAR
        public string sNUM_CP { get; set; }
        public string sESTADO { get; set; }
        public string sDEL_MUN { get; set; }
        public string sNUMERO_INT { get; set; }
        public string sNUMERO_EXT { get; set; }
        public string sREFERENCIA { get; set; }
        public bool bISWEB { get; set; }
        public string sLINEA_SAP { get; set; } //CHAR
        public string sDEMORAS_EXPO { get; set; } //CHAR
        public string sREPORTA_VGM { get; set; } //CHAR
        public string sUSA_PAGO_REF { get; set; } //CHAR
        public string sPAGO_REF_X_BL_IMPO { get; set; } //CHAR
        public string sPAGO_REF_X_CONT_IMPO { get; set; } //CHAR
        public string sPAGO_REF_X_BL_EXPO { get; set; } //CHAR
        public string sPAGO_REF_X_CONT_EXPO { get; set; } //CHAR
    }
}
