using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
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
    }
}