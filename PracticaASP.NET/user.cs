using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticaASP.NET
{
    public class Usuari
    {
        public String id { get; set; }
        public String username { get; set; }
        public String pass { get; set; }
        public String hash { get; set; }
        public String rol { get; set; }

        public Usuari()
        {
        }
    }

}