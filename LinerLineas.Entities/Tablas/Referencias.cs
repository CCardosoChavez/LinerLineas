using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities.Complementarias;
using LinerLineas.Entities.Catalogos;

namespace LinerLineas.Entities.Tablas
{
    public class Referencias
    {
        public int nIdReferencia { get; set; } // IdReferencia
        public string sReferencia { get; set; } // Referencia
        public Extranet.Catalogos.Clientes rCLIENTE { get; set; } // IdCliente
        public string sNUM_BL { get; set; } // Numero de BL
        public int nNUM_ITINE { get; set; } // Numero de Itine
        public DateTime? daFechaRegistro { get; set; } // Fecha Registro
        public long nlRecIdClienteAX { get; set; }
        public decimal dMontoMXN { get; set; } // MontoMXN
        public decimal dTipoCambio { get; set; } // Tipo de Cambio
        public decimal dMontoUSD { get; set; } // MontoUSD

        //Propiedad para la busqueda
        public DatosBusqueda rDATOS_BUSQUEDA { get; set; }

        //Propiedades de navegación
        public CamposExtra rCAMPOS_EXTRA { get; set; }
        public Lineas rLINEAS { get; set; }
        public Buques rBUQUES { get; set; }
        public Puertos rPUERTOS { get; set; }
        public Remesas_AX rREMESAS_AX { get; set; }
        public Itinerario rITINERARIO { get; set; }
        public PagoReferenciado.AspNetUsers rASPNET_USERS { get; set; }
        public PagoReferenciado.AspNetRoles rASPNET_ROLES { get; set; }
        public Control_Devoluciones rCONTROL_DEVOLUCIONES { get; set; }

        //Proiedad para cantidad de contenedores
        public int nCANTIDAD_CONTENEDORES { get; set; }

        //Para la posicion Actual  en el paginado 
        public Paginacion rPAGINACION { get; set; }

        //Para habilitar el boton de Solicitud de Devolucion 
        public int nEstatusContenedoresControlEquipo { get; set; }
        public string sEstatusDatosBancariosTesoreria { get; set; }

        //Para actualizar o insertar datos segun sea el caso
        public int nIdDatosBancarios { get; set; }
        public Datos_Bancarios_Referencia rDatos_Bancarios_Referencia { get; set; }
    }
}
