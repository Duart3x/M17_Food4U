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
    public partial class users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            DataTable dados = M17_Food4U.Models.User.ListarUtilizadores();

            dgv_utilizadores.Columns.Clear();
            dgv_utilizadores.DataSource = null;
            dgv_utilizadores.DataBind();

            dgv_utilizadores.DataSource = dados;
            dgv_utilizadores.AutoGenerateColumns = false;
            // id,email,name,nif,data_nasc,saldo,estado,perfil

            BoundField bfID = new BoundField();
            bfID.HeaderText = "ID";
            bfID.DataField = "id";
            dgv_utilizadores.Columns.Add(bfID);

            BoundField bfNome = new BoundField();
            bfNome.HeaderText = "Email";
            bfNome.DataField = "email";
            dgv_utilizadores.Columns.Add(bfNome);

            BoundField bfDescription = new BoundField();
            bfDescription.HeaderText = "Nome";
            bfDescription.DataField = "name";
            dgv_utilizadores.Columns.Add(bfDescription);

            BoundField bfEstrelas = new BoundField();
            bfEstrelas.HeaderText = "NIF";
            bfEstrelas.DataField = "nif";
            dgv_utilizadores.Columns.Add(bfEstrelas);

            BoundField bfStock = new BoundField();
            bfStock.HeaderText = "Data Nascimento";
            bfStock.DataField = "data_nasc";
            bfStock.DataFormatString = "{0:d}";
            dgv_utilizadores.Columns.Add(bfStock);

            BoundField bfPreco = new BoundField();
            bfPreco.HeaderText = "Saldo";
            bfPreco.DataField = "saldo";
            bfPreco.DataFormatString = "{0:C}";
            dgv_utilizadores.Columns.Add(bfPreco);

            BoundField bfState = new BoundField();
            bfState.HeaderText = "Estado";
            bfState.DataField = "estado";
            dgv_utilizadores.Columns.Add(bfState);

            BoundField bfPerfil = new BoundField();
            bfPerfil.HeaderText = "Perfil";
            bfPerfil.DataField = "perfil";
            dgv_utilizadores.Columns.Add(bfPerfil);

            ButtonField dcOpcoes = new ButtonField();
            dcOpcoes.HeaderText = "Opções";
            dcOpcoes.ButtonType = ButtonType.Link;
            dgv_utilizadores.Columns.Add(dcOpcoes);


            DataColumn dcDetalhes = new DataColumn();
            dcDetalhes.ColumnName = "Histórico Pedidos";
            dcDetalhes.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcDetalhes);

            HyperLinkField hldetalhes = new HyperLinkField();
            hldetalhes.HeaderText = "Histórico Pedidos";
            hldetalhes.DataTextField = "Histórico Pedidos";
            hldetalhes.Text = "Histórico Pedidos";
            hldetalhes.DataNavigateUrlFormatString = "~/admin/historicopedidos.aspx?user={0}";
            hldetalhes.DataNavigateUrlFields = new string[] { "id" };
            dgv_utilizadores.Columns.Add(hldetalhes);

            DataColumn dcDetalhes2 = new DataColumn();
            dcDetalhes2.ColumnName = "Histórico Transações";
            dcDetalhes2.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcDetalhes2);

            HyperLinkField hldetalhes2 = new HyperLinkField();
            hldetalhes2.HeaderText = "Histórico Transações";
            hldetalhes2.DataTextField = "Histórico Transações";
            hldetalhes2.Text = "Histórico Transações";
            hldetalhes2.DataNavigateUrlFormatString = "~/admin/historicotransacoes.aspx?user={0}";
            hldetalhes2.DataNavigateUrlFields = new string[] { "id" };
            dgv_utilizadores.Columns.Add(hldetalhes2);

            dgv_utilizadores.DataBind();
        }

        protected void dgv_utilizadores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count > 1)
            {
                int id = int.Parse(e.Row.Cells[0].Text);
                int estado = int.Parse(e.Row.Cells[6].Text);
                int perfil = int.Parse(e.Row.Cells[7].Text);

                if (estado == 1)
                    e.Row.Cells[6].Text = "Ativado";
                else
                    e.Row.Cells[6].Text = "Bloqueado";

                switch (perfil)
                {
                    case 0:
                        e.Row.Cells[7].Text = "Administrador";
                        break;
                    case 1:
                        e.Row.Cells[7].Text = "Dono Restaurante";
                        break;
                    case 2:
                        e.Row.Cells[7].Text = "Estafeta";
                        break;
                    case 3:
                        e.Row.Cells[7].Text = "Utilizador";
                        break;
                    default:
                        e.Row.Cells[7].Text = "Utilizador";
                        break;
                }

                LinkButton hlativo = new LinkButton();
                hlativo.Text = "<i data-toggle='tooltip' data-placement='top' title='Ativar/Desativar' class='far fa-ban mr-3'></i>";
                hlativo.OnClientClick = $"btnblockclick({id},{(estado == 1 ? "true" : "false")}); return false;";

                LinkButton hleditar = new LinkButton();
                hleditar.Text = "<i data-toggle='tooltip' data-placement='top' title='Editar' class='far fa-edit text-warning mr-3'></i>";
                hleditar.PostBackUrl = $"~/admin/editaruser.aspx?id={id}";

                /*LinkButton hleliminar = new LinkButton();
                hleliminar.Text = "<i data-toggle='tooltip' data-placement='top' title='Eliminar' class='far fa-trash-alt text-danger'></i>";
                hleliminar.OnClientClick = $"btndeleteclick({id}); return false;";*/

                e.Row.Cells[8].Controls.Add(hlativo);
                e.Row.Cells[8].Controls.Add(hleditar);
                //e.Row.Cells[8].Controls.Add(hleliminar);

            }
        }

        protected void btn_ativar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string id_utilizador = Request.Cookies["id_utilizador"].Value;

                if (id_utilizador != null)
                {
                    int num_id_utilizador = int.Parse(id_utilizador);
                    Models.User.ToggleUtilizador(num_id_utilizador, 1);
                    AtualizarGrid();
                    
                }
            }
            catch
            {
                AtualizarGrid();
            }
            Request.Cookies["id_utilizador"].Expires = DateTime.Now.AddDays(-1);
        }

        protected void btn_desativar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string id_utilizador = Request.Cookies["id_utilizador"].Value;
                if (id_utilizador != null)
                {
                    int num_id_utilizador = int.Parse(id_utilizador);

                    Models.User.ToggleUtilizador(num_id_utilizador, 0);
                    AtualizarGrid();

                }
            }
            catch
            {
                AtualizarGrid();
            }
            Request.Cookies["id_utilizador"].Expires = DateTime.Now.AddDays(-1);
        }

        protected void btn_eliminar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string id_utilizador = Request.Cookies["id_utilizador"].Value;
                if (id_utilizador != null)
                {
                    int num_id_utilizador = int.Parse(id_utilizador);

                    Models.User.DeleteUser(num_id_utilizador);
                    AtualizarGrid();

                }
            }
            catch
            {
                AtualizarGrid();
            }
            Request.Cookies["id_utilizador"].Expires = DateTime.Now.AddDays(-1);
        }
    }
}