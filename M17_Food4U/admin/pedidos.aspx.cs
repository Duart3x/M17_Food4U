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
    public partial class pedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            dgv_pedidos.Columns.Clear();
            dgv_pedidos.DataSource = null;
            dgv_pedidos.DataBind();

            DataTable dados = Orders.GetAllOrders();
            dgv_pedidos.DataSource = dados;
            dgv_pedidos.AutoGenerateColumns = false;

            BoundField bfID = new BoundField();
            bfID.HeaderText = "ID";
            bfID.DataField = "id";
            dgv_pedidos.Columns.Add(bfID);

            BoundField bfCliente = new BoundField();
            bfCliente.HeaderText = "Cliente";
            bfCliente.DataField = "Cliente";
            dgv_pedidos.Columns.Add(bfCliente);

            BoundField bfEstafeta = new BoundField();
            bfEstafeta.HeaderText = "Estafeta";
            bfEstafeta.DataField = "Estafeta";
            dgv_pedidos.Columns.Add(bfEstafeta);

            BoundField bfEstado = new BoundField();
            bfEstado.HeaderText = "Estado";
            bfEstado.DataField = "Estado";
            dgv_pedidos.Columns.Add(bfEstado);

            BoundField bfData = new BoundField();
            bfData.HeaderText = "Data";
            bfData.DataField = "Data";
            dgv_pedidos.Columns.Add(bfData);

            DataColumn dcOpcoes = new DataColumn();
            dcOpcoes.ColumnName = "Opções";
            dcOpcoes.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcOpcoes);

            HyperLinkField hlOpcoes = new HyperLinkField();
            hlOpcoes.HeaderText = "Alterar Estado";
            hlOpcoes.DataTextField = "Opções";
            hlOpcoes.Text = "Alterar Estado";
            hlOpcoes.DataNavigateUrlFormatString = "~/admin/estadopedido.aspx?pedido={0}";
            hlOpcoes.DataNavigateUrlFields = new string[] { "id" };
            dgv_pedidos.Columns.Add(hlOpcoes);

            DataColumn dcDetalhes2 = new DataColumn();
            dcDetalhes2.ColumnName = "Detalhes";
            dcDetalhes2.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcDetalhes2);

            HyperLinkField hldetalhes2 = new HyperLinkField();
            hldetalhes2.HeaderText = "Detalhes";
            hldetalhes2.DataTextField = "Detalhes";
            hldetalhes2.Text = "Detalhes";
            hldetalhes2.DataNavigateUrlFormatString = "~/admin/detalhespedido.aspx?pedido={0}";
            hldetalhes2.DataNavigateUrlFields = new string[] { "id" };
            dgv_pedidos.Columns.Add(hldetalhes2);


            dgv_pedidos.DataBind();

        }

        protected void dgv_pedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}