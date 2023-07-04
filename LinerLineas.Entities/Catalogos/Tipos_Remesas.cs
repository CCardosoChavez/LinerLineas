using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Tipos_Remesas
    {
        public int nFIIDTIPO_REMESA { get; set; } // FIIDTIPO_REMESA
        public string sFSTIPO_REMESA { get; set; } // FSTIPO_REMESA
        public DateTime daFDALTA { get; set; } //FDALTA
        public Estatus_Registros rESTATUS_REGISTROS { get; set; } //FIIDESTATUS
    }
}
