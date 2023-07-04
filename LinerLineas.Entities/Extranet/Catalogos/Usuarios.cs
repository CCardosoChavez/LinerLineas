using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities.Extranet.Tablas;

namespace LinerLineas.Entities.Extranet.Catalogos
{
    public class Usuarios
    {
        public int nFIIDUSUARIO { get; set; }
        public string sFSUSUARIO { get; set; }
        public string sFSCONTRASENA { get; set; }
        public string sFSCORREO { get; set; }
        public DateTime daFDALTA { get; set; }
        public int nFIIDESTATUS { get; set; }
        public string sFSNOMBRE { get; set; }
        public string sFSAPELLIDOS { get; set; }
        public int nCODIGO_VALIDACION { get; set; }
        public int nESTATUS_CODIGO { get; set; }
        public bool bFBCAMBIO_CONTRASENA { get; set; }
        public int FIIDAGREGADO_POR { get; set; }

        //Propiedades de relación
        public Usuarios_Rel_Agentes rUSUARIOS_REL_AGENTES { get; set; }
        public Usuarios_Rel_Clientes rUSUARIOS_REL_CLIENTES { get; set; }
        public Usuarios_Rel_Ejecutivos rUSUARIOS_REL_EJECUTIVOS { get; set; }
        public Usuarios_Rel_Lineas rUSUARIOS_REL_LINEAS { get; set; }
        public Usuarios_Rel_Perfiles rUSUARIOS_REL_PERFILES { get; set; }
        public Usuarios_Rel_Roles rUSUARIOS_REL_ROLES { get; set; }

        //Propiedad para el login
        public List<string> liROLES_LOGIN { get; set; } = new List<string>();
    }
}
