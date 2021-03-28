using M17_Food4U.Classes;
using M17_Food4U.Models;
using PayoutsSdk.Payouts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.restaurant
{
    public partial class gerirsaldo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_levantarsaldo_Click(object sender, EventArgs e)
        {
            _ = levantar();
            Response.Redirect(Request.RawUrl);
        }

        private async System.Threading.Tasks.Task levantar()
        {
            try
            {
                var dp_restaurantes = (Page.Master.FindControl("dp_restaurantes") as DropDownList);

                int id_restaurante = int.Parse(dp_restaurantes.SelectedValue.ToString());

                int id_user = int.Parse(Session["id_user"].ToString());
                if (!Restaurant.UserOwnsRestaurant(id_restaurante, id_user))
                    Response.Redirect("~/restaurant/dashboard.aspx");

                string emailPaypal = txt_emaillevantar.Text;
                double valor = double.Parse(txt_moneylevantar.Text.Replace(".",","));

                if (emailPaypal == String.Empty || emailPaypal.Contains("@") == false || emailPaypal.Contains(".") == false)
                    throw new Exception("O email indicado não é válido.");

                if (valor <= 0)
                    throw new Exception("Valor indicado inválido");

                Restaurant restaurante = Restaurant.GetRestaurante(id_restaurante);
                double saldo = restaurante.saldo;

                if (saldo - valor < 0)
                    throw new Exception("O seu saldo não permite executar esta ação!");

                var body = new CreatePayoutRequest()
                {
                    SenderBatchHeader = new SenderBatchHeader()
                    {
                        EmailMessage = $"O teu levantamento foi bem sucedido, recebeste {valor}",
                        EmailSubject = "Recebeste dinheiro da Food4U!!"
                    },
                    Items = new List<PayoutItem>(){
                        new PayoutItem()
                        {
                            RecipientType="EMAIL",
                            Amount=new Currency(){
                            CurrencyCode="EUR",
                            Value=valor.ToString().Replace(",","."),
                        },
                            Receiver= emailPaypal,
                        }
                    }
                };

                PayoutsPostRequest request = new PayoutsPostRequest();
                request.RequestBody(body);
                Transacao.LevantarDinheiroRestaurante(id_restaurante, valor);

                var response = await CreatePayout.client().Execute(request);
                var result = response.Result<CreatePayoutResponse>();


                Debug.WriteLine("Status: {0}", result.BatchHeader.BatchStatus);
                Debug.WriteLine("Batch Id: {0}", result.BatchHeader.PayoutBatchId);
                Debug.WriteLine("Links:");
                foreach (LinkDescription link in result.Links)
                {
                    Debug.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                }
            }
            catch (Exception erro)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "MostrarNotificação", $"ShowNotification('Erro','{erro.Message}', 'error')", true);

            }
        }
    }
}