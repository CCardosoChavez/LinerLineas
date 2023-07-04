using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Tablas
{
    public class Estatus_Dano_Contenedores
    {
        public string sFSCONTENEDOR { get; set; }
        public string sFSBL { get; set; }
        public Entities.Catalogos.Estatus_Dano_Contenedores rCAT_ESTATUS_DANO_CONTENEDORES { get; set; }
        public DateTime FDFECHA_ALTA { get; set; }
    }
}
