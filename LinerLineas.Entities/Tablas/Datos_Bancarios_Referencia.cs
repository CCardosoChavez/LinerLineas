using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Tablas
{
    public class Datos_Bancarios_Referencia
    {
        public int nFIIDDATOS_BANCARIOS { get; set; }
        
        public string sFSRAZONSOCIAL { get; set; }
        public string sFSBANCO { get; set; }

        [StringLength(20, MinimumLength = 20, ErrorMessage = "El número de cuenta debe tener 10 digitos")]
        public string sFSNUMERO_CUENTA { get; set; }
        
        [StringLength(18, MinimumLength = 18, ErrorMessage = "La clabe bancaria debe de tener 10 digitos")]
        public string sFSNUMERO_CLAVE_CUENTA { get; set; }
        
        public DateTime? daFDAFECHA_SOLICITUD { get; set; }
        public Archivo_Recibo_Deposito_En_Garantia rARCHIVO_RECIBO { get; set; }
        public ReferenciasTesoreria rREFERENCIAS_TESORERIA { get; set; }

        public Referencias rREFERENCIAS { get; set; }

        public int nIdEstatusReferencia { get; set; }
        public string sFSOBSERVACIONES { get; set; }
        public Catalogos.Bancos_Datos_Bancarios rBancos_Datos_Bancarios { get; set; }
        public string sFSEMAIL_CONTACTO { get; set; }
    }
}
