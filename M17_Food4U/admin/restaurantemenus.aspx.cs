using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.admin
{
    public partial class restaurantemenus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                Response.Redirect("~/index.aspx");

            int id_restaurante = 0;

            if (Request["restaurante"] == null || !int.TryParse(Request["restaurante"].ToString(), out id_restaurante))
                Response.Redirect("~/admin/restaurantes.aspx");

            AtualizarGrid();

            if (IsPostBack)
                return;
        }
        private void AtualizarGrid()
        {
            int id_restaurante = 0;

            if (Request["restaurante"] == null || !int.TryParse(Request["restaurante"].ToString(), out id_restaurante))
                Response.Redirect("~/admin/restaurantes.aspx");

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

            ImageField ifCapa = new ImageField();
            ifCapa.HeaderText = "Foto";
            int aleatorio = new Random().Next(999999);
            ifCapa.DataImageUrlFormatString = "~/Public/images/menus/{0}.jpg?" + aleatorio;
            ifCapa.DataImageUrlField = "id";
            ifCapa.ControlStyle.Width = 100;
            dgv_menus.Columns.Add(ifCapa);

            ButtonField dcOpcoes = new ButtonField();
            dcOpcoes.HeaderText = "Opções";
            dcOpcoes.ButtonType = ButtonType.Link;
            dgv_menus.Columns.Add(dcOpcoes);

            DataColumn dcDetalhes = new DataColumn();
            dcDetalhes.ColumnName = "Detalhes";
            dcDetalhes.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcDetalhes);

            HyperLinkField hldetalhes = new HyperLinkField();
            hldetalhes.HeaderText = "Detalhes";
            hldetalhes.DataTextField = "Detalhes";
            hldetalhes.Text = "Detalhes";
            hldetalhes.DataNavigateUrlFormatString = "~/menu.aspx?id={0}";
            hldetalhes.DataNavigateUrlFields = new string[] { "id" };
            dgv_menus.Columns.Add(hldetalhes);


            dgv_menus.DataBind();
        }

        protected void dgv_menus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count > 1)
            {
                int id = int.Parse(e.Row.Cells[0].Text);
                int estrelas = int.Parse(e.Row.Cells[4].Text);
                bool instock = bool.Parse(e.Row.Cells[5].Text.ToLower());
                bool ativado = bool.Parse(e.Row.Cells[6].Text.ToLower());

                if (instock)
                    e.Row.Cells[5].Text = "Em Stock";
                else
                    e.Row.Cells[5].Text = "<span class='text-danger'>Fora de stock</span>";

                if (ativado)
                    e.Row.Cells[6].Text = "Ativado";
                else
                    e.Row.Cells[6].Text = "<span class='text-danger'>Desativado</span>";

                string estrelasHtml = "";

                for (int i = 1; i < 6; i++)
                {
                    if (i <= estrelas)
                        estrelasHtml += "<i class='fas fa-star text-warning'></i>";
                    else
                        estrelasHtml += "<i class='far fa-star text-warning'></i>";
                }

                e.Row.Cells[4].Text = estrelasHtml;

                LinkButton hleditar = new LinkButton();
                hleditar.Text = "<i data-toggle='tooltip' data-placement='top' title='Editar' class='far fa-edit text-warning mr-3'></i>";
                hleditar.PostBackUrl = "~/admin/editarmenu.aspx?id=" + id;

                LinkButton hlstock = new LinkButton();
                hlstock.Text = "<i data-toggle='tooltip' data-placement='top' title='Em Stock/Fora de Stock' class='far fa-box-alt text-info mr-3'></i>";
                hlstock.OnClientClick = $"btnstockclick({id},{(instock ? "true" : "false")}); return false;";

                LinkButton hlativo = new LinkButton();
                hlativo.Text = "<i data-toggle='tooltip' data-placement='top' title='Ativar/Desativar' class='far fa-ban'></i>";
                hlativo.OnClientClick = $"btnblockclick({id},{(ativado ? "true" : "false")}); return false;";

                e.Row.Cells[9].Controls.Add(hleditar);
                e.Row.Cells[9].Controls.Add(hlstock);
                e.Row.Cells[9].Controls.Add(hlativo);
            }
        }

        protected void btn_ativar_ServerClick(object sender, EventArgs e)
        {
            string id_menu = Request.Cookies["id_menu"].Value;
            if (id_menu != null)
            {
                int num_id_menu = int.Parse(id_menu);
                Models.Menu.ToggleMenu(num_id_menu, true);
                AtualizarGrid();
            }
        }

        protected void btn_desativar_ServerClick(object sender, EventArgs e)
        {
            string id_menu = Request.Cookies["id_menu"].Value;
            if (id_menu != null)
            {
                int num_id_menu = int.Parse(id_menu);

                Models.Menu.ToggleMenu(num_id_menu, false);
                AtualizarGrid();
                Request.Cookies["id_menu"].Expires = DateTime.Now.AddDays(-1);
            }
        }

        protected void btn_AtivarStock_ServerClick(object sender, EventArgs e)
        {
            string id_menu = Request.Cookies["id_menu"].Value;
            if (id_menu != null)
            {
                
                int num_id_menu = int.Parse(id_menu);

                Models.Menu.ToggleStockMenu(num_id_menu, true);
                AtualizarGrid();
                Request.Cookies["id_menu"].Expires = DateTime.Now.AddDays(-1);

            }
        }

        protected void btn_DesativarStock_ServerClick(object sender, EventArgs e)
        {
            string id_menu = Request.Cookies["id_menu"].Value;
            if (id_menu != null)
            {
                
                int num_id_menu = int.Parse(id_menu);

                Models.Menu.ToggleStockMenu(num_id_menu, false);
                AtualizarGrid();
                Request.Cookies["id_menu"].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}