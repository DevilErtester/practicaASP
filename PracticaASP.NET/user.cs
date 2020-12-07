using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticaASP.NET
{
    public class Usuari
    {
        public String id { get; set; }
        public String nick { get; set; }
        public String email { get; set; }
        public String pass { get; set; }
        public String hash { get; set; }
        public String rol { get; set; }

        public Usuari()
        {
        }
    }
    public class Categoria
    {
        public int id { get; set; }
        public String name { get; set; }
        public int ParentId { get; set; }

        public Categoria()
        {
        }
    }
    public class Ruta
    {
        public int id { get; set; }
        public String Origen { get; set; }
        public String Destino { get; set; }
        public int idCategoria { get; set; }
        public float dif { get; set; }
        public Ruta()
        {
        }
    }
    public class Coment
    {
        public int comentariID { get; set; }
        public String data { get; set; }
        public int userID { get; set; }
        public int idRuta { get; set; }
        public String comentarioTexto { get; set; }
        public String imgPath { get; set; }
        public String nick { get; set; }
        public Coment()
        {
        }
    }

}