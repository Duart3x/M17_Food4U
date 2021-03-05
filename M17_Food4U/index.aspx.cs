using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace M17_Food4U
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            if (Request.QueryString.Get("p") != null)
            {
                // Produto no link
                AtualizarComidas();

            }
            else if(Request.QueryString.Get("r") != null)
            {
                // Restaurante no link
                AtualizarRestaurantes();
            }
            else
            {
                AtualizarCards();
            }
        }
        private void AtualizarCards()
        {
            AtualizarRestaurantes();

            AtualizarComidas();
        }
        private void AtualizarRestaurantes()
        {
            DataTable restaurantes = Restaurant.ListarRestaurantesDiponiveis();
            string cards = "";
            foreach (DataRow row in restaurantes.Rows)
            {
                int id = int.Parse(row["id"].ToString());
                string nome = row["name"].ToString();
                string city = row["city"].ToString();

                string morada = row["address"].ToString();
                int aleatorio = new Random().Next(999999);
                cards += $@"<div class='card' style='width: 18rem; '>
                               <img src = '/Public/images/restaurants/{id}.jpg?{aleatorio}' class='card-img-top' alt='{nome}'>
                              <div class='card-body'>
                                <h5 class='card-title'>{nome}</h5>
                                <p class='card-text'>{city} - {morada}</p>
                              </div>
                                <div class='card-footer d-flex justify-content-between align-items-center'>
                                    <span>Ver menus</span>
                                    <span data-toggle='tooltip' data-placement='top' title='Ver Menus' class='add-cart'><i class='far fa-utensils'></i></span>
                                </div>
                            </div>";

            }

            menu_grid_restaurants.InnerHtml = cards;
            if (menu_grid_restaurants.InnerHtml.Length > 0)
                menu_grid_restaurants.Visible = true;
            else
            {
                menu_grid_restaurants.Visible = false;
                template_restaurants.InnerHtml = " <h2>Restaurantes Disponíveis</h2>Sem Resultados";
            }
                
        }

        private void AtualizarComidas()
        {
            DataTable menus = Menu.ListarMenusDiponíveis();
            string cards = "";
            foreach (DataRow row in menus.Rows)
            {
                int id = int.Parse(row["id"].ToString());
                string title = row["title"].ToString();
                string description = row["description"].ToString();
                double price = double.Parse(row["price"].ToString());
                int stars = int.Parse(row["stars"].ToString());
                bool hasStock = bool.Parse(row["stock"].ToString());
                string morada = row["restaurant"].ToString();

                int aleatorio = new Random().Next(999999);

                cards += $@"<div class='card' style='width: 18rem; '>
                               <img src = '/Public/images/menus/{id}.jpg?{aleatorio}' class='card-img-top' alt='{title}'>
                              <div class='card-body'>
                                <h5 class='card-title'>{title}</h5>
                                <p class='card-text'>{description}</p>
                              </div>
                                <div class='card-footer d-flex justify-content-between align-items-center'>
                                    {(hasStock ? $@"<span>{price.ToString("C2")}</span>
                                    <span data-toggle='tooltip' data-placement='top' title='Adicionar ao carrinho' class='add-cart'><i class='fad fa-shopping-cart mr-2' ></i><i class='far fa-plus-square'></i></span>" : "<span>Artigo fora de stock</span>")}
                                </div>
                            </div>";

            }

            menu_grid_menus.InnerHtml = cards;
            if (menu_grid_menus.InnerHtml.Length > 0)
                menu_grid_menus.Visible = true;
            else
            {
                menu_grid_menus.Visible = false;
                template_menus.InnerHtml = "<h2>Menus Disponíveis</h2>Sem Resultados";
            }
        }
    }
}