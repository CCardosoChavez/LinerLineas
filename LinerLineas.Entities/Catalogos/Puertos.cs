using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Puertos
    {
        public int nNUM_PTO { get; set; }
        public Paises rPAISES { get; set; }
        public string sMCVE_PTO { get; set; }
        public string sNOM_PTO { get; set; }
        public string sEDO_PTO { get; set; }
        public int nCONTI_PTO { get; set; }
        public string sAREA_GC1 { get; set; } //CHAR
        public string sAREA_GC2 { get; set; } //CHAR
        public string sAREA_GP { get; set; } //CHAR
        public string sLADA_CD { get; set; }
        public string sTIPOCIUDAD { get; set; } //CHAR
        public string sTRANSBORDO { get; set; } //CHAR
        public string sUSR_ALTA { get; set; }
        public DateTime daFEC_ALTA { get; set; }
        public string sUSR_MOD { get; set; } //CHAR
        public DateTime daFEC_MOD { get; set; }
        public string sSUB_AGTE { get; set; }
        public decimal dCVE_AMCN { get; set; } //NUMERIC 
        public string sCVE_PTO2 { get; set; }
        public string sLOCODE { get; set; }
    }
}
