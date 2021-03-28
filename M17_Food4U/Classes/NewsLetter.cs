using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace M17_Food4U.Classes
{
    public class NewsLetter
    {
        BaseDados bd;
        Estatisticas estatisticas;

        string ppassword = ConfigurationManager.AppSettings["pwdEmail"].ToString();
        string meuEmail = ConfigurationManager.AppSettings["Email"].ToString();
        public NewsLetter()
        {
            bd = new BaseDados();
            estatisticas = new Estatisticas();
        }

        public string EnviarMenusMaisVendidos(string BaseUrl)
        {
            DataTable dados = estatisticas.get3MenusMaisvendidos();
            string mensagem = @"
            <div>
            <h2>Menus sugestão da semana</h2>
            <div>";
            foreach (DataRow row in dados.Rows)
            {
                mensagem += $@"<a href='http://{BaseUrl}/menu.aspx?id={row["id_menu"].ToString()}'>{row["title"].ToString()} - {row["description"].ToString()}</a>
                <br />";
            }
            mensagem += @"
                </div>
            </div>";

            DataTable users = Models.User.ListarUtilizadores();

            foreach (DataRow row in users.Rows)
            {
                string email = row["email"].ToString();

                Helper.enviarMail(meuEmail, ppassword, email, "Sugestão de Menus", mensagem);
            }

            return mensagem;
        }

        public string EnviarMenusMenosVendidos(string BaseUrl)
        {
            DataTable dados = estatisticas.get3MenusMenosvendidos();
            string mensagem = @"
            <div>
            <h2>Menus sugestão da semana</h2>
            <div>";
            foreach (DataRow row in dados.Rows)
            {
                mensagem += $@"<a href='http://{BaseUrl}/menu.aspx?id={row["id_menu"].ToString()}'>{row["title"].ToString()} - {row["description"].ToString()}</a>
                <br />";
            }
            mensagem += @"
                </div>
            </div>";


            DataTable users = Models.User.ListarUtilizadores();

            foreach (DataRow row in users.Rows)
            {
                string email = row["email"].ToString();

                Helper.enviarMail(meuEmail, ppassword, email, "Sugestão de Menus", mensagem);
            }

            return mensagem;
        }

        public string EnviarCarrinhoCheio(string BaseUrl)
        {
            string Emails = "";
            string mensagem = @"";

            DataTable dados = estatisticas.getClientesComCarrinho();

            string lastEmail = "";
            string lastMensagem = "";
            foreach (DataRow row in dados.Rows)
            {
                string email = row["email"].ToString();

                

                if (email != lastEmail)
                    mensagem = @"
                            <div>
                            <h2>Produtos no carrinho</h2>
                            <div>";

                mensagem += $@"<a href='http://{BaseUrl}/checkout.aspx'>{row["title"].ToString()} - {row["description"].ToString()} : {row["quantity"].ToString()} unidades</a>
                <br />";

                if (email != lastEmail && lastEmail != "")
                {
                    lastMensagem += @"
                        </div>
                    </div>";
                    Helper.enviarMail(meuEmail, ppassword, lastEmail, "Menus perdidos no carrinho?", lastMensagem);
                    Emails += $"<br /><p class='text-muted'>{lastEmail}</p>" + lastMensagem;
                }

                lastEmail = email;
                lastMensagem = mensagem;
            }

            lastMensagem += @"
                        </div>
                    </div>";
            Helper.enviarMail(meuEmail, ppassword, lastEmail, "Menus perdidos no carrinho?", lastMensagem);
            Emails += $"<br /><p class='text-muted'>{lastEmail}</p>" + lastMensagem;

            return Emails;
        }

        public string EnviarClientesSaldo(string BaseUrl)
        {
            string Emails = "";
            DataTable dados = Models.User.ListarUtilizadores();

            foreach (DataRow row in dados.Rows)
            {
                double saldo = double.Parse(row["saldo"].ToString());

                if (saldo <= 0)
                    continue;

                string email = row["email"].ToString();

                string mensagem = $"<div><h2>Saldo na conta</h2><a href='http://{BaseUrl}/index.aspx'> <p>Não se esqueça que ainda tem <b>{saldo.ToString("C2")} </b> para gastar em comida!</p></a></div>";
                Helper.enviarMail(meuEmail, ppassword, email, "Saldo por gastar", mensagem);
                Emails += $"<br /><p class='text-muted'>{email}</p>" + mensagem;
            }

            return Emails;
        }
    }
}