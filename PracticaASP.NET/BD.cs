using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace PracticaASP.NET
{
    
    public class BD
    {
        string strConnString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        private MySqlConnection connection;



        public void Connect()
        {
            connection = new MySqlConnection(strConnString);
            connection.Open();
        }
        public Usuari getUser(string user, string pass)
        {
            String sql = "select * from users where username='" + user + "' and pass='" + pass + "'";
            Usuari p = null;
            MySqlCommand cmd = new MySqlCommand(sql,connection);
            MySqlDataReader sdr = cmd.ExecuteReader();
            p = new Usuari();
            if (sdr.Read())
            {
                
                p.id = sdr[0].ToString();
                p.email = sdr[1].ToString();
                p.nick = sdr[2].ToString();
                p.pass = sdr[3].ToString();
                p.hash = sdr[4].ToString();
                p.rol = sdr[5].ToString();
                sdr.Close();
                return p;
            }
            else
            {

                p.id = null;
                sdr.Close();
                return p;
            }
            
        
        }
        public bool newUser(Usuari p)
        {
            string sql = "INSERT INTO users( username, pass, hash, nickname) VALUES ('"+p.email+"','"+p.pass+"','"+p.hash+ "','" + p.nick + "');";
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            return true;
        }
        public List<Categoria> getCategorias()
        {
            List<Categoria> categories = new List<Categoria>();

            String sql = "SELECT * FROM categorias order by parentid";

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            MySqlDataReader mdr = cmd.ExecuteReader();
            while (mdr.Read())
            {
                Categoria c = new Categoria();
                c.id = Convert.ToInt32(mdr[0].ToString());
                c.name = mdr[1].ToString();
                try { 
                c.ParentId = Convert.ToInt32(mdr[2].ToString());
                }
                catch (FormatException FE)
                {
                    c.ParentId = 0;
                }
                categories.Add(c);
            }
            mdr.Close();

            return categories;
        }
        public string encrypt(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }
        /*
             Para obtener todas las rutas de dicha categoria debemos hacer un select * from rutas where categoria='"+tvwCategorias.SelectedNode.Text+"'
             haremos lo mismo en la pagina de usuario con el cambio de que en esa pagina no debemos cambiar el contenido de las rutas ni el de las categorias
             crear metodo getRutas y la clase Ruta.
             Crear tambien la tabla rutas en la BBDD
             Para obtener la dificultad de la ruta de 0 a 5 haremos un AVG de los campos de dificultadRuta donde la id de ruta sera el mismo de la ruta que estemos valorando
            */
        public List<Ruta> getRutas(String categoria)
        {
            List<Ruta> rutas = new List<Ruta>();

            String sql = "SELECT * FROM rutas join categorias where categorias.name='"+categoria+"'";

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            MySqlDataReader mdr = cmd.ExecuteReader();
            while (mdr.Read())
            {
                Ruta r = new Ruta();
                r.id = Convert.ToInt32(mdr[0].ToString());
                r.Origen = mdr[1].ToString();
                r.Destino = mdr[2].ToString();
                r.idCategoria = Convert.ToInt32(mdr[3].ToString());
                rutas.Add(r);
            }
            mdr.Close();

            return rutas;
        }
    }
}