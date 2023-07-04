using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.PagoReferenciado
{
    public class LineasUsuario
    {
        public int nIdLineaUsuario { get; set; }
        public AspNetUsers rAsPNET_USERS { get; set; }
        public Lineas rLINEAS { get; set; }
        public bool bActivo { get; set; }
    }
}
