using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Xml;

namespace PracticaASP.NET
{
    public partial class user : System.Web.UI.Page
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
            List<Categoria> categories = bd.getCategorias();
            if (!Page.IsPostBack)
            {
                loadTreeView(categories, null);
            }
            if (Session["rutas"] != null)
            {
                initTaula((List<Ruta>)Session["rutas"]);
            }

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
                b_delete.ID = p.id.ToString();
                b_delete.Text = "Comentaris";
                b_delete.Click += B_Click;
                cellButton_delete.Controls.Add(b_delete);

                

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
            Ruta ruta = bd.getRuta(rutaID);
            Session["ruta"] = ruta;
            Response.Redirect("ruta.aspx");
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
            List<Ruta> rutas = bd.getRutas(tvwCategorias.SelectedNode.Text);
            Session["rutas"] = rutas;
            initTaula((List<Ruta>)Session["rutas"]);
        }
    }
}