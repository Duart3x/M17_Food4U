using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.admin
{
    public partial class dashboard : System.Web.UI.Page
    {
        NewsLetter news = new NewsLetter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            Estatisticas estatisticas = new Estatisticas();

            lb_saldo.InnerHtml = estatisticas.getSaldoPlataforma().ToString("C2");
            lb_userscount.InnerHtml = estatisticas.getTotalUsers().ToString();
            lb_normaluserscount.InnerHtml = estatisticas.getTotalNormalUsers().ToString();
            lb_estafetascount.InnerHtml = estatisticas.getTotalEstafetas().ToString();
            lb_admincount.InnerHtml = estatisticas.getTotalAdministradores().ToString();

            lb_donosrestaurantes.InnerHtml = estatisticas.getTotalDonosRestaurantes().ToString();
            lb_restaurantescount.InnerHtml = estatisticas.getTotalRestaurantes().ToString();

            lb_pedidoscount.InnerHtml = estatisticas.getTotalPedidos().ToString();
            lb_pedidosfinished.InnerHtml = estatisticas.getTotalPedidosFinalizados().ToString();

            DataTable MenuMaisPedido = estatisticas.getMenuMaisPedido();
            if (MenuMaisPedido != null)
                lb_menumaispedido.InnerHtml = MenuMaisPedido.Rows[0]["title"].ToString() + " - " + MenuMaisPedido.Rows[0]["restaurante"].ToString() + " - " + MenuMaisPedido.Rows[0]["NumPedidos"].ToString() + " Pedidos";

            DataTable MenuRating = estatisticas.getMenuMaisRating();
            if (MenuRating != null)
            {
                int estrelas = int.Parse(MenuRating.Rows[0]["stars"].ToString());

                string estrelasHtml = "";
                for (int i = 1; i < 6; i++)
                {
                    if (i <= estrelas)
                        estrelasHtml += "<i class='fas fa-star text-warning'></i>";
                    else
                        estrelasHtml += "<i class='far fa-star text-warning'></i>";
                }

                lb_menurating.InnerHtml = MenuRating.Rows[0]["title"].ToString() + " - " + MenuRating.Rows[0]["restaurante"].ToString() + " - " + estrelasHtml;
            }
            DataTable RestauranteRating = estatisticas.GetRestauranteMaisRating();
            if (RestauranteRating != null)
            {
                int estrelas = int.Parse(RestauranteRating.Rows[0]["NumStars"].ToString());

                string estrelasHtml = "";
                for (int i = 1; i < 6; i++)
                {
                    if (i <= estrelas)
                        estrelasHtml += "<i class='fas fa-star text-warning'></i>";
                    else
                        estrelasHtml += "<i class='far fa-star text-warning'></i>";
                }

                lb_restaurantrating.InnerHtml = RestauranteRating.Rows[0]["name"].ToString() + " - " + estrelasHtml;
            }

        }

        protected void btn_menusmaisvendidos_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string email = news.EnviarMenusMaisVendidos(Request.Url.Authority);
                div_emailenviado.InnerHtml = email;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", "ShowNotification('Emails enviados com sucesso','Visualisa o email enviado no fundo da página', 'success')", true);

            }
            catch (Exception erro)
            {

            }

        }
        protected void btn_menusmenosvendidos_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string email =  news.EnviarMenusMenosVendidos(Request.Url.Authority);
                div_emailenviado.InnerHtml = email;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", "ShowNotification('Emails enviados com sucesso','Visualisa o email enviado no fundo da página', 'success')", true);

            }
            catch (Exception erro)
            {

            }
        }
        protected void btn_carrinhocheio_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string email =  news.EnviarCarrinhoCheio(Request.Url.Authority);
                div_emailenviado.InnerHtml = email;

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", "ShowNotification('Emails enviados com sucesso','Visualisa o email enviado no fundo da página', 'success')", true);

            }
            catch (Exception erro)
            {

            }
        }
        protected void btn_saldoplataforma_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string email = news.EnviarClientesSaldo(Request.Url.Authority);
                div_emailenviado.InnerHtml = email;

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", "ShowNotification('Emails enviados com sucesso','Visualisa o email enviado no fundo da página', 'success')", true);

            }
            catch (Exception erro)
            {

            }
        }
    }
}