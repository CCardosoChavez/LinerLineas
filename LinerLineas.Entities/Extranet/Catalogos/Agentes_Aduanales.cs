using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Extranet.Catalogos
{
    public class Agentes_Aduanales
    {
        public int nFIIDAGENTE_ADUANAL { get; set; }
        public string sFSNOMBRE { get; set; }
        public string sFSAPELLIDOS { get; set; }
        public string sFSDIRECCION { get; set; }
        public int nFINUMERO_PATENTE { get; set; }
        public DateTime daFDALTA { get; set; }
        public int nFIIDESTATUS { get; set; }
        public int nFIIDAGREGADO_POR { get; set; }
        public bool bFBAUTORIZADO { get; set; }
        public string sFSCONTACTO { get; set; }
        public string sFSTELEFONO { get; set; }
        public string sFSCELULAR { get; set; }
        public string sFSEMAIL { get; set; }
    }
}
