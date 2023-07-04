using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities.Extranet.Catalogos;

namespace LinerLineas.Entities.Extranet.Tablas
{
    public class Usuarios_Rel_Ejecutivos
    {
        public int nFIIDUSUARIO_REL_EJECUTIVOS { get; set; }
        public Usuarios rUSUARIOS { get; set; }
        public Ejecutivos rEJECUTIVOS { get; set; }
        public DateTime daFDALTA { get; set; }
        public int nFIIDESTATUS { get; set; }
        public int nFIIDAGREGADO_POR { get; set; }
    }
}
