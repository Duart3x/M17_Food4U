using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.admin
{
    public partial class historicotransacoes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                Response.Redirect("~/index.aspx");

            int id_user = 0;

            if (Request["user"] == null || !int.TryParse(Request["user"].ToString(), out id_user))
                Response.Redirect("~/index.aspx");

            if (IsPostBack)
                return;

            dgv_transacoes.DataSource = Transacao.GetTransacoesUser(id_user);
            dgv_transacoes.DataBind();

            dgv_pagamentos.DataSource = Pagamento.getPagamentosUser(id_user);            
            dgv_pagamentos.DataBind();
        }
    }
}