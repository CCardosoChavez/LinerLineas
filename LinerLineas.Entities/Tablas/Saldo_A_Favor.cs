using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Tablas
{
    public class Saldo_A_Favor
    {
        public int nFIIDSALDO_A_FAVOR { get; set; }
        public int nFIIDREMESA_AX { get; set; }
        public int nFIIDAPLICACION_REMESA { get; set; }
        public double dFNMONTO { get; set; } //FLOAT
        public string sNUM_BL { get; set; }
        public int nNUM_ITINE { get; set; }
    }
}
