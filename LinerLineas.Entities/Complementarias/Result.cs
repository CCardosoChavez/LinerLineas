using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Complementarias
{
    public class Result
    {
        public bool Correct { get; set; }
        public object Object { get; set; }
        public List<object> Objects { get; set; } = new List<object>();

        public Exception Ex { get; set; }
        public string ErrorMesagge { get; set; }
        
        //Mensaje para envio de Correo
        public string Mensaje { get; set; }
        public string Correo { get; set; }
    }
}
