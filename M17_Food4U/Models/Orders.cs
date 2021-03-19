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
    }
}