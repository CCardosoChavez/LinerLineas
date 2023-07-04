using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Tablas
{
    public class Remesas_AX
    {
        public int nFIIDREMESA_AX { get; set; }
        public int nFIIDTIPO_REMESA { get; set; }
        public decimal dFNMONTO_REMESA { get; set; }
        public string sFSMONEDA { get; set; }
        public DateTime? daFDFECHA_DEPOSITO { get; set; }
        public DateTime? daFDALTA { get; set; }
        public int nFIIDESTATUS { get; set; }
        public Referencias rReferencias { get; set; }
        public long nlRECID_AX { get; set; }  //BigINT
        public decimal dFNTIPO_CAMBIO { get; set; }
        public string sFSNOMBRE_ARCHIVO { get; set; }
        public string sFSCUENTA_BANCO { get; set; }
        public string sRECID_DEPOSITO_GARANTI_AX { get; set; } //VARCHAR
    }
}
