using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities;

namespace LinerLineas.Entities.Tablas
{
    public class Stock
    {
        public int nNUM_ITINEI { get; set; }
        public string sNUM_CONT { get; set; }
        public Catalogos.Lineas rLINEAS { get; set; }
        public Catalogos.Tipo_Conte rTIPO_CONTE { get; set; }
        public Catalogos.Tama_Conte rTAMA_CONTE { get; set; }
        public Catalogos.Estatus_Registros rESTATUS_REGISTROS { get; set; }
        public string sEDO_FIS_C { get; set; }
        public string sNUM_BL_I { get; set; }
        public Catalogos.Puertos rPTO_CGA_I { get; set; }
        public DateTime daF_DESCGAI { get; set; }
        public string sBLS_LCL_I { get; set; }
        //public string NUM_AGTE_I { get; set; } FK
        public string sSELLO_I { get; set; }
        public decimal dTONS_I { get; set; }
        public string sFOLIO_I { get; set; }
        public DateTime daF_IMP_TMP { get; set; }
        public int nBULTOS_I { get; set; }
        //public int NUM_PROD_I { get; set; } FK
        public decimal dCLASE { get; set; }
        public int nIMO { get; set; }
        public int nUNNBR { get; set; }
        public int nTEMP_MIN { get; set; }
        public int nTEMP_MAX { get; set; }
        public DateTime daF_REVAL { get; set; }
        public DateTime? daF_DEV_CTE { get; set; }
        public DateTime daF_ENTPATIO { get; set; }
        public Catalogos.Patios rPATIOS { get; set; }
        public int nCARRIL { get; set; }
        public int nBLOQUE { get; set; }
        public int nTONGA { get; set; }
        public int nESTIBA { get; set; }
        public DateTime daF_SALPATIO { get; set; }
        public Catalogos.Itinerario rITINERARIO { get; set; }
        public Catalogos.Companias rCOMPANIAS { get; set; }
        //public decimal NUM_BOOK { get; set; } FK
        public string sBLS_LCL_E { get; set; }
        public Catalogos.Puertos rPTO_DESC_E { get; set; }
        //public int NUM_AGTE_E { get; set; } FK
        public string ssSELLO_E { get; set; }
        public string sAUTOR_REPA { get; set; }
        public DateTime daF_REPA { get; set; }
        public DateTime daF_INTERC { get; set; }
        public DateTime daF_EXP_TMP { get; set; }
        public string sTIPO_MANI { get; set; } //CHAR
        public string sFOLIO_E { get; set; }
        public DateTime daF_RECTIF { get; set; }
        public string sSHIPPER_O { get; set; } //CHAR
        public decimal sPESO_E { get; set; }
        public int nBULTOS_E { get; set; }
        public DateTime daF_ENT_TRAN { get; set; }
        public DateTime daF_ENT_PLAN { get; set; }
        public DateTime daF_RETORNO { get; set; }
        public DateTime daF_SAL_PLAN { get; set; }
        public string sUSR_DEVOL { get; set; }
        public int nTIPO_MERC { get; set; }
        public string sCOD_TIPO { get; set; }
        public string sUSR_SALPATIO { get; set; }
        public DateTime daF_USR_SALPATIO { get; set; }
        public DateTime daF_RETPATIO { get; set; }
        public string sUSR_RETPATIO { get; set; }
        public DateTime daF_USR_RETPATIO { get; set; }
        public string sUSR_DESCGAI { get; set; }
        public DateTime daF_USR_DESCGAI { get; set; }
        public DateTime daF_CAP_DEV_CTE { get; set; }
        public DateTime daF_MOD_DEV_CTE { get; set; }
        public string sU_MOD_DEV_CTE { get; set; }

        //Propiedades de Navegacion
        public BL_I rBL_I { get; set; }
        public Referencias rREFERENCIAS { get; set; }
        public Extranet.Catalogos.Clientes rCLIENTES { get; set; }
        public Control_Saldo rCONTROL_SALDO { get; set; }
        public Remesas_AX rREMESAS_AX { get; set; }
        public Desperfecto_Contenedor rDESPERFECTO_CONTENEDOR { get; set; }
        public Complementarias.DatosBusqueda rDATOS_BUSQUEDA { get; set; }
        public Complementarias.CamposExtra rCAMPOS_EXTRA { get; set; }
        public Control_Devoluciones rCONTROL_DEVOLUCIONES { get; set; }
        public Catalogos.Montos_Aplicables rMONTOS_APLICABLES { get; set; }
    }
}
