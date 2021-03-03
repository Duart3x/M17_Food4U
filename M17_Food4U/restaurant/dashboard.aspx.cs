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
    public partial class dashboard : System.Web.UI.Page
    {
        List<int> selecteds = new List<int>();
        DateTime? filter_data = null;
        DateTime? filter_inicio = null;
        DateTime? filter_fim = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            var dp_restaurantes = (Page.Master.FindControl("dp_restaurantes") as DropDownList);
            dp_restaurantes.SelectedIndexChanged += Dp_restaurantes_SelectedIndexChanged;
            AtualizaGrid();
        }

        private void Dp_restaurantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_user = int.Parse(Session["id_user"].ToString());
            int id_restaurante = int.Parse((sender as DropDownList).SelectedValue.ToString());
            if (Restaurant.UserOwnsRestaurant(id_restaurante, id_user))
                AtualizaGrid();
        }

        private void AtualizaGrid()
        {
            try
            {
                var dp_restaurantes = (Page.Master.FindControl("dp_restaurantes") as DropDownList);

                if (dp_restaurantes.SelectedValue.ToString() != null && dp_restaurantes.SelectedValue.ToString() != "")
                {
                    dgv_pedidos.Columns.Clear();
                    dgv_pedidos.DataSource = null;
                    dgv_pedidos.DataBind();

                    List<int> aux = new List<int>();

                    if (check_espera.Checked) //  1
                        aux.Add(1);
                    if (check_aserpreparados.Checked) // 2
                        aux.Add(2);
                    if (check_concluidos.Checked) // 3
                        aux.Add(3);

                    selecteds = aux.ToList();
                    if(txt_hour_start.Text != "")
                        filter_inicio = DateTime.Parse(txt_hour_start.Text);

                    if (txt_hour_end.Text != "")
                        filter_fim = DateTime.Parse(txt_hour_end.Text);

                    if (txt_datepicker.Text != "")
                        filter_data = DateTime.Parse(txt_datepicker.Text);

                    Restaurant restaurante = new Restaurant(int.Parse(dp_restaurantes.SelectedValue.ToString()));
                    DataTable dados = restaurante.ListarPedidosRestaurante(selecteds, filter_data, filter_inicio, filter_fim);
                    dgv_pedidos.DataSource = dados;

                    dgv_pedidos.DataBind();
                }
            }
            catch (Exception)
            {

            }
            
        }

        protected void checks_geral_CheckedChanged(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        protected void txt_datepicker_TextChanged(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        protected void txt_hour_start_TextChanged(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        protected void txt_hour_end_TextChanged(object sender, EventArgs e)
        {
            AtualizaGrid();
        }
    }
}