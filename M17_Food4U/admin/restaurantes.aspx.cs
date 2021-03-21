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
    public partial class restaurantes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            
            DataTable dados = Restaurant.GetRestaurantes();

            dgv_restaurantes.Columns.Clear();
            dgv_restaurantes.DataSource = null;
            dgv_restaurantes.DataBind();

            dgv_restaurantes.DataSource = dados;
            dgv_restaurantes.AutoGenerateColumns = false;

            BoundField bfID = new BoundField();
            bfID.HeaderText = "ID";
            bfID.DataField = "id";
            dgv_restaurantes.Columns.Add(bfID);

            BoundField bfNome = new BoundField();
            bfNome.HeaderText = "Nome";
            bfNome.DataField = "name";
            dgv_restaurantes.Columns.Add(bfNome);

            BoundField bfDono = new BoundField();
            bfDono.HeaderText = "Dono";
            bfDono.DataField = "owner";
            dgv_restaurantes.Columns.Add(bfDono);

            BoundField bfCity = new BoundField();
            bfCity.HeaderText = "Cidade";
            bfCity.DataField = "city";
            dgv_restaurantes.Columns.Add(bfCity);

            BoundField bfAddress = new BoundField();
            bfAddress.HeaderText = "Morada";
            bfAddress.DataField = "address";
            dgv_restaurantes.Columns.Add(bfAddress);

            BoundField bfCP = new BoundField();
            bfCP.HeaderText = "Código Postal";
            bfCP.DataField = "cp";
            dgv_restaurantes.Columns.Add(bfCP);

            BoundField bfEnabled = new BoundField();
            bfEnabled.HeaderText = "Estado";
            bfEnabled.DataField = "enabled";
            dgv_restaurantes.Columns.Add(bfEnabled);

            BoundField bfSaldo = new BoundField();
            bfSaldo.HeaderText = "Saldo";
            bfSaldo.DataField = "saldo";
            bfSaldo.DataFormatString = "{0:C}";
            dgv_restaurantes.Columns.Add(bfSaldo);

            ButtonField dcOpcoes = new ButtonField();
            dcOpcoes.HeaderText = "Opções";
            dcOpcoes.ButtonType = ButtonType.Link;
            dgv_restaurantes.Columns.Add(dcOpcoes);


            DataColumn dcDetalhes2 = new DataColumn();
            dcDetalhes2.ColumnName = "Menus";
            dcDetalhes2.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcDetalhes2);

            HyperLinkField hldetalhes2 = new HyperLinkField();
            hldetalhes2.HeaderText = "Menus";
            hldetalhes2.DataTextField = "Menus";
            hldetalhes2.Text = "Menus";
            hldetalhes2.DataNavigateUrlFormatString = "~/admin/restaurantemenus.aspx?restaurante={0}";
            hldetalhes2.DataNavigateUrlFields = new string[] { "id" };
            dgv_restaurantes.Columns.Add(hldetalhes2);

            dgv_restaurantes.DataBind();

        }

        protected void dgv_restaurantes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count > 1)
            {
                int id = int.Parse(e.Row.Cells[0].Text);
                bool estado = bool.Parse(e.Row.Cells[6].Text);

                if (estado)
                    e.Row.Cells[6].Text = "Ativado";
                else
                    e.Row.Cells[6].Text = "Bloqueado";

                LinkButton hlativo = new LinkButton();
                hlativo.Text = "<i data-toggle='tooltip' data-placement='top' title='Ativar/Desativar' class='far fa-ban mr-3'></i>";
                hlativo.OnClientClick = $"btnblockclick({id},{(estado ? "true" : "false")}); return false;";
                
                LinkButton hledit = new LinkButton();
                hledit.Text = "<i data-toggle='tooltip' data-placement='top' title='Editar' class='far fa-edit text-warning mr-3'></i>";
                hledit.PostBackUrl = $"~/admin/restauranteedit.aspx?id={id}";

                LinkButton hleliminar = new LinkButton();
                hleliminar.Text = "<i data-toggle='tooltip' data-placement='top' title='Eliminar' class='far fa-trash-alt text-danger'></i>";
                hleliminar.OnClientClick = $"btndeleteclick({id}); return false;";

                e.Row.Cells[8].Controls.Add(hlativo);
                e.Row.Cells[8].Controls.Add(hledit);
                e.Row.Cells[8].Controls.Add(hleliminar);
            }
        }

        protected void btn_ativar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string id_restaurante = Request.Cookies["id_restaurante"].Value;

                if (id_restaurante != null)
                {
                    int num_id_restaurante = int.Parse(id_restaurante);
                    Restaurant.ToggleRestaurante(num_id_restaurante, true);
                    AtualizarGrid();

                }
            }
            catch
            {
                AtualizarGrid();
            }
            Request.Cookies["id_restaurante"].Expires = DateTime.Now.AddDays(-1);
        }

        protected void btn_desativar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string id_restaurante = Request.Cookies["id_restaurante"].Value;
                if (id_restaurante != null)
                {
                    int num_id_restaurante = int.Parse(id_restaurante);

                    Restaurant.ToggleRestaurante(num_id_restaurante, false);
                    AtualizarGrid();

                }
            }
            catch
            {
                AtualizarGrid();
            }
            Request.Cookies["id_restaurante"].Expires = DateTime.Now.AddDays(-1);
        }

        protected void btn_eliminar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string id_restaurante = Request.Cookies["id_restaurante"].Value;
                if (id_restaurante != null)
                {
                    int num_id_restaurante = int.Parse(id_restaurante);

                    Restaurant.DeleteRestaurante(num_id_restaurante);
                    AtualizarGrid();

                }
            }
            catch
            {
                AtualizarGrid();
            }
            Request.Cookies["id_restaurante"].Expires = DateTime.Now.AddDays(-1);
        }
    }
}