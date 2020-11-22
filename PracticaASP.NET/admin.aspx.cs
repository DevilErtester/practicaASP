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
                
            if (!Page.IsPostBack)
            {
                loadTreeView();
            }
            
        }
        private String xmlPath = "carrecs.xml";
        private void loadTreeView()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(MapPath(xmlPath));

            tvwCategorias.Nodes.Add(new TreeNode(xml.DocumentElement.Name));

            TreeNode root = new TreeNode();
            root = tvwCategorias.Nodes[0];
            new_node(root, xml.DocumentElement);
        }

        private void new_node(TreeNode treeNodeRoot, XmlNode xmlNodeRoot)
        {
            if (xmlNodeRoot.HasChildNodes)
            {
                foreach (XmlNode node in xmlNodeRoot.ChildNodes)
                {
                    TreeNode tN = new TreeNode(node.Name);
                    treeNodeRoot.Text = xmlNodeRoot.Attributes.GetNamedItem("name").InnerText;
                    treeNodeRoot.ChildNodes.Add(tN);
                    new_node(tN, node);
                }
            }
            else
            {
                treeNodeRoot.Text = xmlNodeRoot.Attributes.GetNamedItem("name").InnerText;
            }
        }

    }
}
//Si la categoria que le paso tiene hijos 