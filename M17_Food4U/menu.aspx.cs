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
    public partial class menu : System.Web.UI.Page
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

            LoadMenuInfo();

            LoadComments();

            
        }

        private void LoadComments()
        {
            div_comentarios_content.InnerHtml = "";
            int id_menu = int.Parse(Request["id"].ToString());

            DataTable dados = MenuComment.LoadComments(id_menu);

            // comment
            // stars
            // name
            // CreateDate
            
            foreach (DataRow item in dados.Rows)
            {
                string comentario =  item["comment"].ToString();
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

        private void LoadMenuInfo()
        {
            int id_menu = int.Parse(Request["id"].ToString());

            Models.Menu menuobj = Models.Menu.GetMenu(id_menu);
            Restaurant restaurantobj = Restaurant.GetRestaurante(menuobj.restaurant);

            int aleatorio = new Random().Next(999999);

            menu_foto.Src = $"~/Public/images/menus/{id_menu}.jpg?{aleatorio}";
            menu_foto.Alt = $"{menuobj.title}";

            menu_nome.InnerText = menuobj.title;

            string estrelasHtml = "";
            for (int i = 1; i < 6; i++)
            {
                if (i <= menuobj.stars)
                    estrelasHtml += "<i class='fas fa-star text-warning'></i>";
                else
                    estrelasHtml += "<i class='far fa-star text-warning'></i>";
            }

            menu_estrelas.InnerHtml = estrelasHtml;
            menu_descricao.InnerText = menuobj.description;
            menu_preco.InnerText = menuobj.price.ToString("C2");
            menu_restaurante.InnerText = restaurantobj.name;
            if (!menuobj.stock)
                btn_adicionarcarrinho.Visible = false;
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

                int id_menu = int.Parse(Request["id"].ToString());
                int estrelas = int.Parse(Request.Cookies["estrelas"].Value);

                if (Session["id_user"] == null)
                    throw new Exception("Não estás logado.");

                int id_user = int.Parse(Session["id_user"].ToString());

                MenuComment menuComment = new MenuComment();
                menuComment.user = id_user;
                menuComment.menu = id_menu;
                menuComment.comment = comentario;
                menuComment.stars = estrelas;

                menuComment.Adicionar();

                lb_erro_comentario.Visible = false;
                txt_comentario.Text = "";
                LoadComments();
                LoadMenuInfo();

            }
            catch (Exception erro)
            {
                lb_erro_comentario.Text = erro.Message;
                lb_erro_comentario.Visible = true;
            }
            
                
        }
    }
}