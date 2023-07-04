using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Montos_Aplicables
    {
        public int nFIIDMONTO_APLICABLE { get; set; } //FIIDMONTO_APLICABLE
        public Tipos_Remesas rTIPOS_REMESAS { get; set; } //FIIDTIPO_REMESA
        public double doFNMONTO_APLICABLE { get; set; } //FNMONTO_APLICABLE
        public DateTime daFDINICIO_VIGENCIA { get; set; } //FDINICIO_VIGENCIA
        public DateTime daFDFIN_VIGENCIA { get; set; } // FDFIN_VIGENCIA
        public Estatus_Registros rESTATUS_REGISTROS { get; set; } // FIIDESTATUS
        public DateTime daFDALTA { get; set; } // FDALTA
    }
}
