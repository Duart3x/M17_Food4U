using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class ShoppingCart
    {
        public int id { get; set; }
        public int user { get; set; }
        public int quantidade { get; set; }
        public int menu { get; set; }
        public DateTime insertDate { get; set; }


        public static void InsertMenuCarrinho(int user, int menu, int quantidade)
        {
            BaseDados bd = new BaseDados();

            string sql = $"SELECT id FROM shopping_carts WHERE [user] = {user} AND menu = {menu}";

            DataTable dados = bd.devolveSQL(sql);

            if(dados != null && dados.Rows.Count > 0 && dados.Rows[0] != null)
            {
                AlterarQuantidadeMenu(user, menu, quantidade);
            }
            else
            {
                sql = $"INSERT INTO shopping_carts([user],quantity,menu) VALUES({user},{quantidade},{menu})";

                bd.executaSQL(sql);
            }


            
        }
        public static void AlterarQuantidadeMenu(int user, int menu, int quantidade)
        {
            BaseDados bd = new BaseDados();

            string sql = $"SELECT quantity FROM shopping_carts WHERE [user] = {user} AND menu = {menu}";
            
            DataTable dados = bd.devolveSQL(sql);
            if(int.Parse(dados.Rows[0]["quantity"].ToString()) + quantidade > 0)
            {
                if (int.Parse(dados.Rows[0]["quantity"].ToString()) + quantidade >= 5)
                    quantidade = 5 - int.Parse(dados.Rows[0]["quantity"].ToString());

                sql = $"UPDATE shopping_carts SET quantity = (quantity + {quantidade}) WHERE [user] = {user} AND menu = {menu}";

                bd.executaSQL(sql);
            }
            else
            {
                sql = $"DELETE FROM shopping_carts WHERE [user] = {user} AND menu = {menu}";
                
                bd.executaSQL(sql);
            }
        }
        public static DataTable GetCarrinhoFromUser(int id_user)
        {
            BaseDados bd = new BaseDados();
            
            string sql = $@"SELECT shopping_carts.id,restaurants.id as id_restaurant, menus.title as Menu,menus.price as preco, 
                        menus.description as descricao, menus.id as MenuId, shopping_carts.quantity as Quantidade 
                        FROM shopping_carts INNER JOIN menus ON shopping_carts.menu = menus.id INNER JOIN restaurants ON menus.restaurant = restaurants.id
                        WHERE [user] = {id_user} ORDER BY insertdate";

            return bd.devolveSQL(sql);
        }

        public static void DeleteCarrinho(int id_user)
        {
            BaseDados bd = new BaseDados();

            string sql = $"DELETE FROM shopping_carts WHERE [user] = {id_user}";

            bd.executaSQL(sql);
        }

        public static double GetCarrinhoValue(int id_user)
        {
            BaseDados bd = new BaseDados();

            string sql = $@"SELECT SUM(menus.price * quantity) as valor
                        FROM shopping_carts INNER JOIN menus ON shopping_carts.menu = menus.id 
                        WHERE [user] = {id_user}";

            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count <= 0)
                return 0;
            
            return double.Parse(dados.Rows[0]["valor"].ToString());
        }

    }
}