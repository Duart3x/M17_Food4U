using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.estafeta
{
    public partial class master_estafetas : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "2")
                Response.Redirect("~/index.aspx");
            HttpCookie cookie = Request.Cookies["allow-cookies"];
            if (cookie != null)
                div_aviso.Visible = false;
            if (IsPostBack)
                return;
        }

        protected void bt1_Click(object sender, EventArgs e)
        {
            //criar o cookie e enviar para o browser
            div_aviso.Visible = false;
            HttpCookie novo = new HttpCookie("allow-cookies");
            novo.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(novo);
        }
        protected void btn_user_Click(object sender, EventArgs e)
        {
            if (Session["perfil"] != null)
            {
                Response.Redirect("/User/perfil.aspx");
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }
    }
}