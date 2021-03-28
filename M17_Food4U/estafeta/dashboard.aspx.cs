using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.estafeta
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "2")
                Response.Redirect("~/index.aspx");

            if (IsPostBack)
                return;
            int id_estafeta = int.Parse(Session["id_user"].ToString());
            Estatisticas estatisticas = new Estatisticas();
            
            // lb_saldo.InnerText = estatisticas.GetTotalAngariado(id_estafeta).ToString("C2");
            lb_pedidosfinalizados.InnerText = estatisticas.getTotalPedidosFinalizados(id_estafeta).ToString();

        }
    }
}