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

            if (IsPostBack)
                return;

            AtualizarRestaurantes();

        }

        private void AtualizarRestaurantes()
        {
            try
            {
                int id_user = int.Parse(Session["id_user"].ToString());

                DataTable dados = Restaurant.ListarRestaurantesUser(id_user);

                if (dados.Rows.Count == 0)
                    throw new Exception("Sem resultados");

                foreach (DataRow row in dados.Rows)
                {
                    dp_restaurantes.Items.Add(new ListItem(row["name"].ToString(), row["id"].ToString()));
                }

                if (Session["id_restaurante"] != null)
                    dp_restaurantes.SelectedValue = Session["id_restaurante"].ToString();
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
        }
    }
}