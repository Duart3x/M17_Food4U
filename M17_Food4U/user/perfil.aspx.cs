using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.user
{
    public partial class perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null)
                Response.Redirect("~/index.aspx");

            if (IsPostBack)
                return;

            txt_email.Attributes.Add("readonly", "true");
            txt_nome.Attributes.Add("readonly", "true");
            txt_nif.Attributes.Add("readonly", "true");
            txt_data_nasc.Attributes.Add("readonly", "true");
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/index.aspx");
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            btn_confirmar.Visible = true;
            btn_cancelar.Visible = true;
            btn_editar.Visible = false;

            txt_email.Attributes.Remove("readonly");
            txt_nome.Attributes.Remove("readonly");
            txt_nif.Attributes.Remove("readonly");
            txt_data_nasc.Attributes.Remove("readonly");

            txt_email.CssClass = "form-control w-75";
            txt_nome.CssClass = "form-control w-75";
            txt_nif.CssClass = "form-control w-75";
            txt_data_nasc.CssClass = "form-control w-75";
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            btn_confirmar.Visible = false;
            btn_cancelar.Visible = false;
            btn_editar.Visible = true;

            txt_email.Attributes.Add("readonly", "true");
            txt_nome.Attributes.Add("readonly", "true");
            txt_nif.Attributes.Add("readonly", "true");
            txt_data_nasc.Attributes.Add("readonly", "true");

            txt_email.CssClass = "form-control-plaintext w-75";
            txt_nome.CssClass = "form-control-plaintext w-75";
            txt_nif.CssClass = "form-control-plaintext w-75";
            txt_data_nasc.CssClass = "form-control-plaintext w-75";
        }

        protected void btn_confirmar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_ConfirmarPassword_ServerClick(object sender, EventArgs e)
        {

        }
    }
}