using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Catalogos
{
    public class Bancos_Datos_Bancarios
    {
        public int nFIIDBANCO { get; set; }
        public string sFSDESCRIPCION { get; set; }
        public int nFNCLAVE { get; set; }
        public string sFSRAZON_SOCIAL { get; set; }
        public DateTime? daFDALTA { get; set; }
        public Estatus_Registros rEstatus_Registros { get; set; }
        public List<object> liBancos { get; set; }
    }
}
