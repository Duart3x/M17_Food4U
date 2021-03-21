using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U.admin
{
    public partial class editaruser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                Response.Redirect("~/index.aspx");


            if (Request["id"] == null || !int.TryParse(Request["id"].ToString(), out int id_user))
                Response.Redirect("~/index.aspx");

            if (IsPostBack)
                return;
        }

        protected void btn_confimar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_user = -1;
                if (Request["id"] == null || !int.TryParse(Request["id"].ToString(), out id_user))
                    Response.Redirect("~/index.aspx");

                int value = int.Parse(radius_list.SelectedValue.ToString());
                if (value <= -1 || value >= 4)
                    throw new Exception();

                Models.User.MudarPerfil(id_user,value);
                Response.Redirect("~/admin/users.aspx");
            }
            catch (Exception)
            {
                Response.Redirect("~/admin/users.aspx");
            }
        }
    }
}