using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Entities.Complementarias
{
    public class Paginacion
    {
        public int nPaginaActual { get; set; }
        public int nTotalDeRegistros { get; set; }
        public int nRegistrosPorPagina { get; set; }
    }
}
