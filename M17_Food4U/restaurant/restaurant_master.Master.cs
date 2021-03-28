using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.restaurant
{
    public partial class restaurant_master : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if(Session["perfil"] == null || Session["perfil"].ToString() != "1")
                Response.Redirect("~/index.aspx");

            HttpCookie cookie = Request.Cookies["allow-cookies"];
            if (cookie != null)
                div_aviso.Visible = false;

            if (IsPostBack)
                return;
            
            

            AtualizarRestaurantes();

            if (dp_restaurantes.Items.Count <= 0 || dp_restaurantes.Items[0].Text == "Sem resultados")
                return;
            int id_restaurante = int.Parse(dp_restaurantes.SelectedValue.ToString());
            var lb_restsaldo = (Page.Master.FindControl("lb_restsaldo") as Label);

            Restaurant restaurant = Restaurant.GetRestaurante(id_restaurante);
            lb_restsaldo.Text = restaurant.saldo.ToString("C2");

        }
        protected void bt1_Click(object sender, EventArgs e)
        {
            //criar o cookie e enviar para o browser
            div_aviso.Visible = false;
            HttpCookie novo = new HttpCookie("allow-cookies");
            novo.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(novo);
        }
        private void AtualizarRestaurantes()
        {
            try
            {
                int id_user = int.Parse(Session["id_user"].ToString());

                DataTable dados = Restaurant.ListarRestaurantesUser(id_user);

                if (dados.Rows.Count <= 0 && Request.Url.LocalPath != "/restaurant/novorestaurante.aspx")
                    Response.Redirect("/restaurant/novorestaurante.aspx");

                foreach (DataRow row in dados.Rows)
                {
                    bool enabled = bool.Parse(row["enabled"].ToString());

                    dp_restaurantes.Items.Add(new ListItem(row["name"].ToString() + (enabled ? "": " (Bloqueado)"), row["id"].ToString()));
                }

                if (Session["id_restaurante"] != null)
                {
                    dp_restaurantes.SelectedValue = Session["id_restaurante"].ToString();
                    
                }               
            }
            catch (Exception erro)
            {
                dp_restaurantes.Items.Add("Sem resultados");
            }
        }

        protected void btn_user_Click(object sender, EventArgs e)
        {
            if (Session["perfil"] != null)
            {
                Response.Redirect("/User/perfil.aspx");
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }

        protected void dp_restaurantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["id_restaurante"] = dp_restaurantes.SelectedValue.ToString();

            var lb_restsaldo = (Page.Master.FindControl("lb_restsaldo") as Label);

            Restaurant restaurant = Restaurant.GetRestaurante(int.Parse(Session["id_restaurante"].ToString()));
            lb_restsaldo.Text = restaurant.saldo.ToString("C2");
        }
    }
}