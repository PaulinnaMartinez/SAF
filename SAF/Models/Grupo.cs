using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SAF.Models
{
    public class Grupo
    {
        public int idGrupo { get; set; }
        public string facultad { get; set; }
        public string salon { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public double costo { get; set; }
        public string maestro { get; set; }
        public string activo { get; set; }
        public Clase clase { get; set; }
        public List<Tema> listTema { get; set; }
        public Usuario usuario { get; set; }



        //public List<Clase> clase { get; set; }
    }
}