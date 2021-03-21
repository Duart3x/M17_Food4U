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
    public partial class comentariosrestaurante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                Response.Redirect("~/index.aspx");


            if (Request["restaurante"] == null || !int.TryParse(Request["restaurante"].ToString(), out int id_restaurante))
                Response.Redirect("~/index.aspx");

            if (IsPostBack)
                return;

            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            int id_restaurante = -1;
            if (Request["restaurante"] == null || !int.TryParse(Request["restaurante"].ToString(), out id_restaurante))
                Response.Redirect("~/index.aspx");

            DataTable dados = RestaurantComment.LoadComments(id_restaurante);

            dgv_comentarios.Columns.Clear();
            dgv_comentarios.DataSource = null;
            dgv_comentarios.DataBind();

            dgv_comentarios.DataSource = dados;
            dgv_comentarios.AutoGenerateColumns = false;

            // id
            // comment
            // stars
            // name
            // CreateDate

            BoundField bfID = new BoundField();
            bfID.HeaderText = "ID";
            bfID.DataField = "id";
            dgv_comentarios.Columns.Add(bfID);

            BoundField bfComentario = new BoundField();
            bfComentario.HeaderText = "Comentário";
            bfComentario.DataField = "comment";
            dgv_comentarios.Columns.Add(bfComentario);

            BoundField bfStars = new BoundField();
            bfStars.HeaderText = "Estrelas";
            bfStars.DataField = "stars";
            dgv_comentarios.Columns.Add(bfStars);

            BoundField bfUtilizador = new BoundField();
            bfUtilizador.HeaderText = "Utilizador";
            bfUtilizador.DataField = "name";
            dgv_comentarios.Columns.Add(bfUtilizador);

            BoundField bfCreateDate = new BoundField();
            bfCreateDate.HeaderText = "Data";
            bfCreateDate.DataField = "CreateDate";
            dgv_comentarios.Columns.Add(bfCreateDate);

            ButtonField dcOpcoes = new ButtonField();
            dcOpcoes.HeaderText = "Opções";
            dcOpcoes.ButtonType = ButtonType.Link;
            dgv_comentarios.Columns.Add(dcOpcoes);

            dgv_comentarios.DataBind();
        }

        protected void dgv_comentarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count > 1)
            { 
                int id = int.Parse(e.Row.Cells[0].Text);
                int estrelas = int.Parse(e.Row.Cells[2].Text);


                string estrelasHtml = "";

                for (int i = 1; i < 6; i++)
                {
                    if (i <= estrelas)
                        estrelasHtml += "<i class='fas fa-star text-warning'></i>";
                    else
                        estrelasHtml += "<i class='far fa-star text-warning'></i>";
                }

                e.Row.Cells[2].Text = estrelasHtml;

                LinkButton hleliminar = new LinkButton();
                hleliminar.Text = "<i data-toggle='tooltip' data-placement='top' title='Eliminar' class='far fa-trash-alt text-danger'></i>";
                hleliminar.OnClientClick = $"btndeleteclick({id}); return false;";

                e.Row.Cells[5].Controls.Add(hleliminar);
            }
        }

        protected void btn_eliminar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string id_comentario = Request.Cookies["id_comentario"].Value;
                if (id_comentario != null)
                {
                    int num_id_comentario = int.Parse(id_comentario);

                    Models.RestaurantComment.DeleteComment(num_id_comentario);
                    AtualizarGrid();
                }
            }
            catch
            {
                AtualizarGrid();
            }
            Request.Cookies["id_comentario"].Expires = DateTime.Now.AddDays(-1);
        }
    }
}