using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Tablas
{
    public class Desperfecto_Contenedor
    {
        public int nFIIDDESPERFFTO_CONTENEDOR { get; set; }
        public decimal dCOSTO_REPARACION_MXN { get; set; }
        public decimal dCOSTO_REPARACION_USD { get; set; }
        public decimal dTIPO_CAMBIO { get; set; }
        public Catalogos.Montos_Aplicables rMONTOS_APLICABLES { get; set; }
        public Catalogos.Estatus_Desperfecto_Contenedor rESTATUS_DESPERFECTO_CONTENEDOR { get; set; }
        public string sFSNUMERO_CONTENEDOR { get; set; }
        public DateTime? daFECHA_ALTA { get; set; }
        public Tablas.Referencias rREFERENCIA { get; set; }
        public Extranet.Catalogos.Usuarios rUSUARIOS { get; set; }

        //Propiedades de Navegación
        public BL_I rBL_I { get; set; }
    }
}
