using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Patios
    {
        public int nNUM_PATIO { get; set; }
        public Proveedor rPROVEEDOR { get; set; }
        public Companias rCOMPANIAS { get; set; }
        public string sUBICACION { get; set; }
        public string sTEL { get; set; }
        public string sFAX { get; set; }
        public string sEMAIL { get; set; }
        public Puertos rPUERTOS { get; set; }
    }
}
