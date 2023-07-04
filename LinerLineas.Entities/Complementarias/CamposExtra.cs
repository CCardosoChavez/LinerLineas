using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Complementarias
{
    public class CamposExtra
    {
        //Metodo: ObtenerContenedores Control:Contenedores
        public double dMONTO_MXN { get; set; }
        public double dMONTO_USD { get; set; }
        public double dOTROS_ADEUDOS { get; set; }
        public int bDEVOLUCION_DEPOSITO_EN_GARANTIA { get; set; }

        //Metodo: GetAllUndamagedContainer Control:Control Equipo
        public string sESTATUS_DEPOSITO { get; set; }

        //Metodo: UpdateDesperfectoContenedor Control : Control Equipo
        public int nFIIDREFERENCIA { get; set; }
        public string sNUM_CONT { get; set; }

        //Metodo GetContenedores Control: ControlEquipo
        public string sCONTENEDOR_PERTENECE_CLIENTE { get; set; }
        public int nESTATUS_DEVOLUCION_DG { get; set; }

        //Porpiedad General
        public string sESTATUS { get; set; }

        //Adeudos 
        public decimal dADEUDO_FLETES { get; set; }
        public decimal dADEUDO_DEMORAS { get; set; }
        public decimal dADEUDO_DANIO_CONTENEDOR { get; set; }
        public int nEstatusDanioContenedor { get; set; }

        //Adeudo de referencia
        public decimal dMONTO_FALTANTE { get; set; }

        //Deposito en Garantia pagado por contenedor
        public bool bCONTENEDOR_PAGADO { get; set; }
    }
}
