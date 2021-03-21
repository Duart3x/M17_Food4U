using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.admin
{
    public partial class estadopedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                Response.Redirect("~/index.aspx");


            if (Request["pedido"] == null || !int.TryParse(Request["pedido"].ToString(), out int id_pedido))
                Response.Redirect("~/index.aspx");

            if (IsPostBack)
                return;
        }

        protected void btn_confimar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_pedido = -1;
                if (Request["pedido"] == null || !int.TryParse(Request["pedido"].ToString(), out id_pedido))
                    Response.Redirect("~/index.aspx");

                int value = int.Parse(radius_list.SelectedValue.ToString());

                if(value == 1 || value == 2 || value == 3 || value == 4 || value == 5)
                {
                    Orders.ToggleState(id_pedido,value);
                    Response.Redirect("~/admin/pedidos.aspx");
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/admin/pedidos.aspx");
            }
        }
    }
}