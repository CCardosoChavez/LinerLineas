using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Estatus_Desperfecto_Contenedor
    {
        public int nFIIDESTATUS_DESPERFECTO_CONTENEDOR { get; set; }
        public string sESTATUS { get; set; }
        public DateTime daFDFECHA_ALTA { get; set; }
        public Estatus_Registros rESTATUS_REGISTROS { get; set; }
    }
}
