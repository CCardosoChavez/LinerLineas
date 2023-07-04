using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Tablas
{
    public class Det_CobDem
    {
        public int nNUM_CIA { get; set; }
        public string sTIPO_CARGO { get; set; } //CHAR
        public string sTIPO_COBRO { get; set; } //CHAR
        public int nNUM_COBRO { get; set; }
        public int nNUM_ITINEI { get; set; }
        public string sNUM_CONT { get; set; }
        public int nNUM_CGO { get; set; }
        public decimal dIMPORTE_D { get; set; }
        public string sNUM_BL { get; set; }
    }
}
