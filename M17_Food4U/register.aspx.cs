using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace M17_Food4U
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void dp_perfis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dp_perfis.SelectedValue == "3")
            {
                frm_estafeta.Visible = false;
                frm_restaurante.Visible = false;
            }
            else if (dp_perfis.SelectedValue == "1")
            {
                frm_estafeta.Visible = false;
                frm_restaurante.Visible = true;
            }
            else if (dp_perfis.SelectedValue == "2")
            {
                frm_estafeta.Visible = true;
                frm_restaurante.Visible = false;
            }
        }

        protected void btn_registar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txt_email.Text;
                string nome = txt_nome.Text;
                string nif = txt_nif.Text;
                DateTime data_nasc = DateTime.Parse(txt_data_nasc.Text);
                string password = txt_password.Text;

                

                //Validar dados
                //email
                if (email == String.Empty || email.Contains("@") == false || email.Contains(".") == false)
                    throw new Exception("O email indicado não é válido.");

                //nome
                if (nome == String.Empty || nome.Trim().Length < 3)
                    throw new Exception("O nome indicado não é válido. Deve ter pelo menos 3 letras.");

                //nif
                int inif = int.Parse(nif);
                if (nif.Trim().Length != 9)
                    throw new Exception("O NIF indicado não é válido. Deve ter 9 digitos.");

                //password
                if (password.Trim().Length < 5)
                    throw new Exception("A password é muito pequena");

                var respostaRecaptcha = Request.Form["g-Recaptcha-Response"];
                var valido = ReCaptcha.Validate(respostaRecaptcha);
                if (valido == false)
                    throw new Exception("Tem de provar que não é um robô.");

                int perfil = int.Parse(dp_perfis.SelectedValue);
                int[] allowedPerfis = new int[3] { 1, 2, 3 };

                if (!allowedPerfis.Contains(perfil))
                    throw new Exception("O perfil indicado é inválido.");

                User utilizador = new User();
                utilizador.email = email;
                utilizador.nome = nome;
                utilizador.nif = nif;
                utilizador.data_nasc = data_nasc;
                utilizador.password = password;
                utilizador.perfil = perfil;

                if (perfil == 1)
                {
                    AdicionarRestaurante(utilizador);
                }
                else if(perfil == 2)
                {
                    AdicionarEstafeta(utilizador);

                }
                else if(perfil == 3)
                {
                    utilizador.Adicionar();
                }

                lb_erro.Text = "Registado com sucesso!";
                div_erro.Attributes["class"] = "alert alert-success";
                div_erro.Visible = true;

                ScriptManager.RegisterStartupScript(this, typeof(Page), "Redirecionar", "returnMain('/login.aspx');", true);

            }
            catch (Exception erro)
            {
                lb_erro.Text = erro.Message;
                div_erro.Attributes["class"] = "alert alert-danger";
                div_erro.Visible = true;
            }
        }

        private void AdicionarEstafeta(User utilizador)
        {
            string carta_conducao = txt_carta_conducao.Text.ToUpper();
            DateTime validade_carta_conducao = DateTime.Parse(txt_validade_carta_conducao.Text);
            
            if (carta_conducao == String.Empty || Regex.Match(carta_conducao.Trim(), @"^\w{2}-\d{5}\s\d{1}$").Success == false)
                throw new Exception("Carta de condução inválida. Formato (AA-12345 1)");

            if (DateTime.Now.Year - utilizador.data_nasc.Year < 18 )
                throw new Exception("Deves ter no mímimo 18 anos para poderes ser estafeta da Food4U");


            if (validade_carta_conducao <= DateTime.Now)
                throw new Exception("Validade da carta de condução inválida");

            
            utilizador.driving_license = carta_conducao;
            utilizador.driving_licenseValidity = validade_carta_conducao;
            utilizador.AdicionarEstafeta();
        }

        private void AdicionarRestaurante(User utilizador)
        {
            string restaurante = txt_nome_restaurante.Text;
            string cidade = txt_cidade.Text;
            string cp = txt_cp.Text;
            string morada = txt_morada.Text;

            if (restaurante == String.Empty || restaurante.Trim().Length <= 3)
                throw new Exception("O nome indicado não é válido. Deve ter pelo menos 3 letras.");

            if (DateTime.Now.Year - utilizador.data_nasc.Year < 18)
                throw new Exception("Deves ter no mímimo 18 anos para poderes ser responsável por um restaurante na Food4U");

            if (cidade == String.Empty || cidade.Trim().Length <= 3)
                throw new Exception("A cidade indicada não é válida. Deve ter pelo menos 3 letras.");
            
            if (cp == String.Empty || Regex.Match(cp.Trim(), @"^\d{4}(-\d{3})?$").Success == false)
                throw new Exception("O código postal indicado não é válido. Deve ter o seguinte formato xxxx-xxx.");

            if (morada == String.Empty || morada.Trim().Length <= 3)
                throw new Exception("A morada indicada não é válida. Deve ter pelo menos 3 letras.");

            //capa
            if (FileUpload1.HasFile == false)
                throw new Exception("Tem de indicar o ficheiro da foto do restaurante");
            if (FileUpload1.PostedFile.ContentType != "image/jpeg" &&
            FileUpload1.PostedFile.ContentType != "image/jpg" &&
            FileUpload1.PostedFile.ContentType != "image/png")
                throw new Exception("O formato do ficheiro da foto do restaurante não é suportado.");
            if (FileUpload1.PostedFile.ContentLength == 0 ||
            FileUpload1.PostedFile.ContentLength > 5000000)
                throw new Exception("O tamanho da foto do restaurante não é válido.");

            Restaurant restaurant = new Restaurant();
            
            restaurant.name = restaurante;
            restaurant.city = cidade;
            restaurant.cp = cp;
            restaurant.address = morada;

            int id_restaurant = restaurant.AdicionarRestauranteOwner(utilizador);

            string ficheiro = Server.MapPath(@"~\Public\images\restaurants\");
            ficheiro += id_restaurant + ".jpg";
            FileUpload1.SaveAs(ficheiro);
        }
    }
}