using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17_Food4U.Classes
{
    public class PesquisaAvancada
    {
        public List<Menu> menus { get; set; }
        public List<Restaurant> restaurants { get; set; }

        public static PesquisaAvancada PesquisarPorTermo(string term)
        {
            BaseDados bd = new BaseDados();
            string sql = $@"SELECT TOP 10 id,title,description,stars,stock,enabled
                            FROM menus 
                            WHERE enabled = 1 AND title LIKE @term ORDER BY stock DESC, stars DESC";

            List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName="@term",SqlDbType=SqlDbType.VarChar,Value= "%" + term + "%" }
                };
            DataTable menusDados = bd.devolveSQL(sql, parametros);

            sql = $@"SELECT TOP 10 id,name,enabled,owner,city,address
                            FROM restaurants 
                            WHERE enabled = 1 AND name LIKE @term ORDER BY name ASC";

            parametros = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName="@term",SqlDbType=SqlDbType.VarChar,Value= "%" + term + "%" }
                };
            DataTable restaurantsDados = bd.devolveSQL(sql, parametros);

            PesquisaAvancada pesquisa = new PesquisaAvancada();
            List<Menu> menus = new List<Menu>();
            List<Restaurant> restaurants = new List<Restaurant>();

            for (int i = 0; i < menusDados.Rows.Count; i++)
            {
                Menu novo = new Menu();

                novo.id = int.Parse(menusDados.Rows[i]["id"].ToString());
                novo.stock = bool.Parse(menusDados.Rows[i]["stock"].ToString());
                novo.enabled = bool.Parse(menusDados.Rows[i]["enabled"].ToString());
                novo.title = menusDados.Rows[i]["title"].ToString();
                novo.stars = int.Parse(menusDados.Rows[i]["stars"].ToString());

                novo.description = menusDados.Rows[i]["description"].ToString();

                menus.Add(novo);
            }
            pesquisa.menus = menus;

            for (int i = 0; i < restaurantsDados.Rows.Count; i++)
            {
                Restaurant novo = new Restaurant();

                novo.id = int.Parse(restaurantsDados.Rows[i]["id"].ToString());
                novo.name = restaurantsDados.Rows[i]["name"].ToString();
                novo.city = restaurantsDados.Rows[i]["city"].ToString();
                novo.address = restaurantsDados.Rows[i]["address"].ToString();
                novo.owner = int.Parse(restaurantsDados.Rows[i]["owner"].ToString());
                novo.enabled = bool.Parse(restaurantsDados.Rows[i]["enabled"].ToString());

                restaurants.Add(novo);
            }
            pesquisa.restaurants = restaurants;

            return pesquisa;
        }
    }
}