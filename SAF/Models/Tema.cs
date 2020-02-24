using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAF.Models
{
    public class Tema
    {
        public int idTema { get; set; }
        public string nombre { get; set; }
        public List<Tema> liste { get; set; }
        public Clase clase { get; set; }
    }
}