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
    public partial class restaurante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Request["id"] == null || Request["id"].Length < 1 || Request["id"].ToString() == String.Empty || int.Parse(Request["id"].ToString()) < 0)
                {
                    Response.Redirect("~/index.aspx");
                    return;
                }
                else
                {
                    int id_restaurante = int.Parse(Request["id"].ToString());
                    if (!Restaurant.IsRestaurantEnabled(id_restaurante))
                    {
                        Response.Redirect("~/index.aspx");
                        return;
                    }
                }

            }
            catch (Exception erro)
            {
                Response.Redirect("~/index.aspx");
                return;
            }

            if (Session["perfil"] == null)
            {
                div_inputcomentario.Visible = false;
                div_informarregisto.Visible = true;
            }

            if (IsPostBack)
                return;

            LoadRestauranteInfo();

            LoadComments();

        }

        private void LoadComments()
        {
            div_comentarios_content.InnerHtml = "";
            int id_restaurante = int.Parse(Request["id"].ToString());

            DataTable dados = RestaurantComment.LoadComments(id_restaurante);

            // comment
            // stars
            // name
            // CreateDate

            foreach (DataRow item in dados.Rows)
            {
                string comentario = item["comment"].ToString();
                int estrelas = int.Parse(item["stars"].ToString());
                string utilizador = item["name"].ToString();
                DateTime data = DateTime.Parse(item["CreateDate"].ToString());

                string estrelasHtml = "";
                for (int i = 1; i < 6; i++)
                {
                    if (i <= estrelas)
                        estrelasHtml += "<i class='fas fa-star text-warning'></i>";
                    else
                        estrelasHtml += "<i class='far fa-star text-warning'></i>";
                }

                div_comentarios_content.InnerHtml += $@"
                <div class='comentario border p-3 bg-white mt-3'>
                    <div class='d-flex align-items-center justify-content-between'>
                        <span style= ""font-family: 'Roboto', sans-serif !important; font-weight: bold; font-size: 18px;""> {utilizador}</span>
                        <span>{estrelasHtml}</span>
                    </div>
                    <div>
                        <small class='text-muted'>{data}</small>
                    </div>
                    <div class='mt-3'>
                        <span>{comentario}</span>
                    </div>
                </div>";
            }
        }

        private void LoadRestauranteInfo()
        {
            int id_restaurante = int.Parse(Request["id"].ToString());

            Restaurant restaurantobj = Restaurant.GetRestaurante(id_restaurante);

            int aleatorio = new Random().Next(999999);

            menu_foto.Src = $"~/Public/images/restaurants/{id_restaurante}.jpg?{aleatorio}";
            menu_foto.Alt = $"{restaurantobj.name}";

            restaurante_nome.InnerText = restaurantobj.name;

            DataTable dados = Restaurant.ListarMenusRestaurante(id_restaurante);

            if(dados.Rows.Count > 0)
            {
                foreach (DataRow item in dados.Rows)
                {
                    int id_menu = int.Parse(item["id"].ToString());
                    string title = item["title"].ToString();
                    string description = item["description"].ToString();
                    double price = double.Parse(item["price"].ToString());
                    int estrelas = int.Parse(item["stars"].ToString());
                    bool stock = bool.Parse(item["stock"].ToString());

                    string strstock = stock ? "" : "Fora de stock";

                    string estrelasHtml = "";
                    for (int i = 1; i < 6; i++)
                    {
                        if (i <= estrelas)
                            estrelasHtml += "<i class='fas fa-star text-warning'></i>";
                        else
                            estrelasHtml += "<i class='far fa-star text-warning'></i>";
                    }

                    aleatorio = new Random().Next(999999);

                    menu_grid_menus.InnerHtml += $@"
<div class='card mb-3 product_card' onclick='showMenu({id_menu})'>
                    <div class='row no-gutters align-items-center'>
                        <div class='col-md-4'>
                            <img src='Public/images/menus/{id_menu}.jpg?{aleatorio}' class='w-100' alt='{title}'>
                        </div>
                        <div class='col-md-8'>
                            <div class='card-body'>
                                <div class='d-flex justify-content-between align-items-center mb-3'>
                                    <h5 class='card-title mb-0'>{title}</h5>
                                    <div>
                                        {estrelasHtml}
                                    </div>
                                </div>
                                <p class='card-text'>{description}</p>
                                <div class='d-flex align-items-center justify-content-between'>
                                    <span class='card-text'><small><b>Preço: </b>{price.ToString("C2")}</small></span>
                                    <span class='card-text'><small class='text-danger'>{strstock}</small></span>
                                  </div>
                              </div>
                          </div>
                      </div>
                  </div>";
                }
            }
            else
            {
                menu_grid_menus.InnerHtml = "Sem menus";
            }

            

        }

        protected void btn_comentar_Click(object sender, EventArgs e)
        {
            try
            {
                string comentario = txt_comentario.Text.Trim();

                if (comentario.Length <= 3)
                    throw new Exception("Comentário inválido");
                if (Request.Cookies["estrelas"] == null)
                    throw new Exception("Número de estrelas inválido");

                if (int.Parse(Request.Cookies["estrelas"].Value) <= 0 || int.Parse(Request.Cookies["estrelas"].Value) > 5)
                    throw new Exception("Número de estrelas inválido");

                int id_restaurante = int.Parse(Request["id"].ToString());
                int id_user = int.Parse(Session["id_user"].ToString());
                if (Session["id_user"] == null)
                    throw new Exception("Não estás logado.");

                if (Restaurant.UserOwnsRestaurant(id_restaurante,id_user))
                    throw new Exception("Não podes comentar no teu próprio restaurante");

                int estrelas = int.Parse(Request.Cookies["estrelas"].Value);

                Request.Cookies["estrelas"].Expires = DateTime.Now.AddDays(-1);

                RestaurantComment restaurantComment = new RestaurantComment();
                restaurantComment.user = id_user;
                restaurantComment.restaurant = id_restaurante;
                restaurantComment.comment = comentario;
                restaurantComment.stars = estrelas;

                restaurantComment.Adicionar();

                lb_erro_comentario.Visible = false;
                txt_comentario.Text = "";
                LoadComments();
            }
            catch (Exception erro)
            {
                /* lb_erro_comentario.Text = erro.Message;
                 lb_erro_comentario.Visible = true;*/

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", "ShowNotification('Erro','"+ erro.Message + "', 'error',4000)", true);
                return;
            }
        }
    }
}