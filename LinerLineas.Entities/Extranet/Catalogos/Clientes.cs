using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities.Extranet.Tablas;

namespace LinerLineas.Entities.Extranet.Catalogos
{
    public class Clientes
    {
        public int? nFIIDCLIETE { get; set; }
        public string sFSCLIENTE { get; set; }
        public int nFIIDLINER { get; set; }
        public string sFSRFC { get; set; }
        public string sFSCALLE { get; set; }
        public string sFSCOLONIA { get; set; }
        public string sFSDEL_MUN { get; set; }
        public string sFSSCIUDAD { get; set; }
        public DateTime daFDALTA { get; set; }
        public int nFIIDESTATUS { get; set; }
        public long nlFIRECIDCLIENTE_AX { get; set; }
        public long nlFBNOTIFICACION { get; set; }
        public string sFSREPRESENTANTE_LEGAL { get; set; }
        public string sFSDOMICILIO_FISCAL { get; set; }
        public bool bFBAUTOORIZADO { get; set; }

        //Propiedades de navegación
        public Usuarios_Rel_Clientes rUSUARIOS_REL_CLIENTES { get; set; }
    }
}
