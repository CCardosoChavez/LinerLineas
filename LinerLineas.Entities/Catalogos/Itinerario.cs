using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Itinerario
    {
        public int nNUM_ITINE { get; set; }
        public Puertos rPUERTOS { get; set; } //PTO_ORI
        public Lineas rLINEAS { get; set; }
        public Buques rBUQUES { get; set; }
        public Companias rCOMPANIAS { get; set; }
        public int nFRECUENCIA { get; set; }
        public DateTime? daF_ARRIVO_P { get; set; }
        public DateTime daF_ATRA_P { get; set; }
        public DateTime daF_ZARPE_P { get; set; }
        public DateTime daF_ARRIVO_R { get; set; }
        public DateTime daF_ATRA_R { get; set; }
        public DateTime daF_ZARPE_R { get; set; }
        public DateTime daF_E_FONDEO { get; set; }
        public DateTime daF_S_FONDEO { get; set; }
        public DateTime daF_CIERRE_A { get; set; }
        public DateTime daF_CIERRE_M { get; set; }
        public int nESP_DISP { get; set; }
        public string sDESTINOS { get; set; }
        public string sMLINEA_EVEN { get; set; } //CHAR
        public DateTime daH_ARRIVO_R { get; set; }
        public DateTime daH_ATRA_R { get; set; }
        public DateTime daH_ZARPE_R { get; set; }
        public DateTime daH_E_FONDEO { get; set; }
        public DateTime daH_S_FONDEO { get; set; }
        public DateTime daH_CIERRE_A { get; set; }
        public DateTime daH_CIERRE_M { get; set; }
        public string sNUM_VIAJE { get; set; }
        public int nNUM_REG { get; set; }
        public DateTime daF_CIERRE_F { get; set; }
        public DateTime daH_CIERRE_F { get; set; }
        public Puertos rPTO_PROC { get; set; }
        public Puertos rPTO_DEST { get; set; }
        public int nNUM_REG_I { get; set; }
        public string sNUM_VIAJEI { get; set; }
        public string sTIPO_BL { get; set; } //CHAR
        public decimal dADU_SEC { get; set; } //NUMERIC
        public decimal dMCVE_RCNT { get; set; } //NUMERIC
        public string sACUSE { get; set; }
        public string sACUSE_I { get; set; }
        public decimal dN_TRANS_I { get; set; } //NUMERIC
        public decimal dN_TRANS { get; set; } //NUMERIC
        public decimal dN_ITINE_M { get; set; } //NUMERIC
        public string sSERV_ZONA { get; set; } //CHAR
        public string sLINEA_COMP { get; set; }
        public string sTIPO_ARCHIVO { get; set; } //CHAR
        public string sACUSE_T { get; set; }
        public decimal dNUM_TRANS_T { get; set; } //NUMERIC
        public string sTIPO_ARCHIVOI { get; set; } //CHAR
        public string sCVE_CAAT { get; set; } //CHAR
        public string sNUM_UNICO_I { get; set; } //CHAR
        public string sNUM_UNICO_E { get; set; } //CHAR
        public DateTime daH_ETA { get; set; } //SAMLLDATETIME
        public DateTime daFEC_REG { get; set; } //SAMLLDATETIME
        public string sUSR_REG { get; set; }
        public DateTime daFEC_F_H_REALES { get; set; } //SMALLDATETIME
        public string sUSR_F_H_REALES { get; set; }
        public DateTime daF_CIERRE_VGM { get; set; } //SMALLDATETIME
        public string sCOD_BUQUE_I { get; set; }
        public string sCOD_BUQUE_E { get; set; }
        public int nPERIODO_ENVIO { get; set; }
        public int nPERIODO_I { get; set; }
        public int nPERIODO_E { get; set; }
        public DateTime daFEC_ENVIO { get; set; } //SAMLLDATETIME
        public string sUSR_ENVIO { get; set; }
    }
}
