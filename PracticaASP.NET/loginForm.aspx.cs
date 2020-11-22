using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace PracticaASP.NET
{


    public partial class loginForm : System.Web.UI.Page
    {
        private BD bd;
        protected void Page_Load(object sender, EventArgs e)
        {
            bd = new BD();
            bd.Connect();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string pass = password.Text;
            string uid = username.Text;
            Usuari user = bd.getUser(uid,pass);

            if (user.id != null)
            {
                if (user.rol == "1")
                {
                    Session["admin"] = true;
                    Response.Redirect("admin.aspx");
                }
                else if(user.rol == "0")
                {
                    Session["user"] = true;
                    Response.Redirect("user.aspx");
                }
            }
            else
            {
                Label4.Text = "UserId & Password Is not correct Try again..!!";
            }
        }  
    }
}
