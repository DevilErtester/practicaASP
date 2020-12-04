using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PracticaASP.NET
{
    public partial class ruta : System.Web.UI.Page
    {
        private BD bd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null )
            {
                Response.Redirect("loginForm.aspx");
            }
            else if (Session["ruta"] == null)
            {
                Response.Redirect("user.aspx");
            }
            else
            {
            }
            bd = new BD();
            bd.Connect();


        }
    }
}