using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            foreach (Coment c in coments)
            {
                html += "<div>";
                html += "<h4>";
                html += c.nick=bd.GetNick(c.userID) + ":";
                html += "</h4>";
                html += "<p> ";
                html +=  c.comentarioTexto;
                html += "</p>";
                if (c.imgPath!="")
                {
                    html += "<img src='img/" + c.imgPath + "' alt='Not Found' />";
                }
                html += "<br />";
                html += "</div>";
               
            }
            divComents.InnerHtml = html;
        }

        protected void newComent_Click(object sender, EventArgs e)
        {
            Coment c = new Coment();
            c.comentarioTexto = comment.InnerText;

            if (Uploader.HasFile)
            {
                try
                {
                    if (Uploader.PostedFile.ContentType == "image/jpeg"|| Uploader.PostedFile.ContentType == "image/png" || Uploader.PostedFile.ContentType == "image/jpg")
                    {
                        if (Uploader.PostedFile.ContentLength < 102400000)
                        {
                            string filename = Uploader.FileName;
                            Uploader.SaveAs(MapPath("~/img/") + filename);
                            c.imgPath = filename;
                            labelError.Text = "Upload status: File uploaded!";
                        }
                                          }
                    else
                    {
                        labelError.Text = "Upload status: Only Images are accepted!";
                        

                        System.IO.StreamWriter fp;

                        try
                        {
                            
                            fp = System.IO.File.AppendText(Server.MapPath("~/img/") + "log.txt");
                            fp.WriteLine("File is not an image");
                            labelError.Text = "File Succesfully created!";
                            fp.Close();
                        }
                        catch (Exception ex)
                        {
                            labelError.Text = "File Creation failed. Reason is as follows" + ex.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    labelError.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            else
            {
                c.imgPath = null;
            }
            Ruta ruta = (Ruta)Session["ruta"];
            c.idRuta = ruta.id;
            Usuari user = (Usuari)Session["user"];
            c.userID = int.Parse(user.id);
            if(Page.IsPostBack)
            bd.NewComent(c);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}