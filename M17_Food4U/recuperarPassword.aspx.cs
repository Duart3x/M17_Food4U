using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_Food4U
{
    public partial class recuperarPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btNovaPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string guid = Server.UrlDecode(Request["id"].ToString());
                string novaPassword = tbPassword.Text;
                if (novaPassword == String.Empty)
                    throw new Exception("");
                Models.User utilizador = new Models.User();
                utilizador.atualizarPassword(guid, novaPassword);
                Response.Redirect("~/login.aspx");
            }
            catch
            {
                Response.Redirect("~/login.aspx");

            }
        }
    }
}