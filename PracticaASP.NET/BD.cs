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
                p.username = sdr[1].ToString();
                p.pass = sdr[2].ToString();
                p.hash = sdr[3].ToString();
                p.rol = sdr[4].ToString();
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
            string sql = "insert into users (mail) values ('" + p.username + "');";

            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            return true;
        }
    }
}