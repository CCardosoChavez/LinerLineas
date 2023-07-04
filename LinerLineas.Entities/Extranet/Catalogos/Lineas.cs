using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Extranet.Catalogos
{
    public class Lineas
    {
        public int nFIIDLINEA { get; set; }
        public int nFIIDLINEA_LINER2000 { get; set; }
        public int nFIIDCOMPANIA { get; set; }
        public string sFSLINEA { get; set; }
        public int nFIESTATUS { get; set; }
        public DateTime daFDALTA { get; set; }
    }
}
