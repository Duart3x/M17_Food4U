using M17_Food4U.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace M17_Food4U.Classes
{
    public class Estatisticas
    {
        BaseDados bd;
        public Estatisticas()
        {
            bd = new BaseDados();
        }
        /*
            <h3>Saldo depositado na plataforma: <span id="lb_saldo" runat="server"></span></h3>
            <hr />
            <h3>Total Utilizadores Registados: <span id="lb_userscount" runat="server"></span></h3>
            <h3>Utilizadores: <span id="lb_normaluserscount" runat="server"></span></h3>
            <h3>Estafetas: <span id="lb_estafetascount" runat="server"></span></h3>
            <h3>Administradores: <span id="lb_admincount" runat="server"></span></h3>
            <hr />
            <h3>Donos Restaurantes: <span id="lb_donosrestaurantes" runat="server"></span></h3>
            <h3>Total Restaurantes Registados: <span id="lb_restaurantescount" runat="server"></span></h3>
            <hr />
            <h3>Total Pedidos: <span id="lb_pedidoscount" runat="server"></span></h3>
            <h3>Total Pedidos Finalizados: <span id="lb_pedidosfinished" runat="server"></span></h3>
            <h3>Menu mais pedido: <span id="lb_menumaispedido" runat="server"></span></h3>
            <h3>Menu com mais rating: <span id="lb_menurating" runat="server"></span></h3>
            <h3>Restaurante com mais rating: <span id="lb_restaurantrating" runat="server"></span></h3>
         */
        #region Users
        public double getSaldoPlataforma()
        {
            string sql = "SELECT CAST(SUM(saldo) as INT) as saldo FROM users";
            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0)
                return 0;

            return int.Parse(dados.Rows[0]["saldo"].ToString());
        }

        public int getTotalUsers()
        {
            string sql = "SELECT COUNT(*) as total FROM users";
            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0)
                return 0;

            return int.Parse(dados.Rows[0]["total"].ToString());
        }

        public int getTotalNormalUsers()
        {
            string sql = "SELECT COUNT(*) as total FROM users WHERE perfil = 3";
            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0)
                return 0;

            return int.Parse(dados.Rows[0]["total"].ToString());
        }
        public int getTotalEstafetas()
        {
            string sql = "SELECT COUNT(*) as total FROM users WHERE perfil = 2";
            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0)
                return 0;

            return int.Parse(dados.Rows[0]["total"].ToString());
        }
        public int getTotalAdministradores()
        {
            string sql = "SELECT COUNT(*) as total FROM users WHERE perfil = 0";
            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0)
                return 0;

            return int.Parse(dados.Rows[0]["total"].ToString());
        }

        public int getTotalDonosRestaurantes()
        {
            string sql = "SELECT COUNT(*) as total FROM users WHERE perfil = 1";
            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0)
                return 0;

            return int.Parse(dados.Rows[0]["total"].ToString());
        }

        public int getTotalRestaurantes()
        {
            string sql = "SELECT COUNT(*) as total FROM restaurants";
            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0)
                return 0;

            return int.Parse(dados.Rows[0]["total"].ToString());
        }
        #endregion

        #region Pedidos
        
        public int getTotalPedidos()
        {
            string sql = "SELECT COUNT(*) as total FROM orders";
            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0)
                return 0;

            return int.Parse(dados.Rows[0]["total"].ToString());
        }

        public int getTotalPedidosFinalizados()
        {

            string sql = "SELECT COUNT(*) as total FROM orders WHERE [state] = 4";
            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0)
                return 0;

            return int.Parse(dados.Rows[0]["total"].ToString());
        }
        #endregion

        #region Menus

        /// <summary>
        /// Devolve o menu mais pedido
        /// </summary>
        /// <returns>NumPedidos, title, restaurante</returns>
        public DataTable getMenuMaisPedido()
        {
            string sql = @"SELECT Count(*) as NumPedidos,menus.title,restaurants.[name] as restaurante
FROM orders_menus INNER JOIN menus on orders_menus.menu = menus.id INNER JOIN restaurants on menus.restaurant = restaurants.id GROUP BY orders_menus.menu,menus.id,menus.title,restaurants.[name]";
            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0)
                return null;

            return dados;
        }


        /// <summary>
        /// Devolve o menu mais votado
        /// </summary>
        /// <returns>title, stars, restaurante</returns>
        public DataTable getMenuMaisRating()
        {
            string sql = @"SELECT TOP 1 menus.title,menus.stars,restaurants.[name] as restaurante FROM menus inner join restaurants on menus.restaurant = restaurants.id ORDER BY stars DESC, price ASC";
            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0)
                return null;

            return dados;
        }

        /// <summary>
        /// Devolve o restaurante mais votado
        /// </summary>
        /// <returns>name, NumStars</returns>
        public DataTable GetRestauranteMaisRating()
        {
            string sql = @"SELECT TOP 1 restaurants.[name],SUM(stars) as NumStars FROM restaurant_comments INNER JOIN restaurants ON restaurant_comments.restaurant = restaurants.id GROUP by restaurant,restaurants.[name] ORDER BY SUM(stars) DESC";
            DataTable dados = bd.devolveSQL(sql);

            
            if (dados == null || dados.Rows.Count == 0)
                return null;

            return dados;
        }
        #endregion
    }
}