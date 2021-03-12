using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.restaurant
{
    public partial class editarmenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
                return;

            try
            {
                if (Request["id"] == null || Request["id"].ToString() == "")
                    Response.Redirect("~/restaurant/menus.aspx");

                int id_menu = int.Parse( Request["id"].ToString());

                var dp_restaurantes = (Page.Master.FindControl("dp_restaurantes") as DropDownList);
                int id_restaurante = int.Parse(dp_restaurantes.SelectedValue.ToString());

                int id_user = int.Parse(Session["id_user"].ToString());
                if (!Restaurant.UserOwnsRestaurant(id_restaurante, id_user))
                    Response.Redirect("~/restaurant/dashboard.aspx");

                Models.Menu menu = Models.Menu.GetMenu(id_menu);
                if (menu == null)
                    throw new Exception("Menu inválido");

                txt_nome.Text = menu.title;
                txt_descricao.Text = menu.description;
                txt_preco.Text = menu.price.ToString();

                int aleatorio = new Random().Next(999999);
                img_menu.Src = $"~/Public/images/menus/{menu.id}.jpg?" + aleatorio;
                img_menu.Alt = menu.title;
            }
            catch (Exception erro)
            {
                Response.Redirect("~/restaurant/menus.aspx");
            }

        }

        protected void btn_confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string nome = txt_nome.Text.Trim();
                string descricao = txt_descricao.Text.Trim();
                double preco = double.Parse(txt_preco.Text.Trim());

                if (nome.Length < 3 || nome.Length > 100)
                    throw new Exception("Nome inválido");

                if (descricao.Length < 3 || descricao.Length > 255)
                    throw new Exception("Descrição inválida");

                if (preco <= 0 || preco > 100)
                    throw new Exception("Preço inválido");

                if (FileUpload1.HasFile)
                {
                    if (FileUpload1.PostedFile.ContentType != "image/jpeg" &&
                    FileUpload1.PostedFile.ContentType != "image/jpg" &&
                    FileUpload1.PostedFile.ContentType != "image/png")
                        throw new Exception("O formato do ficheiro da foto do menu não é suportado.");
                    if (FileUpload1.PostedFile.ContentLength == 0 ||
                    FileUpload1.PostedFile.ContentLength > 5000000)
                        throw new Exception("O tamanho da foto do menu não é válido.");                    
                }
                

                int id_menu = int.Parse(Request["id"].ToString());
                int id_user = int.Parse(Session["id_user"].ToString());
                var dp_restaurantes = (Page.Master.FindControl("dp_restaurantes") as DropDownList);
                int id_restaurante = int.Parse(dp_restaurantes.SelectedValue.ToString());
                if (Restaurant.UserOwnsRestaurant(id_restaurante, id_user))
                {
                    Models.Menu menu = new Models.Menu();
                    menu.id = id_menu;
                    menu.title = nome;
                    menu.description = descricao;
                    menu.price = preco;

                    menu.Atualizar();

                    if (FileUpload1.HasFile)
                    {
                        string ficheiro = Server.MapPath(@"~\Public\images\menus\");
                        ficheiro += id_menu + ".jpg";
                        FileUpload1.SaveAs(ficheiro);
                    }

                    Response.Redirect("~/restaurant/menus.aspx");

                    txt_nome.Text = "";
                    txt_descricao.Text = "";
                    txt_preco.Text = "";
                }
                else
                    throw new Exception("Restaurante inválido");

            }
            catch (Exception erro)
            {
                lb_erro.Text = erro.Message;
                div_erro.Attributes["class"] = "alert alert-danger";
                div_erro.Visible = true;
            }

        }
    }
}