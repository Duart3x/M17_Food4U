using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.admin
{
    public partial class historicopedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                Response.Redirect("~/index.aspx");

            int id_user = 0;

            if (Request["user"] == null || !int.TryParse(Request["user"].ToString(),out id_user))
                Response.Redirect("~/index.aspx");

            if (IsPostBack)
                return;
             
             

            DataTable dados = Orders.GetOrdersFromUser(id_user);
            
            foreach (DataRow row in dados.Rows)
            {
                int id_order = int.Parse(row["id"].ToString());
                int state = int.Parse(row["state"].ToString());
                DateTime createDate = DateTime.Parse(row["createDate"].ToString());

                TreeNode root = new TreeNode("Pedido Num." + id_order);
                tree_pedidos.Nodes.Add(root);

                DataTable dados2 = OrdersMenus.getOrdersMenusFromOrder(id_order);
                foreach (DataRow row2 in dados2.Rows)
                {
                    string title = row2["title"].ToString();
                    int quantidade = int.Parse(row2["quantity"].ToString());

                    root.ChildNodes.Add(new TreeNode(title + " : " + quantidade));
                }
            }

            

        }
    }
}