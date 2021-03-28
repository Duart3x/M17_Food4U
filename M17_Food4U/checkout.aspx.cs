using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U
{
    public partial class checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null)
                Response.Redirect("~/login.aspx");

            if (IsPostBack)
                return;

            int id_user = int.Parse(Session["id_user"].ToString());

            DataTable dados = ShoppingCart.GetCarrinhoFromUser(id_user);

            //id, Menu, preco, descricao, MenuId, Quantidade
            if(dados == null || dados.Rows.Count <= 0)
            {
                lb_semprodutos.Visible = true;
                btn_confirmar.Visible = false;
            }
            else
            {
                lb_total.Visible = true;
            }
            double total = 0;
            foreach (DataRow row in dados.Rows)
            {
                string menu = row["Menu"].ToString();
                string descricao = row["descricao"].ToString();
                int id_menu = int.Parse(row["MenuId"].ToString());
                int quantidade = int.Parse(row["Quantidade"].ToString());
                double preco = double.Parse(row["preco"].ToString());

                int aleatorio = new Random().Next(999999);

                string html = $@"
                <div class='d-flex w-100'>
                    <a href='menu.aspx?id={id_menu}' >
                        <img src = 'Public/images/menus/{id_menu}.jpg?{aleatorio}' class='img-thumbnail' style='max-height: 200px;' alt='{menu}' />
                    </a>
                    <div class='ml-4' style='width: 500px;'>
                        <div>
                            <span class='mb-0'><b>{menu}</b></span>
                            <br />
                            <span class='text-muted'>{descricao}</span>
                        </div>
                    </div>
                    <div style = 'display: flex; width: 100%; justify-content: flex-end; align-items: center' >
                        <i class='far fa-trash-alt' style='cursor: pointer;' onclick='AlterarQuantidade({id_user},{id_menu},-5)' data-toggle='tooltip' data-placement='top' title='Remover'></i>
                        <div class='ml-5 mr-5'>
                            <i class='fal fa-minus-square mr-2' onclick='AlterarQuantidade({id_user},{id_menu},-1)' data-toggle='tooltip'  data-placement='top' title='Remover 1' style='cursor: pointer;'></i>
                            <span class='p-2 border'>{quantidade}</span>
                            <i class='fal fa-plus-square ml-2' onclick='AlterarQuantidade({id_user},{id_menu},1)' data-toggle='tooltip' data-placement='top' title='Adicionar 1' style='cursor: pointer;'></i>
                        </div>
                        <b>{(preco *  quantidade).ToString("C2")}</b>
                    </div>
                </div>
                <hr/>";
                total += preco * quantidade;
                div_produtos.InnerHtml += html;
            }
            lb_precototal.InnerHtml = total.ToString("C2");

            User user = new User(id_user);
            List<Address> moradas = user.getMoradas();

            if(moradas.Count <= 0)
            {
                btn_adicionarmoradas.Visible = true;
            }

            foreach (Address address in moradas)
            {
                string html = $@"
                        <div class='morada_hover d-flex border p-3 justify-content-between mb-4' onclick='selectmorada({address.id})' style='cursor: pointer; '>
                            <div class='d-flex flex-column'>
                                <span><b>{address.address}</b></span>
                                <span>{address.city}</span>
                                <span>{address.cp}</span>
                            </div>
                            <div class='d-flex align-items-center'>
                                <i class='far fa-check-circle text-danger' id='uncheck_morada_{address.id}'  style='font-size: 40px;'></i>
                                <i class='fas fa-check-circle text-danger' id='check_morada_{address.id}' hidden style = 'font-size: 40px;'></i>
                            </div>
                        </div> ";
                div_moradas.InnerHtml += html;
            }

        }

        protected void btn_confirmar_Click(object sender, EventArgs e)
        {
            int morada = -1;

            if (Request.Cookies["morada"] == null || !int.TryParse(Request.Cookies["morada"].Value.ToString(), out morada))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", "ShowNotification('Erro','Selecione uma morada válida', 'error')", true);
                return;
            }
            

            Request.Cookies["morada"].Expires = DateTime.Now.AddDays(-1);
            int id_user = int.Parse(Session["id_user"].ToString());

            Models.User user = new User(id_user);

            if (!user.IsAddressFromUser(morada))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", "ShowNotification('Erro','Selecione uma morada válida', 'error')", true);
                return;
            }

            double usersaldo = user.getSaldo();

            DataTable dados = ShoppingCart.GetCarrinhoFromUser(id_user);


            if (dados == null || dados.Rows.Count <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", "ShowNotification('Erro','Sem produtos no carrinho', 'error')", true);
                return;
            }

            double totalcarrinho = ShoppingCart.GetCarrinhoValue(id_user);

            if (usersaldo < totalcarrinho)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", "ShowNotification('Erro','Saldo insuficiente', 'error')", true);
                return;
            }
            Pagamento pagamento = new Pagamento();
            pagamento.user = id_user;

            pagamento.valor = totalcarrinho;
            pagamento.saldo = usersaldo;
            
            int id_order = Orders.CriarPedido(id_user, morada);
            
            pagamento.order = id_order;
            pagamento.PagarOrder();

            OrdersMenus ordersMenus = new OrdersMenus();

            foreach (DataRow row in dados.Rows)
            {
                int id_menu = int.Parse(row["MenuId"].ToString());
                int quantidade = int.Parse(row["Quantidade"].ToString());

                ordersMenus.menu = id_menu;
                ordersMenus.quantity = quantidade;

                ordersMenus.CriarOrderMenu(id_order);
            }

            ShoppingCart.DeleteCarrinho(id_user);

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", "ShowCartNotification('Sucesso','Compra efetuada com sucesso', 'success')", true);
        }
    }
}