using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.admin
{
    public partial class restauranteedit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                Response.Redirect("~/index.aspx");

            int id_restaurante = 0;

            if (Request["id"] == null || !int.TryParse(Request["id"].ToString(), out id_restaurante))
                Response.Redirect("~/admin/restaurantes.aspx");

            if (IsPostBack)
                return;

            Restaurant restaurante = Restaurant.GetRestaurante(id_restaurante);
            if(restaurante == null)
                Response.Redirect("~/admin/restaurantes.aspx");

            txt_nome_restaurante.Text = restaurante.name;
            txt_cidade.Text = restaurante.city;
            txt_cp.Text = restaurante.cp;
            txt_morada.Text = restaurante.address;


        }

        protected void btn_confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_restaurante = 0;

                if (Request["id"] == null || !int.TryParse(Request["id"].ToString(), out id_restaurante))
                    Response.Redirect("~/admin/restaurantes.aspx");

                string restaurante = txt_nome_restaurante.Text;
                string cidade = txt_cidade.Text;
                string cp = txt_cp.Text;
                string morada = txt_morada.Text;

                if (restaurante == String.Empty || restaurante.Trim().Length <= 3)
                    throw new Exception("O nome indicado não é válido. Deve ter pelo menos 3 letras.");

                if (cidade == String.Empty || cidade.Trim().Length <= 3)
                    throw new Exception("A cidade indicada não é válida. Deve ter pelo menos 3 letras.");

                if (cp == String.Empty || Regex.Match(cp.Trim(), @"^\d{4}(-\d{3})?$").Success == false)
                    throw new Exception("O código postal indicado não é válido. Deve ter o seguinte formato xxxx-xxx.");

                if (morada == String.Empty || morada.Trim().Length <= 3)
                    throw new Exception("A morada indicada não é válida. Deve ter pelo menos 3 letras.");


                if (FileUpload1.HasFile == true)
                {
                    if (FileUpload1.PostedFile.ContentType != "image/jpeg" &&
                        FileUpload1.PostedFile.ContentType != "image/jpg" &&
                        FileUpload1.PostedFile.ContentType != "image/png")
                        throw new Exception("O formato do ficheiro da foto do restaurante não é suportado.");
                    if (FileUpload1.PostedFile.ContentLength == 0 ||
                    FileUpload1.PostedFile.ContentLength > 5000000)
                        throw new Exception("O tamanho da foto do restaurante não é válido.");
                }

                Restaurant restaurant = new Restaurant();

                restaurant.id = id_restaurante;
                restaurant.name = restaurante;
                restaurant.city = cidade;
                restaurant.cp = cp;
                restaurant.address = morada;

                restaurant.Atualizar();

                if (FileUpload1.HasFile == true)
                {
                    string ficheiro = Server.MapPath(@"~\Public\images\restaurants\");
                    ficheiro += id_restaurante + ".jpg";
                    FileUpload1.SaveAs(ficheiro);
                }

                lb_erro.Text = "Atualizado com sucesso!";
                div_erro.Attributes["class"] = "alert alert-success";
                div_erro.Visible = true;

                Response.Redirect("~/admin/restaurantes.aspx");
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