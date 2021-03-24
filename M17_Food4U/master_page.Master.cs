using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U
{
    public partial class master_page : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            AtualizaCarrinho();
        }

        private void AtualizaCarrinho()
        {
            

            list_products.InnerHtml = "";
            if (Session["id_user"] != null)
            {
                
                int id_user = int.Parse(Session["id_user"].ToString());

                Models.User user = new User(id_user);
                lb_saldouser.Text = user.getSaldo().ToString("C2");
                span_saldo.Visible = true;



                DataTable dados = ShoppingCart.GetCarrinhoFromUser(id_user);
                //id, Menu, MenuId, Quantidade
                int total_menus = 0;
                double total_preco = 0;

                foreach (DataRow row in dados.Rows)
                {
                    string menu = row["Menu"].ToString();
                    string descricao = row["descricao"].ToString();
                    int id_menu = int.Parse(row["MenuId"].ToString());
                    int quantidade = int.Parse(row["Quantidade"].ToString());
                    double preco = double.Parse(row["preco"].ToString());

                    int aleatorio = new Random().Next(999999);

                    list_products.InnerHtml += $@"
                    <div class='product'>
                        <a href='menu.aspx?id={id_menu}'>                            
                            <img src='/Public/images/menus/{id_menu}.jpg?{aleatorio}' class='img-thumbnail' style='max-height: 136px;' alt='{menu}' />
                        </a>
                        <div class='product_details ml-4'>
                            <div>
                                <span class='mb-0'><b>{menu}</b></span>
                                <br />
                                <span class='text-muted'>{descricao}</span>
                            </div>
                            <div class='d-flex justify-content-between'>
                                <div>
                                    <span>Qtd: <span>{quantidade}</span></span>
                                    <h4 class='text-danger'>{preco.ToString("C2")}</h4>
                                </div>
                                <span class='shopcard_removermenu mb-3 text-muted' onclick='removerproduct({id_menu},{id_user})'>X Remover</span>
                            </div>
                        </div>
                    </div>
                    ";
                    total_menus += quantidade;
                    total_preco += (quantidade * preco);
                }

                dot_count.InnerText = total_menus.ToString();
                lb_totalitems.InnerText = total_menus.ToString();
                lb_shopcard_total.InnerText = total_preco.ToString("C2");

            }
            else
            {
                if(Request.Cookies["carrinho"] == null)
                {
                    lb_shopcard_total.InnerText = 0.ToString("C2");
                    lb_totalitems.InnerText = 0.ToString();
                    dot_count.InnerText = 0.ToString();
                    return;
                }
                var lista = Request.Cookies["carrinho"].Values;
                int total_menus = 0;
                double total_preco = 0;
                Models.Menu menuligacao = new Models.Menu();
                foreach (var item in lista.AllKeys)
                {
                    int id_menu = int.Parse(item);
                    int quantidade = int.Parse(lista[item]);

                    Models.Menu menuobj = menuligacao.GetMenuInter(id_menu);

                    int aleatorio = new Random().Next(999999);
                    list_products.InnerHtml += $@"
                    <div class='product'>
                        <a href='menu.aspx?id={id_menu}'>                            
                            <img src='/Public/images/menus/{id_menu}.jpg?{aleatorio}' class='img-thumbnail' style='max-height: 136px;' alt='{menuobj.title}' />
                        </a>
                        <div class='product_details ml-4'>
                            <div>
                                <span class='mb-0'><b>{menuobj.title}</b></span>
                                <br />
                                <span class='text-muted'>{menuobj.description}</span>
                            </div>
                            <div class='d-flex justify-content-between'>
                                <div>
                                    <span>Qtd: <span>{quantidade}</span></span>
                                    <h4 class='text-danger'>{menuobj.price.ToString("C2")}</h4>
                                </div>
                                <span class='shopcard_removermenu mb-3 text-muted' onclick='removerproduct({id_menu},null)'>X Remover</span>
                            </div>
                        </div>
                    </div>
                    ";
                    total_menus += quantidade;
                    total_preco += (quantidade * menuobj.price);

                }
                dot_count.InnerText = total_menus.ToString();
                lb_totalitems.InnerText = total_menus.ToString();
                lb_shopcard_total.InnerText = total_preco.ToString("C2");
                

            }
        }

        protected void btn_user_Click(object sender, EventArgs e)
        {
            if(Session["perfil"] != null)
            {
                Response.Redirect("/User/perfil.aspx");
            }
            else
            {
                Response.Redirect("/login.aspx");
            }

        }
    }
}