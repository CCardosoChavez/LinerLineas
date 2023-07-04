using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Tablas
{
    public class Aplicaciones_Remesas
    {
        public int nFIIDAPLICACION_REMESA { get; set; }
        public Remesas_AX rREMESAS_AX { get; set; }
        public Catalogos.Montos_Aplicables rMONTOS_APLICABLES { get; set; }
        public string sFSNUM_CONT { get; set; }
        public DateTime? daFDALTA { get; set; }
        public Catalogos.Estatus_Registros rESTATUS_REGISTROS { get; set; }

    }
}
