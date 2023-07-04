using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities.Extranet.Catalogos;

namespace LinerLineas.Entities.Extranet.Tablas
{
    public class Usuarios_Rel_Lineas
    {
        public int nFIIDUSUARIO_REL_LINEAS { get; set; }
        public Usuarios rUSUARIOS { get; set; }
        public Lineas rLINEAS { get; set; }
        public DateTime daFDALTA { get; set; }
        public int nFIIDESTATUS { get; set; }
        public int nFIIDAGREGADO_POR { get; set; }
    }
}
