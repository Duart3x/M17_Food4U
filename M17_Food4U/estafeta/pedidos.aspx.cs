using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.estafeta
{
    public partial class pedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "2")
                Response.Redirect("~/index.aspx");

            dgv_pedidos.RowCommand += Dgv_pedidos_RowCommand;

            if (IsPostBack)
                return;

            
            AtualizarGrid();
            AtualizarPedidoInfo();
        }

        private void AtualizarPedidoInfo()
        {
            int id_estafeta = int.Parse( Session["id_user"].ToString());
            DataTable dados = Orders.GetEstafetaOrder(id_estafeta);

            if (dados == null)
            {
                div_pedidoaceite.Visible = false;
                return;
            }
                

            int id_order = int.Parse(dados.Rows[0]["id"].ToString());
            int estadonum = int.Parse(dados.Rows[0]["estadonum"].ToString());

            string cliente = dados.Rows[0]["Cliente"].ToString();
            string Estado = dados.Rows[0]["Estado"].ToString();
            string Cidade = dados.Rows[0]["Cidade"].ToString();
            string CP = dados.Rows[0]["CP"].ToString();
            string Morada = dados.Rows[0]["Morada"].ToString();
            DateTime Data = DateTime.Parse(dados.Rows[0]["Data"].ToString());

            dp_estados.SelectedValue = estadonum.ToString();
            lb_cliente.InnerText = cliente;
            lb_estado.InnerText = Estado;
            lb_city.InnerText = Cidade;
            lb_cp.InnerText = CP;
            lb_morada.InnerText = Morada;
            lb_data.InnerText = Data.ToString();
            div_pedidoaceite.Visible = true;

            DataTable dados2 = OrdersMenus.getOrdersMenusFromOrder(id_order);
            dgv_pedidoinfo.Columns.Clear();
            dgv_pedidoinfo.DataSource = null;
            dgv_pedidoinfo.DataBind();

            dgv_pedidoinfo.DataSource = dados2;
            dgv_pedidoinfo.AutoGenerateColumns = false;

            BoundField bfID = new BoundField();
            bfID.HeaderText = "ID";
            bfID.DataField = "id";
            dgv_pedidoinfo.Columns.Add(bfID);

            BoundField bfMenu = new BoundField();
            bfMenu.HeaderText = "Menu";
            bfMenu.DataField = "title";
            dgv_pedidoinfo.Columns.Add(bfMenu);

            BoundField bfRestaurante = new BoundField();
            bfRestaurante.HeaderText = "Restaurante";
            bfRestaurante.DataField = "nome_restaurante";
            dgv_pedidoinfo.Columns.Add(bfRestaurante);

            BoundField bfMoradaRestaurante = new BoundField();
            bfMoradaRestaurante.HeaderText = "Morada Restaurante";
            bfMoradaRestaurante.DataField = "morada_restaurante";
            dgv_pedidoinfo.Columns.Add(bfMoradaRestaurante);

            BoundField bfCpRestaurante = new BoundField();
            bfCpRestaurante.HeaderText = "CP Restaurante";
            bfCpRestaurante.DataField = "cp_restaurante";
            dgv_pedidoinfo.Columns.Add(bfCpRestaurante);

            BoundField bfCidade = new BoundField();
            bfCidade.HeaderText = "Cidade Restaurante";
            bfCidade.DataField = "cidade_restaurante";
            dgv_pedidoinfo.Columns.Add(bfCidade);

            BoundField bfQuantidade = new BoundField();
            bfQuantidade.HeaderText = "Quantidade";
            bfQuantidade.DataField = "quantity";
            dgv_pedidoinfo.Columns.Add(bfQuantidade);

            BoundField bfEstado = new BoundField();
            bfEstado.HeaderText = "Estado";
            bfEstado.DataField = "estado";
            dgv_pedidoinfo.Columns.Add(bfEstado);

            dgv_pedidoinfo.DataBind();
        }

        private void Dgv_pedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page") return;

            
            int linha = int.Parse(e.CommandArgument as string);
            
            int idPedido = int.Parse(dgv_pedidos.Rows[linha].Cells[0].Text);

            if (e.CommandName == "btn_aceitar")
            {
                int id_estafeta = int.Parse(Session["id_user"].ToString());
                Orders.AceitarOrder(id_estafeta,idPedido);
                AtualizarGrid();
                AtualizarPedidoInfo();

            }
        }

        private void AtualizarGrid()
        {
            dgv_pedidos.Columns.Clear();
            dgv_pedidos.DataSource = null;
            dgv_pedidos.DataBind();

            DataTable dados = Orders.GetOrdersEmProcessamentoSemEstafeta();
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

            BoundField bfEstado = new BoundField();
            bfEstado.HeaderText = "Estado";
            bfEstado.DataField = "Estado";
            dgv_pedidos.Columns.Add(bfEstado);

            BoundField bfCidade = new BoundField();
            bfCidade.HeaderText = "Cidade";
            bfCidade.DataField = "Cidade";
            dgv_pedidos.Columns.Add(bfCidade);

            BoundField bfCP = new BoundField();
            bfCP.HeaderText = "CP";
            bfCP.DataField = "CP";
            dgv_pedidos.Columns.Add(bfCP);

            BoundField bfMorada = new BoundField();
            bfMorada.HeaderText = "Morada";
            bfMorada.DataField = "Morada";
            dgv_pedidos.Columns.Add(bfMorada);

            BoundField bfData = new BoundField();
            bfData.HeaderText = "Data";
            bfData.DataField = "Data";
            dgv_pedidos.Columns.Add(bfData);

            ButtonField btn = new ButtonField();
            btn.Text = "Aceitar";
            btn.HeaderText = "Aceitar";
            btn.ControlStyle.CssClass = "btn btn-success";
            btn.ButtonType = ButtonType.Button;
            btn.CommandName = "btn_aceitar";
            dgv_pedidos.Columns.Add(btn);

            dgv_pedidos.DataBind();
        }
        protected void btn_mudarestado_Click(object sender, EventArgs e)
        {
            try
            {
                int estado = int.Parse(dp_estados.SelectedValue.ToString());
                int id_estafeta = int.Parse(Session["id_user"].ToString());
                DataTable dados = Orders.GetEstafetaOrder(id_estafeta);

                if (dados == null)
                    return;

                int id_order = int.Parse(dados.Rows[0]["id"].ToString());
                if(estado == 3 || estado == 4)
                {
                    if (Orders.IsOrderMenusCompleted(id_order))
                        Orders.ToggleState(id_order, estado);
                }
                else
                    Orders.ToggleState(id_order, estado);

                
                AtualizarPedidoInfo();
                AtualizarGrid();
            }
            catch (Exception)
            {
                
                AtualizarPedidoInfo();
                AtualizarGrid();
            }
            

        }
        protected void dgv_pedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void dgv_pedidoinfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        
    }
}