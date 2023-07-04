using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Tipo_Conte
    {
        public string sTIPO_CONT { get; set; }
        public int nTAMANO { get; set; }
        public string sDESC_TC { get; set; }
        public decimal dTARA_TC { get; set; }
        public decimal dCAPAC_PESO { get; set; }
        public decimal dCUBICAJE { get; set; }
        public string sBMP { get; set; }
        public string sTIPO_TARIF { get; set; } //CHAR
        public decimal dTIPO_PZA { get; set; } //NUMERIC
        public string sDEFAULT_PZA { get; set; } //CHAR
    }
}
