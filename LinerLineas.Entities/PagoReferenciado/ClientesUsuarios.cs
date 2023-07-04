using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.PagoReferenciado
{
    public class ClientesUsuarios
    {
        public int nIdClienteUsuario { get; set; }
        public Cliente rCLIENTE { get; set; }
        public AspNetUsers rASPNET_USERS { get; set; }
        public bool bActivo { get; set; }
    }
}
