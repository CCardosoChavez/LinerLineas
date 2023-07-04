using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Estatus_Devoluciones
    {
        public int nFIIDESTATUS_DEVOLUCION { get; set; }
        public string sFSESTATUS_DEVOLUCION { get; set; }
        public DateTime daFDALTA { get; set; }
        public Estatus_Registros rESTATUS_REGISTROS { get; set; }
    }
}
