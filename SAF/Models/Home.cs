using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAF.Models
{
    public class Home
    {
        public int matricula { get; set; }
        public string password { get; set; }

        public Home(int matricula, string password)
        {
            this.matricula = matricula;
            this.password = password;
        }
       
    }
}