using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities.Tablas;

namespace LinerLineas.Entities
{
    public class ControlDevolucion
    {
        public int FIIDCONTROL_DEVOLUCIONES { get; set; }
        public int nIdReferencia { get; set; }
        public string sBL { get; set; }
        public int nNumeroItinerario { get; set; }
        public string sContenedor { get; set; }
        public int FIIDESTATUS_DEVOLUCION { get; set; }
        public DateTime? FDALTA { get; set; }
        public int FIIDESTATUS { get; set; }
        public DateTime? FDDEVOLUCION { get; set; }
        public string FIRECID_DEVOLUCION { get; set; }
        public string sID_USUARIO { get; set; }

        //Parametros para actualizar el estatus de los datos bancarios
        public Datos_Bancarios_Referencia rDATOS_BANCARIOS_REFERENCIAS { get; set; }
    }
}
