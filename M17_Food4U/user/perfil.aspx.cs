using M17_Food4U.Classes;
using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.user
{
    public partial class perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null)
                Response.Redirect("~/index.aspx");

            if (IsPostBack)
                return;

            AtualizarUser();
            AtualizarMoradas();
            

            txt_email.Attributes.Add("readonly", "true");
            txt_nome.Attributes.Add("readonly", "true");
            txt_nif.Attributes.Add("readonly", "true");
            txt_data_nasc.Attributes.Add("readonly", "true");
        }

        private void AtualizarMoradas()
        {
            int id_user = int.Parse(Session["id_user"].ToString());
            Models.User user = new Models.User(id_user);

            List<Address> moradas = user.getMoradas();
            if(moradas.Count >= 1)
            {
                txt_address1.Text = moradas[0].address;
                txt_cidade1.Text = moradas[0].city;
                txt_cp1.Text = moradas[0].cp;

                txt_address1.Attributes.Add("readonly", "true");
                txt_cidade1.Attributes.Add("readonly", "true");
                txt_cp1.Attributes.Add("readonly", "true");

                txt_address1.CssClass = "form-control-plaintext mb-1";
                txt_cidade1.CssClass = "form-control-plaintext mb-1";
                txt_cp1.CssClass = "form-control-plaintext";

                btn_adicionar_morada1.Visible = false;
                btn_editar_morada1.Visible = true;

                btn_confirmar_morada1.Attributes.Add("data-id_address", moradas[0].id.ToString());
            }
            if (moradas.Count >= 2)
            {
                txt_address2.Text = moradas[1].address;
                txt_cidade2.Text = moradas[1].city;
                txt_cp2.Text = moradas[1].cp;

                txt_address2.Attributes.Add("readonly", "true");
                txt_cidade2.Attributes.Add("readonly", "true");
                txt_cp2.Attributes.Add("readonly", "true");

                txt_address2.CssClass = "form-control-plaintext mb-1";
                txt_cidade2.CssClass = "form-control-plaintext mb-1";
                txt_cp2.CssClass = "form-control-plaintext";

                btn_adicionar_morada2.Visible = false;
                btn_editar_morada2.Visible = true;
                btn_confirmar_morada2.Attributes.Add("data-id_address", moradas[1].id.ToString());

            }
            if (moradas.Count == 3)
            {
                txt_address3.Text = moradas[2].address;
                txt_cidade3.Text = moradas[2].city;
                txt_cp3.Text = moradas[2].cp;

                txt_address3.Attributes.Add("readonly", "true");
                txt_cidade3.Attributes.Add("readonly", "true");
                txt_cp3.Attributes.Add("readonly", "true");

                txt_address3.CssClass = "form-control-plaintext mb-1";
                txt_cidade3.CssClass = "form-control-plaintext mb-1";
                txt_cp3.CssClass = "form-control-plaintext";

                btn_adicionar_morada3.Visible = false;
                btn_editar_morada3.Visible = true;
                btn_confirmar_morada3.Attributes.Add("data-id_address", moradas[2].id.ToString());

            }
        }

        private void AtualizarUser()
        {
            int id_user = int.Parse(Session["id_user"].ToString());

            Models.User user = Models.User.GetUser(id_user);

            txt_email.Text = user.email;
            txt_nome.Text = user.nome;
            txt_nif.Text = user.nif;
            txt_data_nasc.Text = user.data_nasc.ToString("yyyy-MM-dd");
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/index.aspx");
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            btn_confirmar.Visible = true;
            btn_cancelar.Visible = true;
            btn_editar.Visible = false;

            txt_email.Attributes.Remove("readonly");
            txt_nome.Attributes.Remove("readonly");
            txt_nif.Attributes.Remove("readonly");
            txt_data_nasc.Attributes.Remove("readonly");

            txt_email.CssClass = "form-control w-75";
            txt_nome.CssClass = "form-control w-75";
            txt_nif.CssClass = "form-control w-75";
            txt_data_nasc.CssClass = "form-control w-75";
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            div_erro2.Visible = false;
            btn_confirmar.Visible = false;
            btn_cancelar.Visible = false;
            btn_editar.Visible = true;

            txt_email.Attributes.Add("readonly", "true");
            txt_nome.Attributes.Add("readonly", "true");
            txt_nif.Attributes.Add("readonly", "true");
            txt_data_nasc.Attributes.Add("readonly", "true");

            txt_email.CssClass = "form-control-plaintext w-75";
            txt_nome.CssClass = "form-control-plaintext w-75";
            txt_nif.CssClass = "form-control-plaintext w-75";
            txt_data_nasc.CssClass = "form-control-plaintext w-75";
        }
        protected void btn_ConfirmarPassword_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string password = txt_password.Text;
                int id_user = int.Parse(Session["id_user"].ToString());

                if (UserLogin.CheckUserPassword(id_user, password))
                {
                    div_erro.Visible = false;

                    Models.User user = new Models.User();

                    string email = txt_email.Text;
                    string nome = txt_nome.Text;
                    string nif = txt_nif.Text;
                    DateTime data_nasc = DateTime.Parse(txt_data_nasc.Text);

                    if (email == String.Empty || email.Contains("@") == false || email.Contains(".") == false)
                        throw new Exception("O email indicado não é válido.");

                    if (DateTime.Now.Year - data_nasc.Year < 16)
                        throw new Exception("Deves ter no mímimo 16 anos para poderes usar a Food4U");

                    if (nome == String.Empty || nome.Trim().Length < 3)
                        throw new Exception("O nome indicado não é válido. Deve ter pelo menos 3 letras.");

                    int inif = int.Parse(nif);
                    if (nif.Trim().Length != 9)
                        throw new Exception("O NIF indicado não é válido. Deve ter 9 digitos.");

                    user.id = id_user;
                    user.email = email;
                    user.nome = nome;
                    user.nif = nif;
                    user.data_nasc = data_nasc;

                    user.Atualizar();

                    div_erro2.Visible = false;
                    btn_confirmar.Visible = false;
                    btn_cancelar.Visible = false;
                    btn_editar.Visible = true;

                    txt_email.Attributes.Add("readonly", "true");
                    txt_nome.Attributes.Add("readonly", "true");
                    txt_nif.Attributes.Add("readonly", "true");
                    txt_data_nasc.Attributes.Add("readonly", "true");

                    txt_email.CssClass = "form-control-plaintext w-75";
                    txt_nome.CssClass = "form-control-plaintext w-75";
                    txt_nif.CssClass = "form-control-plaintext w-75";
                    txt_data_nasc.CssClass = "form-control-plaintext w-75";

                    AtualizarUser();
                }
                else
                {
                    throw new Exception("Palavra-Passe errada");
                }
            }
            catch (Exception erro)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenModal", "openConfirmPassword()", true);

                lb_erro.Text = erro.Message;
                div_erro.Attributes["class"] = "alert alert-danger";
                div_erro.Visible = true;
            }
            

        }

        protected void btn_confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                div_erro2.Visible = false;
                string email = txt_email.Text;
                string nome = txt_nome.Text;
                string nif = txt_nif.Text;
                DateTime data_nasc = DateTime.Parse(txt_data_nasc.Text);

                if (email == String.Empty || email.Contains("@") == false || email.Contains(".") == false)
                    throw new Exception("O email indicado não é válido.");

                if (DateTime.Now.Year - data_nasc.Year < 16)
                    throw new Exception("Deves ter no mímimo 16 anos para poderes usar a Food4U");

                //nome
                if (nome == String.Empty || nome.Trim().Length < 3)
                    throw new Exception("O nome indicado não é válido. Deve ter pelo menos 3 letras.");

                //nif
                int inif = int.Parse(nif);
                if (nif.Trim().Length != 9)
                    throw new Exception("O NIF indicado não é válido. Deve ter 9 digitos.");

                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenModal", "openConfirmPassword()", true);
                div_erro.Visible = false;
            }
            catch (Exception erro)
            {
                lb_erro2.Text = erro.Message;
                div_erro2.Attributes["class"] = "alert alert-danger";
                div_erro2.Visible = true;
            }
        }

        protected void btn_editar_morada1_Click(object sender, EventArgs e)
        {
            btn_confirmar_morada1.Visible = true;
            btn_cancelar_morada1.Visible = true;
            btn_editar_morada1.Visible = false;
            

            txt_address1.Attributes.Remove("readonly");
            txt_cidade1.Attributes.Remove("readonly");
            txt_cp1.Attributes.Remove("readonly");

            txt_address1.CssClass = "form-control mb-1";
            txt_cidade1.CssClass = "form-control mb-1";
            txt_cp1.CssClass = "form-control";
        }

        protected void btn_cancelar_morada1_Click(object sender, EventArgs e)
        {
            div_erro_morada1.Visible = false;
            btn_confirmar_morada1.Visible = false;
            btn_cancelar_morada1.Visible = false;
            btn_editar_morada1.Visible = true;

            txt_address1.Attributes.Add("readonly", "true");
            txt_cidade1.Attributes.Add("readonly", "true");
            txt_cp1.Attributes.Add("readonly", "true");

            txt_address1.CssClass = "form-control-plaintext mb-1";
            txt_cidade1.CssClass = "form-control-plaintext mb-1";
            txt_cp1.CssClass = "form-control-plaintext";
        }

        protected void btn_confirmar_morada1_Click(object sender, EventArgs e)
        {
            string morada = txt_address1.Text;
            string cidade = txt_cidade1.Text;
            string cp = txt_cp1.Text;

            try
            {
                if (morada.Length <= 3)
                    throw new Exception("Morada inválida");

                if (cidade.Length < 3)
                    throw new Exception("Cidade inválida");

                if (cp == String.Empty || Regex.Match(cp.Trim(), @"^\d{4}(-\d{3})?$").Success == false)
                    throw new Exception("O código postal indicado não é válido. Deve ter o seguinte formato xxxx-xxx.");

                int id_user = int.Parse(Session["id_user"].ToString());
                int id_address = int.Parse(btn_confirmar_morada1.Attributes["data-id_address"]);

                Address address = new Address(id_address, id_user, cidade, cp, morada);
                address.Atualizar();

                div_erro_morada1.Visible = false;
                btn_confirmar_morada1.Visible = false;
                btn_cancelar_morada1.Visible = false;
                btn_editar_morada1.Visible = true;

                txt_address1.Attributes.Add("readonly", "true");
                txt_cidade1.Attributes.Add("readonly", "true");
                txt_cp1.Attributes.Add("readonly", "true");

                txt_address1.CssClass = "form-control-plaintext mb-1";
                txt_cidade1.CssClass = "form-control-plaintext mb-1";
                txt_cp1.CssClass = "form-control-plaintext";

                AtualizarMoradas();
            }
            catch (Exception erro)
            {
                lb_erro_morada1.Text = erro.Message;
                div_erro_morada1.Attributes["class"] = "alert alert-danger";
                div_erro_morada1.Visible = true;
            }
        }

        protected void btn_adicionar_morada1_Click(object sender, EventArgs e)
        {
            string morada = txt_address1.Text;
            string cidade = txt_cidade1.Text;
            string cp = txt_cp1.Text;

            try
            {
                if (morada.Length <= 3)
                    throw new Exception("Morada inválida");

                if (cidade.Length < 3)
                    throw new Exception("Cidade inválida");

                if (cp == String.Empty || Regex.Match(cp.Trim(), @"^\d{4}(-\d{3})?$").Success == false)
                    throw new Exception("O código postal indicado não é válido. Deve ter o seguinte formato xxxx-xxx.");

                int id_user = int.Parse(Session["id_user"].ToString());

                Address address = new Address(id_user, cidade, cp, morada);
                address.Adicionar();

                AtualizarMoradas();
            }
            catch (Exception erro)
            {
                lb_erro_morada1.Text = erro.Message;
                div_erro_morada1.Attributes["class"] = "alert alert-danger";
                div_erro_morada1.Visible = true;
            }
        }

        protected void btn_adicionar_morada2_Click(object sender, EventArgs e)
        {
            string morada = txt_address2.Text;
            string cidade = txt_cidade2.Text;
            string cp = txt_cp2.Text;

            try
            {
                if (morada.Length <= 3)
                    throw new Exception("Morada inválida");

                if (cidade.Length < 3)
                    throw new Exception("Cidade inválida");

                if (cp == String.Empty || Regex.Match(cp.Trim(), @"^\d{4}(-\d{3})?$").Success == false)
                    throw new Exception("O código postal indicado não é válido. Deve ter o seguinte formato xxxx-xxx.");

                int id_user = int.Parse(Session["id_user"].ToString());

                Address address = new Address(id_user, cidade, cp, morada);
                address.Adicionar();
                AtualizarMoradas();

            }
            catch (Exception erro)
            {
                lb_erro_morada2.Text = erro.Message;
                div_erro_morada2.Attributes["class"] = "alert alert-danger";
                div_erro_morada2.Visible = true;
            }
        }

        protected void btn_adicionar_morada3_Click(object sender, EventArgs e)
        {
            string morada = txt_address3.Text;
            string cidade = txt_cidade3.Text;
            string cp = txt_cp3.Text;

            try
            {
                if (morada.Length <= 3)
                    throw new Exception("Morada inválida");

                if (cidade.Length < 3)
                    throw new Exception("Cidade inválida");

                if (cp == String.Empty || Regex.Match(cp.Trim(), @"^\d{4}(-\d{3})?$").Success == false)
                    throw new Exception("O código postal indicado não é válido. Deve ter o seguinte formato xxxx-xxx.");

                int id_user = int.Parse(Session["id_user"].ToString());

                Address address = new Address(id_user, cidade, cp, morada);
                address.Adicionar();
                AtualizarMoradas();

            }
            catch (Exception erro)
            {
                lb_erro_morada3.Text = erro.Message;
                div_erro_morada3.Attributes["class"] = "alert alert-danger";
                div_erro_morada3.Visible = true;
            }
        }

        protected void btn_editar_morada2_Click(object sender, EventArgs e)
        {
            btn_confirmar_morada2.Visible = true;
            btn_cancelar_morada2.Visible = true;
            btn_editar_morada2.Visible = false;


            txt_address2.Attributes.Remove("readonly");
            txt_cidade2.Attributes.Remove("readonly");
            txt_cp2.Attributes.Remove("readonly");

            txt_address2.CssClass = "form-control mb-1";
            txt_cidade2.CssClass = "form-control mb-1";
            txt_cp2.CssClass = "form-control";
        }

        protected void btn_editar_morada3_Click(object sender, EventArgs e)
        {
            btn_confirmar_morada3.Visible = true;
            btn_cancelar_morada3.Visible = true;
            btn_editar_morada3.Visible = false;


            txt_address3.Attributes.Remove("readonly");
            txt_cidade3.Attributes.Remove("readonly");
            txt_cp3.Attributes.Remove("readonly");

            txt_address3.CssClass = "form-control mb-1";
            txt_cidade3.CssClass = "form-control mb-1";
            txt_cp3.CssClass = "form-control";
        }

        protected void btn_cancelar_morada2_Click(object sender, EventArgs e)
        {
            div_erro_morada2.Visible = false;
            btn_confirmar_morada2.Visible = false;
            btn_cancelar_morada2.Visible = false;
            btn_editar_morada2.Visible = true;

            txt_address2.Attributes.Add("readonly", "true");
            txt_cidade2.Attributes.Add("readonly", "true");
            txt_cp2.Attributes.Add("readonly", "true");

            txt_address2.CssClass = "form-control-plaintext mb-1";
            txt_cidade2.CssClass = "form-control-plaintext mb-1";
            txt_cp2.CssClass = "form-control-plaintext";
        }

        protected void btn_cancelar_morada3_Click(object sender, EventArgs e)
        {
            div_erro_morada3.Visible = false;
            btn_confirmar_morada3.Visible = false;
            btn_cancelar_morada3.Visible = false;
            btn_editar_morada3.Visible = true;

            txt_address3.Attributes.Add("readonly", "true");
            txt_cidade3.Attributes.Add("readonly", "true");
            txt_cp3.Attributes.Add("readonly", "true");

            txt_address3.CssClass = "form-control-plaintext mb-1";
            txt_cidade3.CssClass = "form-control-plaintext mb-1";
            txt_cp3.CssClass = "form-control-plaintext";
        }

        protected void btn_confirmar_morada2_Click(object sender, EventArgs e)
        {
            string morada = txt_address2.Text;
            string cidade = txt_cidade2.Text;
            string cp = txt_cp2.Text;

            try
            {
                if (morada.Length <= 3)
                    throw new Exception("Morada inválida");

                if (cidade.Length < 3)
                    throw new Exception("Cidade inválida");

                if (cp == String.Empty || Regex.Match(cp.Trim(), @"^\d{4}(-\d{3})?$").Success == false)
                    throw new Exception("O código postal indicado não é válido. Deve ter o seguinte formato xxxx-xxx.");

                int id_user = int.Parse(Session["id_user"].ToString());
                int id_address = int.Parse(btn_confirmar_morada2.Attributes["data-id_address"]);

                Address address = new Address(id_address, id_user, cidade, cp, morada);
                address.Atualizar();

                div_erro_morada2.Visible = false;
                btn_confirmar_morada2.Visible = false;
                btn_cancelar_morada2.Visible = false;
                btn_editar_morada2.Visible = true;

                txt_address2.Attributes.Add("readonly", "true");
                txt_cidade2.Attributes.Add("readonly", "true");
                txt_cp2.Attributes.Add("readonly", "true");

                txt_address2.CssClass = "form-control-plaintext mb-1";
                txt_cidade2.CssClass = "form-control-plaintext mb-1";
                txt_cp2.CssClass = "form-control-plaintext";

                AtualizarMoradas();
            }
            catch (Exception erro)
            {
                lb_erro_morada2.Text = erro.Message;
                div_erro_morada2.Attributes["class"] = "alert alert-danger";
                div_erro_morada2.Visible = true;
            }
        }

        protected void btn_confirmar_morada3_Click(object sender, EventArgs e)
        {
            string morada = txt_address3.Text;
            string cidade = txt_cidade3.Text;
            string cp = txt_cp3.Text;

            try
            {
                if (morada.Length <= 3)
                    throw new Exception("Morada inválida");

                if (cidade.Length < 3)
                    throw new Exception("Cidade inválida");

                if (cp == String.Empty || Regex.Match(cp.Trim(), @"^\d{4}(-\d{3})?$").Success == false)
                    throw new Exception("O código postal indicado não é válido. Deve ter o seguinte formato xxxx-xxx.");

                int id_user = int.Parse(Session["id_user"].ToString());
                int id_address = int.Parse(btn_confirmar_morada3.Attributes["data-id_address"]);

                Address address = new Address(id_address, id_user, cidade, cp, morada);
                address.Atualizar();

                div_erro_morada3.Visible = false;
                btn_confirmar_morada3.Visible = false;
                btn_cancelar_morada3.Visible = false;
                btn_editar_morada3.Visible = true;

                txt_address3.Attributes.Add("readonly", "true");
                txt_cidade3.Attributes.Add("readonly", "true");
                txt_cp3.Attributes.Add("readonly", "true");

                txt_address3.CssClass = "form-control-plaintext mb-1";
                txt_cidade3.CssClass = "form-control-plaintext mb-1";
                txt_cp3.CssClass = "form-control-plaintext";

                AtualizarMoradas();
            }
            catch (Exception erro)
            {
                lb_erro_morada3.Text = erro.Message;
                div_erro_morada3.Attributes["class"] = "alert alert-danger";
                div_erro_morada3.Visible = true;
            }
        }
    }
}