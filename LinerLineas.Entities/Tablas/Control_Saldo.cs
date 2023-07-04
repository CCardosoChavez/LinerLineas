using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Tablas
{
    public class Control_Saldo
    {
        public int nFIIDCONTROL_SALDO { get; set; }
        public Aplicaciones_Remesas rAPLICACIONES_REMESAS { get; set; }
        public int nFIIDAPLICACION_REMESA { get; set; }
        public decimal dFNSALDO_ADEUDO_DEPOSITO { get; set; }
        public int nFIIDUSUARIO_REGISTRO { get; set; }
        public DateTime? daFDALTA { get; set; }
        public Catalogos.Estatus_Registros rESTATUS_REGISTROS { get; set; }
        public decimal dFNSALDO_RESTANTE { get; set; }
    }
}
