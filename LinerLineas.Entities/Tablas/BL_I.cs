using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Tablas
{
    public class BL_I
    {
        public string sNUM_BL { get; set; }
        public string sSTATUS { get; set; } //CHAR
        public DateTime daFEC_CANCEL { get; set; }
        public int nNUM_ITINE { get; set; }
        public Catalogos.Puertos PTO_CGA { get; set; }
        //public int NUM_TER { get; set; } FK
        //public int NUM_TM { get; set; } FK
        //public int NUM_AGTE { get; set; } FK
        public string sTIPO_MANI { get; set; } //CHAR
        public string sBLOQUEADO { get; set; } //CHAR
        public Catalogos.Puertos rPTO_DES { get; set; }
        public DateTime daFEC_POS { get; set; }
        public string sEMBARCADOR { get; set; }
        public string sCONSIGNATA { get; set; }
        public string sNOTIFICA { get; set; }
        public string sTIPO_CGA { get; set; } //CHAR
        public int nDIAS_LIB { get; set; }
        public int nDIAS_TAR1 { get; set; }
        public string sUSR_AUTO { get; set; }
        public DateTime daFEC_AUTO { get; set; }
        public decimal dPESO_BB { get; set; }
        public int nBULTOS_BB { get; set; }
        public decimal dDESCTO_DEM { get; set; }
        public string sUSR_RECT { get; set; }
        public DateTime daFEC_RECT { get; set; }
        public string sUSR_ALTA { get; set; }
        public DateTime daFEC_ALTA { get; set; }
        public string sUSR_MOD { get; set; }
        public DateTime daFEC_MOD { get; set; }
        public string sUSR_ACTFTE { get; set; }
        public DateTime daFEC_ACTFTE { get; set; }
        public DateTime daF_REVAL { get; set; }
        public string sUSR_REVAL { get; set; }
        public string sUSR_TREV { get; set; }
        public DateTime daFEC_TREV { get; set; }
        public string sTIPO_REV { get; set; } //CHAR
        public DateTime daFEC_TCTE { get; set; }
        public string sUSR_TCTE { get; set; }
        public decimal dTAR_TCTE { get; set; }
        public decimal sTIPO_PZA { get; set; } //NUMERIC
        public decimal dTIPO_MERC { get; set; } //NUMERIC
        public decimal dCVE_ADU { get; set; } //NUMERIC
        public decimal dSEC_RCNT { get; set; } //NUMERIC
        public decimal dCVE_RCNT { get; set; } //NUMERIC
        public decimal dNUM_TRANS { get; set; } //NUMERIC
        public string sACUSE { get; set; }
        public int nPTO_ORI { get; set; }
        public string sNOM_CONSIG { get; set; }
        public decimal dREG_CONSIG { get; set; } //NUMERIC
        public string sMDIR_CONSIG { get; set; }
        public string sRFC_CONSIG { get; set; }
        public string sNOM_CTE { get; set; }
        public string sNOM_NOT { get; set; }
        public string sNOM_BL_R { get; set; }
        public string sDIR_EMB { get; set; }
        public string sDIR_NOT { get; set; }
        public string sREFERENCIA { get; set; }
        public int nPTO_TRANS { get; set; }
        public DateTime? daFEC_DEPO { get; set; }
        public string sNUM_UNICO_I { get; set; } //CHAR
        public decimal dCLASE { get; set; }
        public int nUNNMBR { get; set; }
        public Int16 nsMTIPO_MERC_PELIG { get; set; } //SMALLINT
        public string sTEL_CONTACTO { get; set; }
        public string sNOM_CONTACTO { get; set; }
        public int nNUM_NOTIF { get; set; }
        public string sMRFC_EMB { get; set; }
        public string sRFC_NOT { get; set; }
        public int nDIAS_CRED_FTE { get; set; }
        public string sFACTURA_FE { get; set; } //CHAR
        public string sSERIE_FE { get; set; }
        public int nFOLIO_FE { get; set; }
        public DateTime daFECHA_FE { get; set; }
        public string sUSR_FE { get; set; }
        public int nDIAS_CRED_FE { get; set; }
        public DateTime daFEC_CRED_FE { get; set; }
        public string sUSR_CRED_FE { get; set; }
        public string sPAGO { get; set; } //CHAR
        public string sUSR_PAGO { get; set; }
        public DateTime daFEC_PAGO { get; set; }
        public string sRecId { get; set; } //NVARCHAR

        //Propiedades de Navegacion
        public Referencias rREFERENCIAS { get; set; }
        public Stock rSTOCK { get; set; }
        public Remesas_AX rREMESAS_AX { get; set; }
        public Catalogos.Estatus_Devoluciones rESTATUS_DEVOLUCIONES { get; set; }
        public Control_Saldo rCONTROL_SALDO { get; set; }
        public Control_Devoluciones rCONTROL_DEVOLUCIONES { get; set; }

        //Propiedad para campos extra en la consulta
        public Complementarias.CamposExtra rCAMPOSEXTRA { get; set; }

        //
        public string sUSER_ID { get; set; }
    }
}
