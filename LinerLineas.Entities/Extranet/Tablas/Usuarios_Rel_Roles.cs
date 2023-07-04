using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities.Extranet.Catalogos;

namespace LinerLineas.Entities.Extranet.Tablas
{
    public class Usuarios_Rel_Roles
    {
        public int nFIIDUSUARIO_REL_ROLES { get; set; }
        public Usuarios rUSUARIOS { get; set; }
        public Roles rROLES { get; set; }
        public DateTime daFDALTA { get; set; }
        public int nFIIDESTATUS { get; set; }
        public int nFIIDAGREGADO_POR { get; set; }
    }
}
