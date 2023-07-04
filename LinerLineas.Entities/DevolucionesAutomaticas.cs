using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities
{
    public class DevolucionesAutomaticas
    {
        public string sNUM_BL { get; set; }
        public int nNUM_INTINEI { get; set; }
        public int nTIPO_REV { get; set; }
        public string sDESC_REV { get; set; }
        public decimal dADEUDO_FLETES { get; set; }
        public decimal dADEUDO_DEMORAS { get; set; }
        public decimal  dADEUDO_DANIOS { get; set; }
        public int nNUM_CIA { get; set; }
        public string sNOMBRE_COMPANIA { get; set; }
        public int nID_REFERENCIA { get; set; }

        //Porpiedades para consulta
        public string sBL { get; set; }
        public int nINTINEI { get; set; }
        public int nLINEA { get; set; }

        //Propiedades para obtener el estatus de la devolución del BL
        public int nESTATUS_DEVOLUCION { get; set; }
    }
}
