using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Proveedor
    {
        public int nNUM_PROV { get; set; }
        public Puertos rPUERTOS { get; set; }
        public Companias rCOMPANIAS { get; set; }
        public string sNOM_PROV { get; set; }
        public string sCALLE { get; set; }
        public string sCOLONIA { get; set; }
        public string sDEL_MUN { get; set; }
        public string sCIUDAD { get; set; }
        public Paises rPAISES { get; set; }
        public string sTEL { get; set; }
        public string sFAX { get; set; }
        public string sEMAIL { get; set; }
        public string sCONTACTO { get; set; }
        public string sPUESTO_C { get; set; }
        public string sTIPO_PROV { get; set; } //CHAR
        public string sESTATUS { get; set; } //CHAR
        public int nDIAS_CRED { get; set; }
        public string sUSR_CRED { get; set; }
        public DateTime rFEC_CRED { get; set; }
        public string sCVE_MEXT { get; set; } //CHAR
        public string sRFC { get; set; }
    }
}
