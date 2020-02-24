using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SAF.Models
{
    public class Usuario
    {
        public int matricula { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]
        public string password2 { get; set; }
        public string modalidad { get; set; }
        


    }
}


