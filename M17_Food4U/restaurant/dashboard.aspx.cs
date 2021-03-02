using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.restaurant
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void AtualizaGrid()
        {
            //int id_restaurante = int.Parse(Session["restaurante"].ToString());

            Restaurant restaurante = new Restaurant(2);
            restaurante.ListarPedidosRestaurante(new List<int>() {2,3});
        }

        protected void txt_datepicker_TextChanged(object sender, EventArgs e)
        {

        }

        protected void dp_estado_pedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id_user = int.Parse(Session["id_user"].ToString());
                int id_restaurante = int.Parse(dp_estado_pedidos.SelectedValue.ToString());
                Restaurant.UserOwnsRestaurant(id_restaurante, id_user);
            }
            catch (Exception erro)
            {

                throw;
            }
            
        }
    }
}