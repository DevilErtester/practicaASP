using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PracticaASP.NET
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private BD bd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("loginForm.aspx");
            }
            else
            {
            }
            bd = new BD();
            bd.Connect();
            
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Usuari userAux = (Usuari)Session["user"];
            userAux.nick = nickname.Text;
            if (verifyCode.Text == userAux.hash)
            {
                if (bd.newUser(userAux))
                {
                    Session["user"] = userAux;
                    Response.Redirect("user.aspx");
                }
            }
            else
            {
                Label4.Text = "Codigo de verificacion erroneo";
            }
        }
    }

}