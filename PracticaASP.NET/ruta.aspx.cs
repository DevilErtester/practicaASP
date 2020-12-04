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
            Ruta ruta = (Ruta)Session["ruta"];

            List<Coment> coments = bd.GetComents(ruta.id);
            string html = "";
            int i = 0;
            foreach (Coment c in coments)
            {
                html += "<div>";
                html += "<h4>";
                html += c.nick=bd.GetNick(c.userID) + ":";
                html += "</h4>";
                html += "<p> ";
                html +=  c.comentarioTexto;
                html += "</p>";
               
                html += "<br />";
                html += "</div>";
               
            }
            divComents.InnerHtml = html;
           
        }
    }
}