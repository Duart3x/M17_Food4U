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

        internal static bool IsMenuEnabled(int id_menu)
        {
            BaseDados bd = new BaseDados();
            string sql = $"SELECT enabled,restaurant FROM menus WHERE id = {id_menu}";

            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0 || dados.Rows.Count > 1)
                return false;

            int id_restaurante = int.Parse(dados.Rows[0]["restaurant"].ToString());

            if (!Restaurant.IsRestaurantEnabled(id_restaurante))
                return false;

            return bool.Parse(dados.Rows[0]["enabled"].ToString());
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
                    Value= (this.price) * 1.6
                },
            };
            DataTable dados = bd.devolveSQL(sql, parametros);
            id = int.Parse(dados.Rows[0].ItemArray[0].ToString());

            return id;
        }
        public Menu GetMenuInter(int id_menu)
        {
            string sql = $"SELECT * FROM menus WHERE id = {id_menu}";

            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0 || dados.Rows.Count > 1)
                return null;

            Menu menu = new Menu();
            menu.id = id_menu;
            menu.restaurant = int.Parse(dados.Rows[0]["restaurant"].ToString());
            menu.title = dados.Rows[0]["title"].ToString();
            menu.description = dados.Rows[0]["description"].ToString();
            menu.stock = bool.Parse(dados.Rows[0]["stock"].ToString());
            menu.stars = int.Parse(dados.Rows[0]["stars"].ToString());
            menu.price = double.Parse(dados.Rows[0]["price"].ToString());
            menu.enabled = bool.Parse(dados.Rows[0]["enabled"].ToString());

            return menu;
        }

        internal static Menu GetMenu(int id_menu)
        {
            BaseDados bd = new BaseDados();
            string sql = $"SELECT * FROM menus WHERE id = {id_menu}";

            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0 || dados.Rows.Count > 1)
                return null;

            Menu menu = new Menu();
            menu.id = id_menu;
            menu.restaurant = int.Parse(dados.Rows[0]["restaurant"].ToString());
            menu.title = dados.Rows[0]["title"].ToString();
            menu.description = dados.Rows[0]["description"].ToString();
            menu.stock = bool.Parse(dados.Rows[0]["stock"].ToString());
            menu.stars = int.Parse(dados.Rows[0]["stars"].ToString());
            menu.price = double.Parse(dados.Rows[0]["price"].ToString());
            menu.enabled = bool.Parse(dados.Rows[0]["enabled"].ToString());

            return menu;
        }

        public static DataTable ListarMenusDiponíveis(string termo = "")
        {
            BaseDados bd = new BaseDados();
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@termo",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value= "%"+termo+"%"
                }
            };

            DataTable dados = bd.devolveSQL($@"SELECT menus.id,menus.title,menus.description,menus.price,menus.stars,menus.stock, restaurants.name as restaurant 
                                            FROM menus JOIN restaurants ON menus.restaurant = restaurants.id  
                                            WHERE menus.enabled = 1 AND restaurants.enabled = 1 AND menus.title LIKE @termo", parametros);
            
            return dados;
        }

        internal static bool UserOwnsMenu(int id_user, int id_menu)
        {
            BaseDados bd = new BaseDados();

            string sql = $@"SELECT menus.id FROM menus INNER JOIN restaurants ON menus.restaurant = restaurants.id WHERE menus.id = {id_menu} AND restaurants.owner = {id_user}";

            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count <= 0 || dados.Rows.Count > 1)
                return false;

            return true;
        }

        internal void Atualizar()
        {
            string sql = "UPDATE menus SET title=@title, description=@description,price=@price WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@id",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.id
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
            bd.executaSQL(sql, parametros);
        }

        public static void ToggleMenu(int id_menu, bool state)
        {
            BaseDados bd = new BaseDados();

            string sql = "UPDATE menus SET [enabled] = @enabled WHERE id = @id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@id",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=id_menu
                },
                new SqlParameter()
                {
                    ParameterName="@enabled",
                    SqlDbType=System.Data.SqlDbType.Bit,
                    Value=state
                },
            };

            bd.executaSQL(sql, parametros);
        }

        public static void ToggleStockMenu(int id_menu, bool state)
        {
            BaseDados bd = new BaseDados();

            string sql = "UPDATE menus SET [stock] = @stock WHERE id = @id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@id",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=id_menu
                },
                new SqlParameter()
                {
                    ParameterName="@stock",
                    SqlDbType=System.Data.SqlDbType.Bit,
                    Value=state
                },
            };

            bd.executaSQL(sql, parametros);
        }
    }
}