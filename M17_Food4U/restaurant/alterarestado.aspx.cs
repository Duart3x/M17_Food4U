using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.restaurant
{
    public partial class alterarestado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["id"] == null || Request["id"].ToString() == "")
                    Response.Redirect("~/restaurant/dashboard.aspx");

                int id_order = int.Parse(Request["id"].ToString());
                var dp_restaurantes = (Page.Master.FindControl("dp_restaurantes") as DropDownList);
                int id_restaurante = int.Parse(dp_restaurantes.SelectedValue.ToString());

                int id_user = int.Parse(Session["id_user"].ToString());
                if(!Restaurant.UserOwnsRestaurant(id_restaurante, id_user))
                    Response.Redirect("~/restaurant/dashboard.aspx");
                if (!Restaurant.OwnsOrderMenu(id_restaurante, id_order))
                    Response.Redirect("~/restaurant/dashboard.aspx");

            }
            catch (Exception erro)
            {
                Response.Redirect("~/restaurant/dashboard.aspx");
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                int id_order = int.Parse(Request["id"].ToString());
                var dp_restaurantes = (Page.Master.FindControl("dp_restaurantes") as DropDownList);
                int id_restaurante = int.Parse(dp_restaurantes.SelectedValue.ToString());

                int id_user = int.Parse(Session["id_user"].ToString());
                if (!Restaurant.UserOwnsRestaurant(id_restaurante, id_user))
                    Response.Redirect("~/restaurant/dashboard.aspx");

                if (!Restaurant.OwnsOrderMenu(id_restaurante, id_order))
                    Response.Redirect("~/restaurant/dashboard.aspx");

                int estado = int.Parse(radius_list.SelectedValue);

                if (estado <= 0 || estado > 3)
                    throw new Exception("Estado inválido");

                OrdersMenus order = new OrdersMenus(id_order);
                order.AlterarEstado(estado);


            }
            catch(Exception erro)
            {

            }
            finally
            {
                Response.Redirect("~/restaurant/dashboard.aspx");
            }

        }
    }
}