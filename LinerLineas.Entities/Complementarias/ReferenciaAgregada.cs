using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Complementarias
{
    public class ReferenciaAgregada
    {
        public bool Correct { get; set; }

        //Mensaje para envio de Correo
        public string Mensaje { get; set; }
        public string Correo { get; set; }
        public int nIdReferencia { get; set; }
        public string sReferencia { get; set; }
    }
}
