using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities.Extranet.Catalogos;

namespace LinerLineas.Entities.Extranet.Tablas
{
    public class Usuarios_Rel_Clientes
    {
        public int nFIIDUSUARIO_REL_CLIENTES { get; set; }
        public Usuarios rUSUARIOS { get; set; }
        public Clientes rCLIENTES { get; set; }
        public DateTime daFDALTA { get; set; }
        public int nFIIDESTATUS { get; set; }
        public int nFIIDAGREGADO_POR { get; set; }
    }
}
