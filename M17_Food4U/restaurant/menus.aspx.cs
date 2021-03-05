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
    public partial class menus : System.Web.UI.Page
    {
        const int GRID_PAGE_SIZE = 5;
        protected void Page_Load(object sender, EventArgs e)
        {
            var dp_restaurantes = (Page.Master.FindControl("dp_restaurantes") as DropDownList);
            int id_restaurante = int.Parse(dp_restaurantes.SelectedValue.ToString());

            int id_user = int.Parse(Session["id_user"].ToString());
            if (!Restaurant.UserOwnsRestaurant(id_restaurante, id_user))
                Response.Redirect("~/restaurant/dashboard.aspx");

            if (IsPostBack)
                return;

            dgv_menus.AllowPaging = true;
            dgv_menus.PageSize = GRID_PAGE_SIZE;

            dgv_menus.PageIndexChanging += Dgv_menus_PageIndexChanging;

            AtualizarGrid();
        }

        private void Dgv_menus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgv_menus.PageIndex = e.NewPageIndex;
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            var dp_restaurantes = (Page.Master.FindControl("dp_restaurantes") as DropDownList);
            int id_restaurante = int.Parse(dp_restaurantes.SelectedValue.ToString());

            int id_user = int.Parse(Session["id_user"].ToString());
            if (!Restaurant.UserOwnsRestaurant(id_restaurante, id_user))
                Response.Redirect("~/restaurant/dashboard.aspx");

            Restaurant restaurant = new Restaurant(id_restaurante);
            DataTable dados = restaurant.ListarMenusRestaurante();

            dgv_menus.Columns.Clear();
            dgv_menus.DataSource = null;
            dgv_menus.DataBind();


            dgv_menus.DataSource = dados;
            dgv_menus.AutoGenerateColumns = false;

            BoundField bfID = new BoundField();
            bfID.HeaderText = "ID";
            bfID.DataField = "id";
            dgv_menus.Columns.Add(bfID);

            BoundField bfNome = new BoundField();
            bfNome.HeaderText = "Nome";
            bfNome.DataField = "nome";
            dgv_menus.Columns.Add(bfNome);

            BoundField bfDescription = new BoundField();
            bfDescription.HeaderText = "Descrição";
            bfDescription.DataField = "descricao";
            dgv_menus.Columns.Add(bfDescription);

            BoundField bfPreco = new BoundField();
            bfPreco.HeaderText = "Preço";
            bfPreco.DataField = "preco";
            bfPreco.DataFormatString = "{0:C}";
            dgv_menus.Columns.Add(bfPreco);

            BoundField bfEstrelas = new BoundField();
            bfEstrelas.HeaderText = "Estrelas";
            bfEstrelas.DataField = "estrelas";
            dgv_menus.Columns.Add(bfEstrelas);

            BoundField bfStock = new BoundField();
            bfStock.HeaderText = "Em Stock";
            bfStock.DataField = "stock";
            dgv_menus.Columns.Add(bfStock);

            BoundField bfAtivado = new BoundField();
            bfAtivado.HeaderText = "Ativado";
            bfAtivado.DataField = "ativado";
            dgv_menus.Columns.Add(bfAtivado);

            BoundField bfQuantidade = new BoundField();
            bfQuantidade.HeaderText = "Vendas";
            bfQuantidade.DataField = "quantidade";
            dgv_menus.Columns.Add(bfQuantidade);

            ButtonField dcOpcoes = new ButtonField();
            dcOpcoes.HeaderText = "Opções";
            dcOpcoes.ButtonType = ButtonType.Link;
            dgv_menus.Columns.Add(dcOpcoes);
           

            dgv_menus.DataBind();
        }

        protected void btn_adicionarmenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/restaurant/adicionarmenu.aspx");
        }

        protected void dgv_menus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            

            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count > 1)
            {
                LinkButton hleditar = new LinkButton();
                hleditar.Text = "<i data-toggle='tooltip' data-placement='top' title='Editar' class='far fa-edit mr-3'></i>";
                hleditar.PostBackUrl = "~/restaurant/editarmenu.aspx?id=" + e.Row.Cells[0].Text;
                LinkButton hlativo = new LinkButton();
                hlativo.Text = "<i data-toggle='tooltip' data-placement='top' title='Ativar/Desativar' class='far fa-ban'></i>";
                hlativo.PostBackUrl = "~/restaurant/togglemenu.aspx?id=" + e.Row.Cells[0].Text;

                e.Row.Cells[8].Controls.Add(hleditar);
                e.Row.Cells[8].Controls.Add(hlativo);
            }
                

        }
    }
}