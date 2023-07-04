using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Buques
    {
        public decimal dNUM_LINEA { get; set; } //NUMERIC
        public int nNUM_BUQUE { get; set; }
        public Paises rPAISES { get; set; }
        public Puertos rPUERTOS { get; set; }
        public string sNOM_BUQUE { get; set; }
        public decimal dSLORA { get; set; }
        public decimal dMANGA { get; set; }
        public decimal dCALADO { get; set; }
        public decimal dPUNTAL { get; set; }
        public decimal dPROA { get; set; }
        public decimal dPOPA { get; set; }
        public decimal dTBR { get; set; }
        public decimal dTNR { get; set; }
        public decimal dPESO_MUERT { get; set; }
        public string sASOCIADO { get; set; } //CHAR
        public string sLETRA_LLAM { get; set; }
        public string sNUM_REG { get; set; }
        public string sCAPITAN { get; set; }
        public string sTIPO_TRAFI { get; set; } //CHAR
        public int nBODEGAS { get; set; }
        public int nESCOTILLAS { get; set; }
        public int nGRUAS { get; set; }
        public int nCAP_GRUAS { get; set; }
        public string sSTATUS { get; set; } //CHAR
        public string sSATCOM { get; set; }
        public string sTEL { get; set; }
        public string sTELEX { get; set; }
        public string sFAX { get; set; }
        public string sEMAIL { get; set; }
        public int nMA_CONSTRUC { get; set; }
        public string sCLASIFICA { get; set; }
        public int nCAP_TEUS { get; set; }
        public Tipo_Buques rTIPO_BUQUES { get; set; }
    }
}
