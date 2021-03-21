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
    public partial class detalhespedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                Response.Redirect("~/index.aspx");
            
            int id_pedido = -1;

            if (Request["pedido"] == null || !int.TryParse(Request["pedido"].ToString(), out id_pedido))
                Response.Redirect("~/index.aspx");

            if (IsPostBack)
                return;
            DataTable dados = OrdersMenus.getOrdersMenusFromOrder(id_pedido);
            dgv_pedidos.Columns.Clear();
            dgv_pedidos.DataSource = null;
            dgv_pedidos.DataBind();

            dgv_pedidos.DataSource = dados;
            dgv_pedidos.AutoGenerateColumns = false;

            BoundField bfID = new BoundField();
            bfID.HeaderText = "ID";
            bfID.DataField = "id";
            dgv_pedidos.Columns.Add(bfID);

            BoundField bfMenu = new BoundField();
            bfMenu.HeaderText = "Menu";
            bfMenu.DataField = "title";
            dgv_pedidos.Columns.Add(bfMenu);

            BoundField bfQuantidade = new BoundField();
            bfQuantidade.HeaderText = "Quantidade";
            bfQuantidade.DataField = "quantity";
            dgv_pedidos.Columns.Add(bfQuantidade);

            BoundField bfEstado = new BoundField();
            bfEstado.HeaderText = "Estado";
            bfEstado.DataField = "estado";
            dgv_pedidos.Columns.Add(bfEstado);

            dgv_pedidos.DataBind();
        }
    }
}