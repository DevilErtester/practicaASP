using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace PracticaASP.NET
{
    public partial class admin : System.Web.UI.Page
    { 
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        private BD bd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("loginForm.aspx");
            }
            else
            {
            }
            bd = new BD();
            bd.Connect();
            List<Categoria> categories = bd.GetCategorias();
            if (!Page.IsPostBack)
            {
                loadTreeView(categories, null);
                dropdownCat();

            }
            if (Session["rutas"] != null)
            {
                initTaula((List<Ruta>)Session["rutas"]);
            }


        }
        private void loadTreeView(IEnumerable<Categoria> list, TreeNode parentNode)
        {
            var nodes = list.Where(x => parentNode == null ? x.ParentId == 0 : x.ParentId == int.Parse(parentNode.Value));
            foreach (var node in nodes)
            {
                TreeNode newNode = new TreeNode(node.name, node.id.ToString());
                if (parentNode == null)
                {
                    tvwCategorias.Nodes.Add(newNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(newNode);
                }
                loadTreeView(list, newNode);
            }
        }
        protected void tvHoldingDetail_SelectedNodeChanged(object sender, EventArgs e)
        {
            List<Ruta> rutas = bd.GetRutas(tvwCategorias.SelectedNode.Text);
            Session["rutas"] = rutas;
            initTaula((List<Ruta>)Session["rutas"]);
        }
        private void initTaula(List<Ruta> ruta)
        {
            gvRutas.Controls.Clear();

            foreach (Ruta p in ruta)
            {
                TableRow row = new TableRow();

                TableCell cellId = new TableCell();
                TableCell cellDesti = new TableCell();
                TableCell cellOrigen = new TableCell();
                TableCell cellParentId = new TableCell();
                TableCell cellButton_delete = new TableCell();
                TableCell cellButton_update = new TableCell();

                cellId.Text = p.id.ToString();
                cellDesti.Text = p.Destino;
                cellOrigen.Text = p.Origen;
                cellParentId.Text = p.idCategoria.ToString();

                Button b_delete = new Button();
                b_delete.UseSubmitBehavior = false;
                b_delete.CausesValidation = false;
                b_delete.ID =  p.id.ToString();
                b_delete.Text = "Elimina";
                b_delete.Click += B_Click;
                cellButton_delete.Controls.Add(b_delete);

                Button b_update = new Button();
                b_update.CausesValidation = false;
                b_update.ID = "U" + p.id.ToString();
                b_update.Text = "Update";
                b_update.OnClientClick +=new EventHandler( B_update_Click);
                cellButton_update.Controls.Add(b_update);

                row.Controls.Add(cellId);
                row.Controls.Add(cellDesti);
                row.Controls.Add(cellOrigen);
                row.Controls.Add(cellParentId);
                row.Controls.Add(cellButton_delete);
                row.Controls.Add(cellButton_update);

                gvRutas.Controls.Add(row);
            }
        }
        private void B_update_Click(object sender, EventArgs e)
        {
            Response.Redirect("loginForm.aspx");
            Button button = (Button)sender;
            string rutaID = button.ID;
        }
        private void B_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int rutaID = Convert.ToInt32(button.ID);
            bd.DeleteRuta(rutaID);
            List<Ruta> rutas = bd.GetRutas(tvwCategorias.SelectedNode.Text);
            Session["rutas"] = rutas;
            initTaula((List <Ruta>)Session["rutas"]);

        }
        public void dropdownCat()
        {
            MySqlConnection con = new MySqlConnection(constr);
            con.Open();

            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM categorias where parentId IS NOT NULL");

            cmd1.Connection = con;

            MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = cmd1;

            using (DataTable dt = new DataTable())
            {
                sda.Fill(dt);
                dropCategorias.DataSource = dt;
                dropCategorias.DataBind();
            }
            con.Close();
            con.Open();
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            String dest = Destino.Text;
            String org = Origen.Text;
            int categoria = Convert.ToInt32(dropCategorias.SelectedValue);
            bd.NewRuta(categoria,dest, org);
            foreach (Control b in newruta.Controls) 
            {
                TextBox c;
                if (b is TextBox)
                {
                    c = b as TextBox;
                    if (c != null)
                    {
                        c.Text = String.Empty;
                    }
                }
            }
        }
    }
}