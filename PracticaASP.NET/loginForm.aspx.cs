using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using System.Text;

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
            string pass = bd.Encrypt(password.Text);
            string uid = email.Text;
            Usuari user = bd.GetUser(uid,pass);

            if (user.id != null)
            {
                if (user.rol == "1")
                {
                    Session["admin"] = true;
                    Response.Redirect("admin.aspx");
                }
                else if(user.rol == "0")
                {
                    Session["user"] = user;
                    Response.Redirect("user.aspx");
                }
            }
            else
            {
                Usuari userAux = new Usuari
                {
                    email = uid,
                    pass = pass.ToString(),
                };
                string hash = bd.Encrypt(email.Text);
                userAux.hash = hash;
                Label4.Text = "Usuario no creado, hemos enviado un mail a la direccion que has especificado para la creacion del Usuario.";
                try {
                    SEND_mail(email.Text, userAux.hash);
                    Session["user"] = userAux;
                    Response.Redirect("verify.aspx");
                } catch (Exception ex)
                {
                    Label4.Text = "Formato de mail erroneo";
                }
            }
        }
        private void SEND_mail(string mail, string hash)
        {
            string url = "Este es tu codigo de verificacion: " + hash;

            var fromAddress = new MailAddress("marioproves1@gmail.com", "Mario Chaves");
            var toAddress = new MailAddress(mail, "To Name");
            const string fromPassword = "NSvH2Aubg4KzWKr";
            const string subject = "Codigo de verificacion";
            string body = url;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
