using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class Orders
    {
        public int id { get; set; }
        public int client { get; set; }
        public int courier { get; set; }
        public int state { get; set; }
        public DateTime deliveryDate { get; set; }
        public DateTime createDate { get; set; }

        

        BaseDados bd;
        public Orders()
        {
            bd = new BaseDados();
        }

        public static DataTable GetOrdersFromUser(int id_user)
        {
            BaseDados bd = new BaseDados();
            string sql = $@"SELECT orders.id, orders.[state], orders.createDate 
FROM orders WHERE client = {id_user};";

            return bd.devolveSQL(sql);
        }

        public static DataTable GetOrdersFromUserExtended(int id_user)
        {
            BaseDados bd = new BaseDados();
            string sql = $@"SELECT orders.id, menus.title as Menu, orders_menus.quantity as Quantidade, 
                users.[name] as Estafeta,
                CASE
                    WHEN orders.[state] = 1 THEN 'Em processamento'
                    WHEN orders.[state] = 2 THEN 'A ser preparado'
                    WHEN orders.[state] = 3 THEN 'A caminho'
                    WHEN orders.[state] = 4 THEN 'Entregue'
                    WHEN orders.[state] = 5 THEN 'Cancelado'
                    ELSE 'Desconhecido'
                END AS Estado, orders.createDate as [Data]
                FROM orders, orders_menus, menus, users 
                WHERE orders.id = orders_menus.[order] AND orders_menus.menu = menus.id AND orders.courier = users.id
                AND orders.client = {id_user};";

            return bd.devolveSQL(sql);
        }

        internal static void ToggleState(int id_pedido, int estado)
        {
            BaseDados bd = new BaseDados();
            string sql = $"UPDATE orders SET state = {estado} WHERE id = {id_pedido}";

            bd.executaSQL(sql);
        }

        internal static DataTable GetAllOrders()
        {
            BaseDados bd = new BaseDados();
            string sql = $@"SELECT orders.id,users2.[name] as Cliente, 
                    users1.[name] as Estafeta,
                    CASE
                        WHEN orders.[state] = 1 THEN 'Em processamento'
                        WHEN orders.[state] = 2 THEN 'A ser preparado'
                        WHEN orders.[state] = 3 THEN 'A caminho'
                        WHEN orders.[state] = 4 THEN 'Entregue'
                        WHEN orders.[state] = 5 THEN 'Cancelado'
                        ELSE 'Desconhecido'
                    END AS Estado, orders.createDate as [Data]
                    FROM orders, users as users1, users as users2
                    WHERE orders.courier = users1.id AND orders.client = users2.id";

            return bd.devolveSQL(sql);
        }
    }
}