using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace PracticaASP.NET
{
    public partial class admin : System.Web.UI.Page
    {
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
            List<Categoria> categories = bd.getCategorias();
            if (!Page.IsPostBack)
            {
                loadTreeView(categories,null);
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
            Response.Write(tvwCategorias.SelectedNode.Text);
            List<Ruta> rutas = bd.getRutas(tvwCategorias.SelectedNode.Text);


        }
    }
} 