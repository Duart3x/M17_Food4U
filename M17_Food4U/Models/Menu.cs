using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class Menu
    {
        public int id { get; set; }
        public int restaurant { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int stars { get; set; }
        public bool stock { get; set; }
        public bool enabled { get; set; }

        BaseDados bd;
        public Menu()
        {
            bd = new BaseDados();
        }

        public Menu(int restaurant, string title, string description, double price)
        {
            this.restaurant = restaurant;
            this.title = title;
            this.description = description;
            this.price = price;
            bd = new BaseDados();

        }

        public int Adicionar()
        {
            string sql = $@"INSERT INTO menus (restaurant, title, description, price) VALUES (@restaurant,@title,@description,@price); SELECT SCOPE_IDENTITY();";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@restaurant",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.restaurant
                },
                new SqlParameter()
                {
                    ParameterName="@title",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.title
                },
                new SqlParameter()
                {
                    ParameterName="@description",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.description
                },
                new SqlParameter()
                {
                    ParameterName="@price",
                    SqlDbType=System.Data.SqlDbType.Decimal,
                    Value=this.price
                },
            };
            DataTable dados = bd.devolveSQL(sql, parametros);
            id = int.Parse(dados.Rows[0].ItemArray[0].ToString());

            return id;
        }

        public static DataTable ListarMenusDiponíveis()
        {
            BaseDados bd = new BaseDados();
            DataTable dados = bd.devolveSQL("SELECT menus.id,menus.title,menus.description,menus.price,menus.stars,menus.stock, restaurants.name as restaurant FROM menus JOIN restaurants ON menus.restaurant = restaurants.id  WHERE menus.enabled = 1 AND restaurants.enabled = 1");
            return dados;
        }
    }
}