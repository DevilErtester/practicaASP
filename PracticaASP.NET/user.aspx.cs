using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace PracticaASP.NET
{
    public partial class user : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
    }
}