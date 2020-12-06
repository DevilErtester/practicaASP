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
        public List<Coment> GetComents(int idRuta)
        {
            List<Coment> coments = new List<Coment>();
            String sql = "SELECT comentari.*,nick FROM comentari JOIN users ON comentari.userID=users.userID WHERE idRuta='" + idRuta + "'";

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            MySqlDataReader mdr = cmd.ExecuteReader();

            while (mdr.Read())
            {
                Coment c = new Coment();
                c.comentariID = Convert.ToInt32(mdr[0].ToString());
                c.data = mdr[1].ToString();
                c.userID = Convert.ToInt32(mdr[2].ToString());
                c.idRuta = Convert.ToInt32(mdr[3].ToString());
                c.comentarioTexto = mdr[4].ToString();
                c.imgPath = mdr[5].ToString();
                c.nick = mdr[6].ToString();
                coments.Add(c);

            }
            mdr.Close();
            return coments;
        }
        public void NewComent(Coment c)
        {
            string sql;
            if (c.imgPath != null)
            {
                sql = "INSERT INTO comentari(data, userID, idRuta, comentarioTexto, imgpath) VALUES ('" + c.data + "','" + c.userID + "','" + c.idRuta + "','" + c.comentarioTexto + "','" + c.imgPath + "')";
            }
            else
            {
                sql = "INSERT INTO comentari(data, userID, idRuta, comentarioTexto) VALUES ('" + c.data + "','" + c.userID + "','" + c.idRuta + "','" + c.comentarioTexto + "')";
            }
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
        }
        public String GetNick(int idUser)
        {
            String nick;
            String sql = "SELECT nick FROM users where userid=" + idUser;

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            MySqlDataReader mdr = cmd.ExecuteReader();
            mdr.Read();
            nick = mdr[0].ToString();
            mdr.Close();
            return nick;
        }
        public Usuari GetUser(string user, string pass)
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
        public bool NewUser(Usuari p)
        {
            string sql = "INSERT INTO users( username, pass, hash, nickname) VALUES ('"+p.email+"','"+p.pass+"','"+p.hash+ "','" + p.nick + "');";
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            return true;
        }
        public List<Categoria> GetCategorias()
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
                catch (FormatException)
                {
                    c.ParentId = 0;
                }
                categories.Add(c);
            }
            mdr.Close();

            return categories;
        }
        public string Encrypt(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }

        public List<Ruta> GetRutas(String categoria)
        {
            List<Ruta> rutas = new List<Ruta>();

            String sql = "SELECT rutas.* FROM rutas right join categorias on categorias.name='" + categoria + "' where idCategoria=categorias.id ";

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
        public Ruta GetRuta(int id)
        {
            Ruta r = new Ruta();
            String sql = "SELECT * FROM rutas where idruta='" + id + "'";

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            MySqlDataReader mdr = cmd.ExecuteReader();
            mdr.Read();
            r.id = int.Parse(mdr[0].ToString());
            r.Origen = mdr[1].ToString();
            r.Destino = mdr[2].ToString();
            r.idCategoria = int.Parse(mdr[3].ToString());
            mdr.Close();
            return r;
        }
        public bool NewRuta(int categoria, String dest, String org)
        {
            string sql = "insert into rutas (destino,origen,idcategoria) values ('" + dest + "','" + org + "','" + categoria + "');";

            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            return true;
        }
        public bool DeleteRuta(int ID)
        {
            string sql = "DELETE FROM rutas WHERE idRuta='" + ID + "'";
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            return true;
        }
    }
}