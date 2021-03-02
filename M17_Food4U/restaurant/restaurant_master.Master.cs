using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.restaurant
{
    public partial class restaurant_master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["perfil"] == null || Session["perfil"].ToString() != "1")
                Response.Redirect("~/index.aspx");
            if (IsPostBack)
                return;
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

        protected void dp_restaurantes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}