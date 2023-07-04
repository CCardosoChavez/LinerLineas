using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Tablas
{
    public class Control_Devoluciones
    {
        public int nFIIDCONTROL_DEVOLUCIONES { get; set; }
        public Referencias rREFERENCIAS { get; set; }
        public Catalogos.Estatus_Devoluciones rESTATUS_EVOLUCIONES { get; set; }

        public DateTime? daFDALTA { get; set; }
        public Catalogos.Estatus_Registros rESTAUS_REGISTROS { get; set; }
        public DateTime? daFDDEVOLUCION { get; set; }
        public string sFIRECID_DEVOLUCION { get; set; }

        public string sNUM_BL { get; set; }
        public int nNUM_ITINE { get; set; }
        public string sFSNUM_CONT { get; set; }
        public int nIdDatosBancarios { get; set; }


    }
}
