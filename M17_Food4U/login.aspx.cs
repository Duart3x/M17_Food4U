using M17_Food4U.Classes;
using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txt_email.Text.Trim();
                string password = txt_password.Text.Trim();

                DataTable dados = UserLogin.verificaLogin(email, password);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("Login inválido");

                //Iniciar Sessão
                Session["nome"] = dados.Rows[0]["name"].ToString();
                Session["id_user"] = dados.Rows[0]["id"].ToString();
                Session["perfil"] = dados.Rows[0]["perfil"].ToString();

                var carrinho = Request.Cookies["carrinho"];
                if (carrinho != null)
                {
                    int id_user = int.Parse(dados.Rows[0]["id"].ToString());
                    foreach (var item in carrinho.Values.AllKeys)
                    {
                        int id_menu = int.Parse(item);
                        int quantidade = int.Parse(carrinho.Values[item]);

                        ShoppingCart.InsertMenuCarrinho(id_user, id_menu, quantidade);
                    }
                    carrinho.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Set(carrinho);
                }

                Response.Redirect("/index.aspx");
            }
            catch (Exception erro)
            {
                lb_erro.Text = erro.Message;
                div_erro.Attributes["class"] = "alert alert-danger";
                div_erro.Visible = true;
            }
            
        }

        protected void btn_recuperar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txt_email.Text.Trim();
                if (email.Length == 0)
                    throw new Exception("Erro no email");
                Models.User utilizador = new Models.User();

                DataTable dados = Models.User.GetUser(email);
                
                if (dados == null || dados.Rows.Count == 0 || dados.Rows.Count > 1)
                    throw new Exception("Erro no email");

                Guid g = new Guid();
                utilizador.recuperarPassword(email, g.ToString());

                string mensagem = "Clique no link para recuperar a sua passowrd. \n";
                mensagem += "<a href='http://" + Request.Url.Authority + "/recuperarPassword.aspx?";
                mensagem += "id=" + Server.UrlEncode(g.ToString()) + "'>Clique aqui</a>";

                string ppassword = ConfigurationManager.AppSettings["pwdEmail"].ToString();
                string meuEmail = ConfigurationManager.AppSettings["Email"].ToString();

                Helper.enviarMail(meuEmail, ppassword, email, "Recuperar Password", mensagem);

                /*lb_erro.Text = "Foi enviado um email de recuperação de password";
                div_erro.Attributes["class"] = "alert alert-success";
                div_erro.Visible = true;*/

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", $"ShowNotification('Email enviado com sucesso','Foi lhe enviado um email para recuperar a sua password.', 'success')", true);
            }
            catch (Exception erro)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", $"ShowNotification('Erro','{erro.Message}', 'error')", true);

            }
        }
    }
}