using M17_Food4U.Classes;
using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace M17_Food4U
{
    /// <summary>
    /// Summary description for servicos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class servicos : System.Web.Services.WebService
    {

        [WebMethod]
        public string PesquisaConteudos(string term)
        {
            PesquisaAvancada pesquisa = PesquisaAvancada.PesquisarPorTermo(term);

            return new JavaScriptSerializer().Serialize(pesquisa);
        }

    }
}
