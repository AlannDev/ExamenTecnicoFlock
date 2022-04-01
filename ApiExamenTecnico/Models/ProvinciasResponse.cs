using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExamenTecnico.Models
{
    public class ProvinciasResponse
    {
        public int cantidad { get; set; }
        public int inicio { get; set; }
        public Parametros parametros { get; set; }
        public List<Provincia> provincias { get; set; }
        public int total { get; set; }
    }
}
