using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Extranet.Catalogos
{
    public class Ejecutivos
    {
        public int nFIIDEJECUTIVO { get; set; }
        public int nFIIDAGENTE_ADUANAL { get; set; }
        public string sFSNOMBRE { get; set; }
        public string sFSAPELLIDO { get; set; }
        public string sFSCORREO_ELECTRONICO { get; set; }
        public DateTime daFDALTA { get; set; }
        public int nFIIDESTATUS { get; set; }
        public int nFIIDAGREGADO_POR { get; set; }
    }
}
